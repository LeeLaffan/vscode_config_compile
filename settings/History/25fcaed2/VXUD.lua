cool_beans = class({})

function cool_beans:OnSpellStart()
    local target = self:GetCursorTarget()
    local caster = self:GetCaster()

    if target ~= nil then
        local damageTable = {
            victim = target,
            attacker = caster,
            damage = self:GetAbilityDamage(),
            damage_type = self:GetAbilityDamageType(),
            --damage_flags = DOTA_DAMAGE_FLAG_NONE, --Optional.
            --ability = playerHero:GetAbilityByIndex(0), --Optional.
        }

        ApplyDamage(damageTable)

        local stunDur = self:GetSpecialValueFor("stun_duration")

    end

end

function cool_beans:IsDebuff()
	return true
end

--------------------------------------------------------------------------------

function cool_beans:IsStunDebuff()
	return true
end

--------------------------------------------------------------------------------

function cool_beans:GetEffectName()
	return "particles/generic_gameplay/generic_stunned.vpcf"
end