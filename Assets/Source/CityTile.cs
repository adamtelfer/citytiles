using UnityEngine;
using System.Collections;

public class CityTile : MonoBehaviour {

    public string type;
    public int row;
    public int column;

    public Economy baseEconomy;

    public virtual Economy CalculateEconomyChange(GameManager gm)
    {
        return baseEconomy;
    }

    public void Initialize(GameManager gm, GameObject root, string t, int r, int c)
    {
        baseEconomy = new Economy();
        type = t;
        row = r;
        column = c;
    }

	// Use this for initialization
	void Start () {
	
	}
}
