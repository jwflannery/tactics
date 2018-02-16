using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitInfoTextController : MonoBehaviour {

    public Text unitInfoText;
    private string unitHealth = "";
    private string unitTeam = "";

    public void UpdateUnitInfo() 
    {
        if (GameManager.instance.FindUnitOnTile(MoveCursor.instance.currentGridX, MoveCursor.instance.currentGridY) != null)
        {
            GameObject unit = GameManager.instance.FindUnitOnTile(MoveCursor.instance.currentGridX, MoveCursor.instance.currentGridY);
            unitHealth = "Health: " + unit.GetComponent<UnitDetails>().Health;

            Team correctTeam = GameManager.instance.AllTeams.Find(x => x.teamNumber == unit.GetComponent<UnitDetails>().TeamNumber);
            unitTeam = "Team: " + correctTeam.teamName;
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
