soldier_respawn_modifier_timer = class({})

raxManager = require('RaxManager')

function soldier_respawn_modifier_timer:OnCreated(kv)
    if not IsServer() then return end
    self:SetDuration(15, true)
    --self:SetStackCount(1)
    self:StartIntervalThink(0)
end

function soldier_respawn_modifier_timer:OnIntervalThink()
    if not IsServer() then return end
    print("Interval think")
    --this:Destory()
end

function soldier_respawn_modifier_timer:OnDestroy()
    if not IsServer() then return end
    print("ENDING TIMER")
    raxManager.AllowUnitSpawn(self:GetParent():GetEntityIndex())
end

function soldier_respawn_modifier_timer:GetTexture()
    return "skeleton_king_vampiric_aura"
end