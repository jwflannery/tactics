using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public List<GameObject> units = new List<GameObject>();
    public GameObject moveTilePrefab;
    public GameObject pathTilePrefab;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
	
	// Update is called once per frame
	void Update () {
		if (!units.Exists(u => u.GetComponent<UnitStateManager>().stateMachine.TopState.GetType() != typeof(PlayerUnitExhaustedState)))
        {
            foreach (GameObject unit in units)
            {
                unit.GetComponent<UnitStateManager>().stateMachine.ReplaceTop(new PlayerUnitFreshState());
            }
        }
	}
}
