soldier_respawn_move_modifier = class({})

key_unit = "unit"
key_rally = "rally"

soldier_respawn_move_modifier:OnCreated(kv)
    soldier = kv[key_unit]
    rally = kv[key_rally]
    self:SetDuration(2, true)
end

soldier_respawn_modifier:OnDestroy()
    soldier.MoveToPosition(rally)
end

