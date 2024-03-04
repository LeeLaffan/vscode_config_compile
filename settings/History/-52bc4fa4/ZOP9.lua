barracks_set_rally = class({})

function barracks_set_rally:OnSpellStart()
    local caster = self:GetCaster()
    local point = self:GetCursorPosition()

    caster:SetContext("rally_x", point.X)
    print(caster:GetHealth())
end