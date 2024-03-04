cool_beans = class({})

function cool_beans:OnSpellStart()
    local target = self:GetCursorTarget()
    local caster = self:GetCaster()

    if target ~= nil then

end