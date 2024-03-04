custom_blink = class({})

function custom_blink:OnSpellStart()
    local caster = self:GetCaster()
    local point = self:GetCursorPosition()
    --FindClearSpaceForUnit(caster, point, true)
    print(point)
    local barracks = CreateUnitByName("custom_barracks", point, true, nil, nil, DOTA_TEAM_GOODGUYS)
--CreateUnitByName("leeboy", Vector(-942.490173, -772.419556, 128.000000), true, nil, nil, DOTA_TEAM_BADGUYS)

    barracks:SetOwner(caster)
    barracks:SetControllableByPlayer(caster:GetPlayerID(), true)
    barracks:SetAbsOrigin(point)

    print("1")
    --FindClearSpaceForUnit(barracks, point, true)
    print("2")
end