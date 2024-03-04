custom_blink = class({})

function custom_blink:OnSpellStart()
    local caster = self:GetCaster()
    local point = self:GetCursorPosition()
    --FindClearSpaceForUnit(caster, point, true)
    print(point)
    local barracks = CreateUnitByName("custom_barracks", point, true, nil, nil, DOTA_TEAM_GOODGUYS)

    barracks:SetOwner(caster)
    barracks:SetControllableByPlayer(caster:GetPlayerID(), true)
    barracks:SetAbsOrigin(point)

    print("1")
    --FindClearSpaceForUnit(barracks, point, true)
    print("2")
end