using UnityEngine;
using System.Collections;
using GoogleFu;

public class CityTile : MonoBehaviour {

    public string type;
    public int row;
    public int column;

    public int cost;

    public Economy baseEconomy;

    public virtual Economy CalculateEconomyChange(GameManager gm)
    {
        return baseEconomy;
    }

    public void Initialize(GameManager gm, TileDBRow config, string t, int r, int c)
    {
        baseEconomy = new Economy();
        baseEconomy.ApplyConfig(config);
        type = t;
        row = r;
        column = c;

        cost = config._cost;
    }

	// Use this for initialization
	void Start () {
	
	}
}
