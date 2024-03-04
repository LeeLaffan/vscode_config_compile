barracks_set_rally = class({})

units_spawned = {}

function barracks_set_rally:OnSpellStart()
    local caster = self:GetCaster()
    local point = self:GetCursorPosition()

    print(point)
    print("POINT 1"..point[1])

    caster:SetContextNum("rally_x", point[1], 0)
    caster:SetContextNum("rally_y", point[2], 0)
    caster:SetContextNum("rally_z", point[3], 0)

    local unit_count = table.getn(units_spawned)

    local john = CreateUnitByName("johnboy", rally, true, nil, nil, DOTA_TEAM_GOODGUYS)
    john:SetUnitName(john:GetUnitName().."_"..unit_count)
    print("Created unit "..john:GetUnitName())


    units_spawned[unit_count] = john

end