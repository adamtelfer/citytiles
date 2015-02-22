using UnityEngine;
using System.Collections;

public class HUDManager : MonoBehaviour {

    public TextMesh cash;
    public TextMesh jobs;
    public TextMesh population;
    public TextMesh power;
    public TextMesh profit;
    public TextMesh reputation;

    public GameObject selectedIcon;

    public GameObject tiles;

    public GameManager gameManager;

	// Use this for initialization
	void Start () {
        DebugUtils.Assert(gameManager != null, "GameManager not attached to HUDManager");
        DebugUtils.Assert(tiles != null, "Tiles List not attached to HUDManager");
        DebugUtils.Assert(selectedIcon != null, "SelectedIcon not attached to HUDManager");

        gameManager.tileUpdateDelegate += new GameManager.SelectedTileUpdated(this.SelectedTileUpdated);
	}

	// Update is called once per frame
	void Update () {
        cash.text = "Cash: $" + gameManager.cash;
        profit.text = "Daily: $" + gameManager.currentEconomy.profit;
        jobs.text = "Jobs: " + gameManager.currentEconomy.jobs;
        population.text = "Pop: " + gameManager.currentEconomy.population;
        power.text = "Power: " + gameManager.currentEconomy.power;
        reputation.text = "Rep: " + gameManager.currentEconomy.reputation;
	}

    void OnTap(TapGesture gesture) {
       Collider2D collision = gesture.Raycast.Hit2D.collider;
       if (collision == null) return;
       if (collision.gameObject == null) return;

       Debug.Log("Tapping Detected on Button: "+collision.gameObject.name);
       Debug.Log("Tapping Selected on :" + gesture.Selection.name);
       GameObject button = collision.gameObject;

       if (button.name.Equals("EndTurn")) { gameManager.OnEndTurn(); }
       else if (button.name.StartsWith("SELECT_")) { gameManager.SelectTile(button.name.Substring("SELECT_".Length)); }
       else if (button.name.Equals("World")) {
           Vector3 worldPosition = Camera.main.ScreenToWorldPoint(gesture.Position);
           Vector3 localPosition = button.transform.worldToLocalMatrix.MultiplyPoint3x4(worldPosition);
           gameManager.AddSelectedTileToLocation(localPosition); 
       }

        
    }

    void SelectedTileUpdated(string newTile)
    {
       //Debug.Log("Updating Selected Tile");
        selectedIcon.transform.position = tiles.transform.Find("SELECT_" + newTile).transform.position;
    }
}
