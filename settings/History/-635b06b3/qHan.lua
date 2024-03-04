soldier_respawn_move_modifier = class({})

key_rally = "rally"

function soldier_respawn_move_modifier:OnCreated(kv)
    rallyStr = kv[key_rally]
    --rally = Vector(rallyStr["x"], rallyStr["y"], rallyStr["z"])
    print("recieved")
    print(kv)

    for k,v in pairs(kv) do
        print(k)
    end
    chunks = {}
    for substring in rallyStr:gmatch("%S+") do
       table.insert(chunks, substring)
    end

    rally = Vector(chunks[1], chunks[2], chunks[3])

    -- for key, val in pairs(rallyStr) do
    --     print("Key:")
    --     print(key)
    --     print("Val: ")
    --     print(val)
    -- end

    print("wahtsi[]")
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