custom_barracks = class({})

if CustommBarracks == nil then
    CustomBarracks = class({})
end

function CustomBarracks:SpawnUnit(bar_id, unit_id)

end

function Spawn( entityKeyValues )
    if not IsServer() then return end
    --custom_barracks:Spawned()
    ListenToGameEvent('entity_killed', OnEntityKilled, nil)

    print("SPAWNED")
end


function OnEntityKilled(args)
    if not IsServer() then return end

    print("Bart Entity Killed")
    print(tostring(args == nil))
    print(args.entindex_killed)
    local hero = EntIndexToHScript(args.entindex_killed)

    print(hero:GetUnitName())
    custom_barracks:UnitKilled(hero:GetUnitName())

end

function custom_barracks:UnitKilled(unit_name)
    local id = string.sub(unit_name, 9, 10)

    print("unit killed: "..id)

    print(self:units_spawned[id])
end

function custom_barracks:Spawned()
    self:SetGenericRallyPoint()

    self:units_spawned = {}

    self:SpawnUnit()
end

function custom_barracks:SpawnUnit()
    local rally = self:GetRallyPoint()

    local unit_count = table.getn(units_spawned)
    print("Current unit count before spawn"..unit_count)

    local john = CreateUnitByName("johnboy", rally, true, nil, nil, DOTA_TEAM_GOODGUYS)
    john:SetUnitName(john:GetUnitName().."_"..unit_count)
    print("Created unit "..john:GetUnitName())

    self:units_spawned[unit_count] = john


end

function custom_barracks:SetGenericRallyPoint()
    local point = Vector(-3108.893066, -2321.499756, 256.000000)
    self:SetContextNum("rally_x", point[1] + 200, 0)
    self:SetContextNum("rally_y", point[2], 0)
    self:SetContextNum("rally_z", point[3], 0)
end

function barracks_spawn_soldier:GetRallyPoint()
    local rally_x = self:GetContext("rally_x")
    local rally_y = self:GetContext("rally_y")
    local rally_z = self:GetContext("rally_z")
    return Vector(rally_x, rally_y, rally_z)
end

