modifier_test = class({})

isAlive = false
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

    print("createdTEST "..test)
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
    print("removed "..test.."   "..isAlive)
    test = test - 1
end

function modifier_test:OnDestroy()
    print("destroyed "..test.."   "..isAlive)
end