cool_beans = class({})

function cool_beans:OnSpellStart()
    local target = self:GetCursorTarget()
    local caster = self:GetCaster()

    if target ~= nil then
        local damageTable = {
            victim = target,
            attacker = caster,
            damage = 500,
            damage_type = DAMAGE_TYPE_PURE,
            damage_flags = DOTA_DAMAGE_FLAG_NONE, --Optional.
            ability = playerHero:GetAbilityByIndex(0), --Optional.
        }

ApplyDamage(damageTable)


    end

end