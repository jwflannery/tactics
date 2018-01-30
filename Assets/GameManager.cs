using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public List<GameObject> units = new List<GameObject>();
    public GameObject moveTilePrefab;
    public GameObject pathTilePrefab;
    public GameObject attackTilePrefab;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public GameObject FindUnitOnTile(int gridX, int gridY)
    {
        GameObject unit = units.Find(x => x.GetComponent<PlayerUnit>().CurrentGridX == gridX && x.GetComponent<PlayerUnit>().CurrentGridY == gridY);
        return unit;
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


        if (Input.GetMouseButtonDown(0))
        {
            if (units.Exists(x => x.GetComponent<UnitStateManager>().Active)) {
                foreach (GameObject u in units.FindAll(x => x.GetComponent<UnitStateManager>().Active))
                {
                    u.GetComponent<UnitStateManager>().stateMachine.OnAccept();
                }
            }
            else
            {
                foreach (GameObject u in units)
                {
                    u.GetComponent<UnitStateManager>().stateMachine.OnAccept();
                }
            }

        }
        if (Input.GetMouseButtonDown(1))
        {
            if (units.Exists(x => x.GetComponent<UnitStateManager>().Active)) {
                foreach (GameObject u in units.FindAll(x => x.GetComponent<UnitStateManager>().Active))
                {
                    u.GetComponent<UnitStateManager>().stateMachine.OnCancel();
                }
            }
            else
            {
                foreach (GameObject u in units)
                {
                    u.GetComponent<UnitStateManager>().stateMachine.OnCancel();
                }
            }
        }
	}
}
