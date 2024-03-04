modifier_cool_beans = class{{}}

function modifier_cool_beans:IsDebuff()
	return true
end

--------------------------------------------------------------------------------

function modifier_cool_beans:IsStunDebuff()
	return true
end

--------------------------------------------------------------------------------

function modifier_cool_beans:GetEffectName()
	return "particles/generic_gameplay/generic_stunned.vpcf"
end

function modifier_cool_beans:GetEffectAttachType()
	return PATTACH_OVERHEAD_FOLLOW
end

--------------------------------------------------------------------------------

function modifier_cool_beans:DeclareFunctions()
	local funcs = {
		MODIFIER_PROPERTY_OVERRIDE_ANIMATION,
	}

	return funcs
end

--------------------------------------------------------------------------------

function modifier_cool_beans:GetOverrideAnimation( params )
	return ACT_DOTA_DISABLED
end

--------------------------------------------------------------------------------

function modifier_cool_beans:CheckState()
	local state = {
	[MODIFIER_STATE_STUNNED] = true,
	}

	return state
end