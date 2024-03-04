barracks_set_rally = class({})

function barracks_set_rally:OnSpellStart()
    local caster = self:GetCaster()
    local point = self:GetCursorPosition()
    print(point.X)

    caster:SetContext("rally_x", point.X, 0)
end