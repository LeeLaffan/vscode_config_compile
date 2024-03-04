soldier_respawn_modifier = class({})

-- if soldier_respawn_modifier == nil then 
--     soldier_respawn_modifier = class({})
-- end

key_rax_id = "rax_id"
key_duration = "duration"

function soldier_respawn_modifier:OnCreated(kv)
    if IsServer() then
        print(kv[key_rax_id])

        print("STARTING TINK")
        self:StartIntervalThink(1)
        self:SetStackCount(0)
        self:SetDuration(10, true)
    end 
end

function soldier_respawn_modifier:OnIntervalThink()
    if IsServer() then

    end
end

function soldier_respawn_modifier:GetTexture()
    return "meepo_divided_we_stand"
end

function soldier_respawn_modifier:GetEffectName ()
    return "particles/generic_gameplay/generic_stunned.vpcf"
end
--effect attached position the Buff effect attachment position
function soldier_respawn_modifier:GetEffectAttachType ()
    return PATTACH_OVERHEAD_FOLLOW
end

function soldier_respawn_modifier:OnDestroy()
    if IsServer() then
        print("DESTORYING SOLDIER MODIFIER")
        --UTIL_Remove( self )
    end
end