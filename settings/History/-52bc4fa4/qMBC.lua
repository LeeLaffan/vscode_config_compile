barracks_set_rally = class({})

raxManager = require('RaxManager')

function barracks_set_rally:OnSpellStart()
    local caster = self:GetCaster()
    local rally = self:GetCursorPosition()

    print("CasteR: "..self:GetEntityIndex())

    raxManager.SetRally(rally, 1)
    print("rally set")
    --raxManager.SpawnUnit("johnboy")

    -- print(rally)
    -- print("rally 1"..rally[1])

    -- --local uid = CustomBarracks:GetUnitId()
    -- --print("UnitId spell "..uid)
    
    -- caster:SetContextNum("rally_x", rally[1] , 0)
    -- caster:SetContextNum("rally_y", rally[2], 0)
    -- caster:SetContextNum("rally_z", rally[3], 0)

    -- local uc = caster:GetContext("unit_count")
    -- if uc == nil then
    --     uc = 0
    -- end

    -- --local unit_count = table.getn(units_spawned)

    -- local john = CreateUnitByName("johnboy", rally, true, nil, nil, DOTA_TEAM_GOODGUYS)
    -- john:SetUnitName(john:GetUnitName().."_"..uc)
    -- print("Created unit "..john:GetUnitName())

    -- caster:SetContextNum("unit_count", uc + 1, 0)

end