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

        --self:SetDuration(stunDur, true)
        target:AddNewModifier(caster, nil, "cool_beans", stunDur)


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

function cool_beans:GetEffectAttachType()
	return PATTACH_OVERHEAD_FOLLOW
end

--------------------------------------------------------------------------------

function cool_beans:DeclareFunctions()
	local funcs = {
		MODIFIER_PROPERTY_OVERRIDE_ANIMATION,
	}

	return funcs
end

--------------------------------------------------------------------------------

function cool_beans:GetOverrideAnimation( params )
	return ACT_DOTA_DISABLED
end

--------------------------------------------------------------------------------

function cool_beans:CheckState()
	local state = {
	[MODIFIER_STATE_STUNNED] = true,
	}

	return state
end
