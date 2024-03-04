modifier_test = class({})

local test = 5

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

    print("created")
    --print(params_table)
    test = 10
    --print(params_table[1])
    --print(params_table)
end

function modifier_test:RemoveOnDeath()
    return true
end

function modifier_test:OnDeath()
    print("removed")
end

function modifier_test:OnDestroy()
    print("destroyed")
end