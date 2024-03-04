custom_blink = class({})

function custom_blink:OnSpellStart()
    local caster = self:GetCaster()
    local point = self:GetCursorPosition()
    --FindClearSpaceForUnit(caster, point, true)
    print(point)
    local barracks = CreateUnitByName("custom_barracks", point, true, nil, nil, DOTA_TEAM_GOODGUYS)
    Timers:CreateTimer(function() barracks:SetAbsOrigin(point) end)


    barracks:SetOwner(caster)
    barracks:SetControllableByPlayer(caster:GetPlayerID(), true)

    print("1")
    --FindClearSpaceForUnit(barracks, point, true)
    print("2")
end