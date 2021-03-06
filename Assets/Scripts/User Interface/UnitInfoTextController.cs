﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitInfoTextController : MonoBehaviour {

    private Text unitInfoText;
    private string unitHealth = "";
    private string unitTeam = "";

    private void Start()
    {
        unitInfoText = ObjectReferences.Instance.UnitInfoText;
    }

    public void UpdateUnitInfo() 
    {
        if (MapUtils.FindUnitOnTile(MoveCursor.Instance.CurrentGridX, MoveCursor.Instance.CurrentGridY) != null)
        {
            GameObject unit = MapUtils.FindUnitOnTile(MoveCursor.Instance.CurrentGridX, MoveCursor.Instance.CurrentGridY);
            unitHealth = "Health: " + unit.GetComponent<UnitDetails>().Health;

            Team correctTeam = GameManager.Instance.AllTeams.Find(x => x.TeamNumber == unit.GetComponent<UnitDetails>().TeamNumber);
            unitTeam = "Team: " + correctTeam.TeamName;
        }
        else
        {
            unitHealth = "";
            unitTeam = "";
        }
    }

    // Update is called once per frame
    void Update () {
        UpdateUnitInfo();
        unitInfoText.text = unitHealth + "\n" + unitTeam;
	}


}
