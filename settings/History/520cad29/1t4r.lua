modifier_test = class({})

local isAlive = false
test = 5

function modifier_test:DeclareFunctions()
    local funcs = {
        MODIFIER_EVENT_ON_ABILITY_EXECUTED,
        MODIFIER_EVENT_ON_ABILITY_START,
        MODIFIER_EVENT_ON_DEATH
    }
    return funcs
end

function modifier_test:OnAbilityStart()
    print("started")
end

function modifier_test:OnCreated(params_table)
    if not IsServer() then
        return
    end
    local percent_health_to_damage = self:GetAbility():GetContext("test_val")
    local caster = self:GetCaster()
    print(caster)
    print("HpDmg: "..percent_health_to_damage)

    isAlive = true
    --print(params_table)
    test = test + 1
    --print(params_table[1])
    --print(params_table)
end

function modifier_test:RemoveOnDeath()
    return true
end

function modifier_test:OnDeath()
    print("removed "..test.."   "..tostring(isAlive))
    test = test - 1
end

function modifier_test:OnDestroy()
    print("destroyed "..test.."   "..tostring(isAlive))
end