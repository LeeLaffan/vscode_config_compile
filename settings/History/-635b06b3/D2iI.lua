soldier_respawn_move_modifier = class({})

key_rally = "rally"

soldier_respawn_move_modifier:OnCreated(kv)
    rally = kv[key_rally]
    self:SetDuration(2, true)
end

soldier_respawn_move_modifier:OnDestroy()
    local caster = self:GetCaster()
    caster:MoveToPosition(rally)
end