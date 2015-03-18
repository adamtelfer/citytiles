using UnityEngine;
using System.Collections;
using GoogleFu;

public class GameManager : MonoBehaviour {

    public int maxMapWidth = 18;
    public int maxMapHeight = 12;

    public float cellSize = 2f;

    public int cash;
    public Economy currentEconomy;
    public Economy baseEconomy;

    private string _currentSelectedTile;
    public string currentSelectedTile
    {
        get
        {
            return _currentSelectedTile;
        }
    }
    public delegate void SelectedTileUpdated(string newTile);
    public SelectedTileUpdated tileUpdateDelegate;
    public void SelectTile(string TileName)
    {
        //Debug.Log("Select Tile: " + TileName);
        _currentSelectedTile = TileName;
        if (tileUpdateDelegate != null)
        {
            tileUpdateDelegate(TileName);
        }
    }

    private CityTile _currentAddingTile;
    public CityTile currentAddingTile
    {
        get
        { return _currentAddingTile; }
    }

    public static GoogleFu.ConstDBRow Constants
    {
        get { return GoogleFu.ConstDB.Instance.GetRow(GoogleFu.ConstDB.rowIds.GameConfig); }
    }

    public GameObject TilePool;

    public static string RESIDENTIAL_TILE = "RES";
    public static string TOWNHALL_TILE = "TH";
    public static string CONVSTORE_TILE = "CNV";
    public static string INDUSTRIAL_TILE = "IND";

    public Vector3 ConvertRCtoV3(int r, int c)
    {
        Vector3 localPosition = new Vector3((c-1) * cellSize - (cellSize/2f), (r-1) * -(cellSize) - (cellSize/2f));
        return localPosition;
    }

    public Vector2 ConvertV3toRC(Vector3 position)
    {
        float row = -Mathf.FloorToInt(position.y / cellSize);
        float col = Mathf.FloorToInt(position.x / cellSize);
        return new Vector2(row, col+2f);
    }

    public string GetTileName(int r, int c)
    {
        return string.Format("{0},{1}", r, c);
    }

    public string GetTileName(Vector2 rowColumnVector)
    {
        return GetTileName((int)rowColumnVector.x, (int)rowColumnVector.y);
    }

    private bool _removeTile(int r, int c)
    {
        CityTile ctile = null;
        if (TryGetTileAt(r, c, ref ctile))
        {
            GameObject go = ctile.gameObject;
            go.transform.parent = null;
            Destroy(go);
            RefreshEconomy();
            return true;
        }
        return false;
    }

    private GameObject _addTileToLocation(int r, int c, string type)
    {
        TileDBRow tileDBEntry = TileDB.Instance.GetRow(type);
        DebugUtils.Assert(tileDBEntry != null, "No Tile DB Entry Found for " + type);

        Transform child = TilePool.transform.FindChild(tileDBEntry._prefab);
        DebugUtils.Assert(child != null, "Cannot find " + type + " in TilePool");

        GameObject tilePrefab = child.gameObject;
        GameObject newTile = (GameObject)GameObject.Instantiate(tilePrefab);
        newTile.name = GetTileName(r, c);
        CityTile tileComponent = newTile.GetComponent<CityTile>();
        tileComponent.Initialize(this, tileDBEntry, type, r, c);
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

    public bool CheckForNeighbourTiles (int r, int c)
    {
        CityTile nTile = null;
        if (TryGetTileAt(r + 1, c, ref nTile)) { return true; }
        if (TryGetTileAt(r - 1, c, ref nTile)) { return true; }
        if (TryGetTileAt(r, c + 1, ref nTile)) { return true; }
        if (TryGetTileAt(r, c - 1, ref nTile)) { return true; }
        return false;
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
        cash = Constants._initialcash;

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

        SelectTile(RESIDENTIAL_TILE);

        _currentAddingTile = null;
    }


    // Use this for initialization
	void Start () {
        DebugUtils.Assert(TilePool != null, "Tile Pool does not exist");
        DebugUtils.Assert(this.maxMapHeight > 0, "maxMapHeight is < 1");
        DebugUtils.Assert(this.maxMapWidth > 0, "maxMapWidth is < 1");

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

    public void OnEndTurn()
    {
        Debug.Log("End Turn");

        RefreshEconomy();
        
        cash += currentEconomy.profit;

        _currentAddingTile = null;
    }

    public bool CanAddSelectedTileToLocation(int r, int c)
    {
        CityTile tile = null;
        return CheckForNeighbourTiles(r, c) && !TryGetTileAt(r,c,ref tile);
    }

    public bool AddSelectedTileToLocation(Vector3 localPosition)
    {
        if (_currentAddingTile != null)
        {
            _removeTile(_currentAddingTile.row, _currentAddingTile.column);
            _currentAddingTile = null;
        }

        Vector2 rowColumn = ConvertV3toRC(localPosition);
        int row = (int)rowColumn.x;
        int col = (int)rowColumn.y;

        if (CanAddSelectedTileToLocation(row, col))
        {
            GameObject newTile = _addTileToLocation(row, col, _currentSelectedTile);
            _currentAddingTile = newTile.GetComponent<CityTile>();
            RefreshEconomy();
            return true;
        }
        else
        {
            return false;
        }
    }
}
