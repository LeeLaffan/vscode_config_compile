barracks_spawn_soldier = class({})

LinkLuaModifier("modifier_test", LUA_MODIFIER_MOTION_NONE )

unit_count = 0 

function barracks_spawn_soldier:OnSpellStart()
    if not IsServer() then return end

    local caster = self:GetCaster()
    print(caster:GetAbsOrigin())

    local testval = self:GetContext("test_val")
    if testval == nil then
        testval = 0
    end

    if testval >= 6 then
        print("testval too big "..testval)
        return
    end

    local rally = self:GetRallyPoint()
    local john = CreateUnitByName("johnboy", rally, true, nil, nil, DOTA_TEAM_GOODGUYS)
    self:SetContextNum("test_val", testval + 1, 0)
    print("Setting test_val to "..testval + 1)

    john:AddNewModifier(caster, self, "modifier_test", params_table)

    john:SetUnitName(john:GetUnitName().."_"..testval)

    print("printing john")
    print(john:GetUnitName())
    print("dont")

    print("john HeroId: "..john:GetHeroID())
    --ListenToGameEvent('prizepool_received', OnEntityKilled, nil)

end

function OnEntityKilled(arg)
    if not IsServer() then return end
--     print("Killed")
     local killedUnit = EntIndexToHScript( arg.prizepool )
    print("ded2 ", tostring(arg == nil))
--     print(killedUnit)
end

function barracks_spawn_soldier:GetRallyPoint()
    local rally_x = caster:GetContext("rally_x")
    local rally_y = caster:GetContext("rally_y")
    local rally_z = caster:GetContext("rally_z")

    return Vector(rally_x, rally_y, rally_z)
end
