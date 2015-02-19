using UnityEngine;
using System.Collections;

public class HUDManager : MonoBehaviour {

    public TextMesh cash;
    public TextMesh jobs;
    public TextMesh population;
    public TextMesh power;
    public TextMesh profit;
    public TextMesh reputation;

    public GameManager gameManager;

	// Use this for initialization
	void Start () {
        
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
}
