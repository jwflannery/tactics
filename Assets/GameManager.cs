using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager i = null;
    public List<GameObject> units = new List<GameObject>();


    private void Awake()
    {
        if (i == null)
        {
            i = this;
        }
    }
	
	// Update is called once per frame
	void Update () {
		if (!units.Exists(u => u.GetComponent<Pathfinding>().exhausted == false))
        {
            foreach (GameObject unit in units)
            {
                unit.GetComponent<Pathfinding>().exhausted = false;
                unit.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
	}
}
