custom_blink = class({})

function custom_blink:OnSpellStart()
    local caster = self:GetCaster()
    local point = self:GetCursorPosition()
    --FindClearSpaceForUnit(caster, point, true)
    print(point)
    local barracks = CreateUnitByName("custom_barracks", Vector(point.x, point.y, point.z), true, nil, nil, DOTA_TEAM_GOODGUYS)

    barracks:SetOwner(caster)
    barracks:SetControllableByPlayer(caster:GetPlayerId())

    print("1")
    --FindClearSpaceForUnit(barracks, point, true)
    print("2")
end