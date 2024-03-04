custom_blink = class({})

function custom_blink:OnSpellStart()
    local caster = self:GetCaster()
    local point = self:GetCursorPosition()
    --FindClearSpaceForUnit(caster, point, true)
    print(point.X)
    CreateUnitByName("custom_barracks", Vector(-100, 1300, 0), true, nil, nil, DOTA_TEAM_GOODGUYS)
    print("2")
end