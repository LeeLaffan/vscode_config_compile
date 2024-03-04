soldier_respawn_modifier_timer = class({})

raxManager = require('RaxManager')

function soldier_respawn_modifier_timer:OnCreated(kv)
    if not IsServer() then return end
    --self:SetStackCount(1)
    self:SetDuration(10, true)
    self:StartIntervalThink(1)
end

function soldier_respawn_modifier_timer:OnIntervalThink()
    if not IsServer() then return end
    print("Interval think")
    --this:Destory()
end

-- function soldier_respawn_modifier_timer:OnDestroy()
--     if not IsServer() then return end
--     print("ENDING TIMER")
--     raxManager.AllowUnitSpawn(self:GetParent():GetEntityIndex())
--     UTIL_Remove( self )
-- end

function soldier_respawn_modifier_timer:GetTexture()
    return "skeleton_king_vampiric_aura"
end