custom_blink = class({})

function custom_blink:OnSpellStart()
    local caster = self:GetCaster()
    local point = self:GetCursorPosition()
    --FindClearSpaceForUnit(caster, point, true)
    print("1")
    CreateUnitByName("custom_barracks", point, true, nil, nil, DOTA_TEAM_BADGUYS)
    print("2")
end