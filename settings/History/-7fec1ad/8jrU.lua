custom_blink = class({})

function custom_blink:OnSpellStart()
    local caster = self:GetCaster()
    local point = self:GetCursorPosition()
    --FindClearSpaceForUnit(caster, point, true)
    print(point)
    CreateUnitByName("custom_barracks", Vector(point:X, point:Y, 0), true, nil, nil, DOTA_TEAM_GOODGUYS)
    print("2")
end