soldier_respawn_modifier = class({})

-- if soldier_respawn_modifier == nil then 
--     soldier_respawn_modifier = class({})
-- end

function soldier_respawn_modifier:OnCreated(kv)
    if IsServer() then
        print("STARTING TINK")
        self:StartIntervalThink(5)
    end
end

function soldier_respawn_modifier:OnIntervalThink()
    if IsServer() then
        print("THUNK")
        soldier_respawn_modifier:OnDestroy()
    end
end

function soldier_respawn_modifier:OnDestroy()
    if IsServer() then
        UTIL_Remove( self:GetParent() )
    end
end