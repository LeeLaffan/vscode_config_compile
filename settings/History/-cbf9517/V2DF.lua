soldier_respawn_modifier_timer = class({})

function soldier_respawn_modifier_timer:OnCreated(kv)
    if not IsServer() then return end
    self:SetStackCount(1)
    self:SetDuration()
end
