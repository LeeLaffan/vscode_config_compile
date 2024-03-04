barracks_set_rally = class({})

function barracks_set_rally:OnSpellStart()
    local caster = self:GetCaster()
    print(caster:GetHealth())
end