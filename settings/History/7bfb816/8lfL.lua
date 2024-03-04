modifier_test = class({})

function modifier_test:OnCreated()
    print("Test modifier created")
end

function modifier_test:RemoveOnDeath()
    return true
end