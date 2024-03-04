barracks_set_rally = class({})

function barracks_set_rally:OnSpellStart()
    local caster = self:GetCaster()
    local point = self:GetCursorPosition()

    print("POINT "..point)
    print("POINT 1"..point[1])

    caster:SetContext("rally_x", point, 0)
end