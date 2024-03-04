modifier_test = class({})

function modifier_test:OnCreated(params_table)
    print("Test modifier created")
    print(params_table)
end

function modifier_test:RemoveOnDeath()
    return true
end

function modifier_test:OnRemoved()
    print("removed")
end

function modifier_test:OnDestoryed()
    print("destroyed")
end