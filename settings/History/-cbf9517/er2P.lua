soldier_respawn_modifier_timer = class({})

raxManager = require('RaxManager')

function soldier_respawn_modifier_timer:OnCreated(kv)
    if not IsServer() then return end
    self:SetStackCount(1)
    self:SetDuration()
end

function soldier_respawn_modifier_timer:OnDestroy()
    if not IsServer() then return end
    print("ENDING TIMER")
    raxManager.AllowUnitSpawn(self:GetParent():GetEntityIndex())
end