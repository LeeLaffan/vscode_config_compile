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
    --print("started")
end

function modifier_test:OnCreated(params_table)
    if not IsServer() then return end
    local unit_count = self:GetAbility():GetContext("test_val")
    local caster = self:GetCaster():GetAbsOrigin()
    --print(caster)
    --print("HpDmg: "..unit_count)

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
    if not IsServer() then return end

    local unit_count = self:GetAbility():GetContext("test_val")
    self:GetAbility():SetContextNum("test_val", unit_count - 1, 0)
    --print("Setting test_val to "..unit_count - 1)

    local kv = {}
    k = "prizepool"
    kv[k] = 1

    --FireGameEvent("prizepool_received", {prizepool=1})

    --GameEvents:SendCustomGameEventToServer( "soldier_killed", { "key1" : "value1"} )
    --CustomGameEventManager:Send_ServerToAllClients("soldier_killed", self)
    --print("removed "..unit_count.."   "..tostring(isAlive))
end

function modifier_test:OnDestroy()
    --print("destroyed "..test.."   "..tostring(isAlive))
end