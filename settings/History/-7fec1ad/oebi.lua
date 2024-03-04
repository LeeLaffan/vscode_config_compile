custom_blink = class({})

function custom_blink:OnSpellStart()
    local caster = self:GetCaster()
    local point = self:GetCursorPosition()
    --FindClearSpaceForUnit(caster, point, true)

    CreateUnitByName("johnboy", Vector(0,1300,0), true, nil, nil, DOTA_TEAM_BADGUYS)

end