barracks_set_rally = class({})

units_spawned = {}

function barracks_set_rally:OnSpellStart()
    local caster = self:GetCaster()
    local point = self:GetCursorPosition()

    print(rally)
    print("rally 1"..rally[1])

    caster:SetContextNum("rally_x", rally[1], 0)
    caster:SetContextNum("rally_y", rally[2], 0)
    caster:SetContextNum("rally_z", rally[3], 0)

    local unit_count = table.getn(units_spawned)

    local john = CreateUnitByName("johnboy", rally, true, nil, nil, DOTA_TEAM_GOODGUYS)
    john:SetUnitName(john:GetUnitName().."_"..unit_count)
    print("Created unit "..john:GetUnitName())


    units_spawned[unit_count] = john

end