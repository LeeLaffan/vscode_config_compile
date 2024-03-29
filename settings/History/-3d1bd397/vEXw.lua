local RaxManager = {}

rax = {}

key_solider = "soldier"
key_max_units = "max_units"
key_allowed_units = "allowed_units_count"
key_units_spawned = "units_spawned"
key_units = "units"
key_rally = "rally"
key_rax_id = "rax_id"
key_unit_modifier = "unit_modifier"

LinkLuaModifier("soldier_respawn_modifier", "modifiers/soldier_respawn_modifier.lua", LUA_MODIFIER_MOTION_NONE )
LinkLuaModifier("soldier_respawn_modifier_timer", "modifiers/soldier_respawn_modifier_timer.lua", LUA_MODIFIER_MOTION_NONE )



function RaxManager.OnRaxCreated(soldier_name, rax_id)
    print("RaxManager: Rax created with id "..rax_id.." and soldier "..soldier_name)
    rax[rax_id] = {}
    local r = rax[rax_id]
    r[key_solider] = soldier_name
    r[key_max_units] = 3
    r[key_units_spawned] = 0
    r[key_units] = {}
    r[key_allowed_units] = 3

    local raxEnt = EntIndexToHScript(rax_id)
    local kv = {}
    kv[key_rax_id] = rax_id
    local modifier = raxEnt:AddNewModifier(raxEnt, nil, "soldier_respawn_modifier", kv)
    modifier:SetStackCount(3)
    r[key_unit_modifier] = modifier

end

function RaxManager.AllowUnitSpawn(rax_id)
    local r = rax[rax_id]
    local max_units = r[key_max_units]
    local raxEnt = EntIndexToHScript(rax_id)

    r[key_allowed_units] = r[key_allowed_units] + 1
    local mod = r[key_unit_modifier]
    mod:SetStackCount(r[key_allowed_units])

    print("RaxManager:RaxId="..rax_id..":Attempting to allow unit spawn: Allowed="..r[key_allowed_units].." Max="..max_units)

    if r[key_allowed_units] < max_units then
        raxEnt:AddNewModifier(raxEnt, nil, "soldier_respawn_modifier_timer", {})
    end

    if r[key_units_spawned] < max_units then
        RaxManager.SpawnUnit(rax_id)
    end
end

ListenToGameEvent('entity_killed', Dynamic_Wrap(RaxManager, "OnEntityKilled"), nil)
function RaxManager.OnEntityKilled(args)
    if not IsServer() then return end
    print("Unit killed: "..args.entindex_killed)
    local hero = EntIndexToHScript(args.entindex_killed)


    local rax_id = 0    
    for k,v in pairs(rax) do
        local temp = v[key_units]
        --print("found TEMP: "..temp)
        if temp[args.entindex_killed] ~= nil then
            rax_id = k
            temp[args.entindex_killed] = nil
        end
    end

    if rax_id == 0 then
        return
    end


    local r = rax[rax_id]
    local uid = r["units_spawned"]
    r["units_spawned"] = uid - 1
    local allowed_units = r[key_allowed_units]

    print("RaxManager: Unit died from rax. UnitId="..args.entindex_killed..", RaxId="..rax_id.." Allowed="..allowed_units)

    if allowed_units > 0 then
        print("Unit died! Can spawn another!")
        RaxManager.SpawnUnit(rax_id)
    end
end

function RaxManager.SetRally(point, rax_id)
    local rally_vector = Vector(point["x"], point["y"], point["z"])
    local r = rax[rax_id]

    if r[key_rally] == nil then
        print("first rally!")
        print("RaxManager: Setting Rally for RaxId: "..rax_id.." Point: "..(tostring(v)))
        r[key_rally] = rally_vector
        for i=1, tonumber(r[key_max_units]) do
            RaxManager.SpawnUnit(rax_id)
        end
    end

    print("RaxManager: Setting Rally for RaxId: "..rax_id.." Point: "..(tostring(rally_vector)))
    r[key_rally] = rally_vector

    for key, val in pairs(r[key_units]) do
        print("Unit = "..key)
        local soldier = EntIndexToHScript(key)
        soldier:MoveToPosition(rally_vector)
    end
end

function RaxManager.GetRally(rax_id)
    local r = rax[rax_id]
    local rally = r[key_rally]
    local v = Vector(rally["x"], rally["y"], rally["z"])
    return v
end

function RaxManager.SpawnUnit(rax_id)
    local r = rax[rax_id]
    local uid = r[key_units_spawned]
    local max_units = r[key_max_units]
    local soldier = r[key_solider]

    if uid >= max_units then
        print("Attempted to spawn unit over ma, units spawned: "..uid)
        return
    end

    local rally = RaxManager.GetRally(rax_id)
    local raxEnt = EntIndexToHScript(rax_id)

    local john = CreateUnitByName(soldier, raxEnt:GetAbsOrigin(), true, nil, nil, DOTA_TEAM_GOODGUYS)
    --FindClearSpaceForUnit(john, raxEnt:GetAbsOrigin(), true)
    john:MoveToPosition(rally)

    john:SetUnitName(john:GetUnitName().."_"..rax_id)
    print("Created unit: "..john:GetUnitName().." Setting uid to ".. uid+1)

    r[key_units_spawned] = uid + 1
    r[key_allowed_units] = r[key_allowed_units] - 1
    local mod = r[key_unit_modifier]
    mod:SetStackCount(r[key_allowed_units])

    local units = r[key_units] 
    units[john:GetEntityIndex()] = false

    raxEnt:AddNewModifier(raxEnt, nil, "soldier_respawn_modifier_timer", {})

    --CreateModifierThinker(john, nil, "soldier_respawn_modifier", {}, rally, DOTA_TEAM_GOODGUYS, true)
end

return RaxManager