barracks_set_rally = class({})

function barracks_set_rally:OnSpellStart()
    local caster = self:GetCaster()
    local point = self:GetCursorPosition()

    print(point)
    print("POINT 1"..point[1])

    caster:SetContextNum("rally_x", point[1], 0)
end