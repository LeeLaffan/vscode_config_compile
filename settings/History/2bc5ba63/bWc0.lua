soldier_respawn_modifier = class({})

-- if soldier_respawn_modifier == nil then 
--     soldier_respawn_modifier = class({})
-- end

function soldier_respawn_modifier:OnCreated(kv)
    if IsServer() then
        print("STARTING TINK")
        self:StartIntervalThink(10)
        self:SetStackCount(0)
        self:SetDuration(30, true)
    end 
end

function soldier_respawn_modifier:OnIntervalThink()
    if IsServer() then
        print("THUNK")
        self:SetStackCount(self:GetStackCount() + 1)
        self:ForceRefresh()
        print(self:GetStackCount())
        --self:Destroy()
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
        UTIL_Remove( self:GetParent() )
    end
end