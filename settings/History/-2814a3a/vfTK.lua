custom_barracks = class({})

-- if custom_barracks == nil then
--     custom_barracks = class({})
--     --ListenToGameEvent('npc_spawned', Dynamic_Wrap(CustomBarracks, "OnEntitySpawn"), nil)
-- end

raxManager = require('RaxManager')


function custom_barracks:OnEntitySpawn(args)
    print("entitySpawned: "..self:GetEntityIndex())
end

function custom_barracks:GetUnitId()
    if units == nil then
        units = {}
        return 0
    end

    local count = table.getn(units)
    print("Count was "..count)
    return count
end

function Spawn(entityKeyValues )
    if not IsServer() then return end
    --custom_barracks:Spawned()
    --ListenToGameEvent('entity_killed', OnEntityKilled, nil)

    thisEntity:SetMoveCapability(0)


    --print("PRINTING"..thisEntity:GetEntityIndex())
    --print(custom_barracks:OnEntitySpawn(entityKeyValues))
    raxManager.OnRaxCreated("johnboy", thisEntity:GetEntityIndex())
    --self:SetUnitName("Rax_1")

    print("SPAWNED RAX")
end


function custom_barracks:SpawnUnit()
    local rally = self:GetRallyPoint()

    local unit_count = table.getn(units_spawned)
    print("Current unit count before spawn"..unit_count)

    local john = CreateUnitByName("johnboy", rally, true, nil, nil, DOTA_TEAM_GOODGUYS)
    john:SetUnitName(john:GetUnitName().."_"..unit_count)
    print("Created unit "..john:GetUnitName())

    --self:units_spawned[unit_count] = john


end

function custom_barracks:SetGenericRallyPoint()
    local point = Vector(-3108.893066, -2321.499756, 256.000000)
    self:SetContextNum("rally_x", point[1] + 200, 0)
    self:SetContextNum("rally_y", point[2], 0)
    self:SetContextNum("rally_z", point[3], 0)
end

function custom_barracks:GetRallyPoint()
    local rally_x = self:GetContext("rally_x")
    local rally_y = self:GetContext("rally_y")
    local rally_z = self:GetContext("rally_z")
    return Vector(rally_x, rally_y, rally_z)
end

function custom_barracks:Spawn()
    print("SPW CNSTR")
    --self:SetGenericRallyPoint()

    --self:units_spawned = {}
    print("Rax EntIndex: "..self:GetEntityIndex())

    self:SpawnUnit()
end

