using UnityEngine;
using System.Collections.Generic;

public class CityTileNeighbourBonus : CityTile {

    public List<string> neighbourTypes;
    public Economy.Types effectType = Economy.Types.PROFIT;
    public int effectAmount = 1;

    public override Economy CalculateEconomyChange(GameManager gm)
    {
        System.Collections.Generic.List<CityTile> neighbours = gm.GetNeighbourTiles(this);
        Economy calcEconomy = new Economy();
        calcEconomy = calcEconomy + baseEconomy;
        int tilesMatchingCondition = 0;
        foreach (CityTile t in neighbours)
        {
            foreach (string conditionType in neighbourTypes)
            {
                if (t.type.Equals(conditionType))
                {
                    ++tilesMatchingCondition;
                    break;
                }
            }
        }

        calcEconomy.AddValue(effectType, tilesMatchingCondition * effectAmount);

        return calcEconomy;
    }
}
