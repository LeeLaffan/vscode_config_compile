cool_beans = class({})
LinkLuaModifier("modifier_cool_beans", LUA_MODIFIER_MOTION_NONE )

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
        CreateUnitByName("leeboy", Vector(-942.490173, -772.419556, 128.000000), true, nil, nil, DOTA_TEAM_BADGUYS)

        --self:SetDuration(stunDur, true)
        target:AddNewModifier(caster, self, "modifier_cool_beans", { duration = stunDur })


    end

end