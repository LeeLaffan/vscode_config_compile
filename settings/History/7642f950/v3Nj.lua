johnboy = class({})

function Spawn(keys )
    print("JOHNBOY IS HERE")
    local npc = EntIndexToHScript(keys.entindex)

    print(npc)

end

function OnEntityKilled(keys)
    print("ent killed")
end