using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
		if (!units.Exists(u => u.GetComponent<Unit>().state != Unit.States.exhausted))
        {
            foreach (GameObject unit in units)
            {
                unit.GetComponent<Unit>().state = Unit.States.fresh;
                unit.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
	}
}
