soldier_respawn_move_modifier = class({})

key_unit = "unit"

soldier_respawn_move_modifier:OnCreated(kv)
    soldier = kv[key_unit]
    self:SetDuration(1, true)
end