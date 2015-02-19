﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public int maxMapWidth = 18;
    public int maxMapHeight = 12;

    public float cellSize = 2f;

    public int cash;
    public Economy currentEconomy;
    public Economy baseEconomy;

    public string[] keys;
    public GameObject[] prefabs;
    public System.Collections.Generic.Dictionary<string, GameObject> prefabList;

    public static string RESIDENTIAL_TILE = "R";
    public static string TOWNHALL_TILE = "TH";
    public static string CONVSTORE_TILE = "C";

    public Vector3 ConvertRCtoV3(int r, int c)
    {
        Vector3 localPosition = new Vector3((c-1) * cellSize - (cellSize/2f), (r-1) * -(cellSize) - (cellSize/2f));
        return localPosition;
    }

    public string GetTileName(int r, int c)
    {
        return string.Format("{0},{1}", r, c);
    }

    private GameObject _addTileToLocation(int r, int c, string type)
    {
        GameObject newTile = (GameObject)GameObject.Instantiate(prefabList[type]);
        newTile.name = GetTileName(r, c);
        CityTile tileComponent = newTile.GetComponent<CityTile>();
        tileComponent.Initialize(this, prefabList[type], type, r, c);
        newTile.transform.parent = this.gameObject.transform;
        newTile.transform.localPosition = ConvertRCtoV3(r, c);
        return newTile;
    }

    public bool TryGetTileAt(int r, int c, ref CityTile t) {
        Transform tr = this.transform.Find(GetTileName(r, c));
        if (tr != null)
        {
            GameObject g = tr.gameObject;
            t = g.GetComponent<CityTile>();
            return true;
        }
        else
        {
            return false;
        }
    }

    public System.Collections.Generic.List<CityTile> GetNeighbourTiles (CityTile tile)
    {
        System.Collections.Generic.List<CityTile> list = new System.Collections.Generic.List<CityTile>(4);
        CityTile nTile = null;
        if (TryGetTileAt(tile.row + 1, tile.column, ref nTile)) { list.Add(nTile); }
        if (TryGetTileAt(tile.row - 1, tile.column, ref nTile)) { list.Add(nTile); }
        if (TryGetTileAt(tile.row, tile.column + 1, ref nTile)) { list.Add(nTile); }
        if (TryGetTileAt(tile.row, tile.column - 1, ref nTile)) { list.Add(nTile); }
        return list;
    }

    public void RefreshEconomy()
    {
        CityTile[] tileComponents = this.GetComponentsInChildren<CityTile>();
        Economy econChange = new Economy();
        foreach (CityTile tile in tileComponents)
        {
            econChange = econChange + tile.CalculateEconomyChange(this);
        }

        currentEconomy = baseEconomy + econChange;
    }

    public void InitializeState()
    {
        cash = 5000;
        currentEconomy = new Economy();
        baseEconomy = new Economy();

        for (int i = 0; i < transform.childCount; ++i ) {
            Transform t = transform.GetChild(i);
            t.parent = null;
            GameObject.Destroy(t.gameObject); 
        }

        int c = maxMapWidth / 2;
        int r = maxMapHeight / 2;

        _addTileToLocation(r, c, TOWNHALL_TILE);
        _addTileToLocation(r-1, c, RESIDENTIAL_TILE);
        _addTileToLocation(r-1, c-1, RESIDENTIAL_TILE);
        _addTileToLocation(r-1, c + 1, CONVSTORE_TILE);

        RefreshEconomy();

    }

    // Use this for initialization
	void Start () {
        prefabList = new System.Collections.Generic.Dictionary<string, GameObject>(keys.Length);
        for (int i = 0; i < keys.Length; ++i )
        {
            prefabList.Add(keys[i], prefabs[i]);
        }
        InitializeState();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.matrix = this.transform.localToWorldMatrix;
        // Horizontal Grid Lines
        for (float x = 0f; x <= maxMapWidth * cellSize; x += cellSize) {
            Gizmos.DrawLine(new Vector3(x, 0f, 0f), new Vector3(x, -1f * maxMapHeight * cellSize, 0f));
        }
        // Vertical Grid Lines
        for (float y = 0f; y <= maxMapHeight * cellSize; y += cellSize)
        {
            Gizmos.DrawLine(new Vector3(0f, -y, 0f), new Vector3(maxMapWidth * cellSize, -y, 0f));
        }

        Gizmos.matrix = Matrix4x4.identity;
    }
}