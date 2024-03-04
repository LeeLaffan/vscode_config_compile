barracks_spawn_soldier = class({})

LinkLuaModifier("modifier_test", LUA_MODIFIER_MOTION_NONE )

ListenToGameEvent('entity_killed', Dynamic_Wrap(barracks_spawn_soldier, "OnEntityKilled"), nil)

function barracks_spawn_soldier:OnSpellStart()
    if not IsServer() then return end

    local testval = self:GetContext("test_val")
    if testval == nil then
        testval = 0
    end

    if testval >= 5 then
        print("testval too big "..testval)
        return
    end

    local caster = self:GetCaster()
    local rally = self:GetRallyPoint(caster)
    local john = CreateUnitByName("johnboy", rally, true, nil, nil, DOTA_TEAM_GOODGUYS)
    john:SetUnitName(john:GetUnitName().."_"..testval)
    print("Created unit "..john:GetUnitName())

    self:SetContextNum("test_val", testval + 1, 0)
    print("Setting test_val to "..testval + 1)

    john:AddNewModifier(caster, self, "modifier_test", params_table)
end

function barracks_spawn_soldier:GetRallyPoint(caster)
    local rally_x = caster:GetContext("rally_x")
    local rally_y = caster:GetContext("rally_y")
    local rally_z = caster:GetContext("rally_z")

    return Vector(rally_x, rally_y, rally_z)
end

function barracks_spawn_soldier:OnEntityKilled(args)
    print("Bar Entity Killed")
end
