using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CityTileConvenienceStore : CityTile
{
    public override Economy CalculateEconomyChange(GameManager gm)
    {
        System.Collections.Generic.List<CityTile> neighbours = gm.GetNeighbourTiles(this);
        Economy calcEconomy = new Economy();
        calcEconomy = calcEconomy + baseEconomy;
        foreach (CityTile t in neighbours)
        {
            if (t.type.Equals(GameManager.RESIDENTIAL_TILE))
            {
                ++calcEconomy.profit;
            }
        }

        return calcEconomy;
    }
}
