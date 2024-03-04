barracks_spawn_soldier = class({})

LinkLuaModifier("modifier_test", LUA_MODIFIER_MOTION_NONE )

unit_count = 0 

function barracks_spawn_soldier:OnSpellStart()
    local caster = self:GetCaster()
    print(caster:GetAbsOrigin())

    local rally_x = caster:GetContext("rally_x")
    local rally_y = caster:GetContext("rally_y")
    local rally_z = caster:GetContext("rally_z")

    local rally = Vector(rally_x, rally_y, rally_z)

    print(rally)
    print("creating the john")
    local john = CreateUnitByName("johnboy", rally, true, nil, nil, DOTA_TEAM_GOODGUYS)

    local params_table = {}
    params_table[1] = 5

    self:SetContextNum("test_val", 15, 0)

    john:AddNewModifier(caster, self, "modifier_test", params_table)

end
