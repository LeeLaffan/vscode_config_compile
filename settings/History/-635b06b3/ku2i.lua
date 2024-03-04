soldier_respawn_move_modifier = class({})

key_rally = "rally"

function soldier_respawn_move_modifier:OnCreated(kv)
    rally = kv[key_rally]
    print(rally)
    self:SetDuration(2, true)
end

function soldier_respawn_move_modifier:OnDestroy()
    local caster = self:GetCaster()
    print("DETROYA")
    print(rally)
    local soldier = EntIndexToHScript(caster:GetEntityIndex())

    soldier:MoveToPosition(rally)
end