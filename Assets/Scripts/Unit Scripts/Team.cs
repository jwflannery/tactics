using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Team
{
    public Team(int teamNumber)
    {
        TeamUnits = new List<GameObject>();
        IsActive = false;
        this.TeamNumber = teamNumber;
        TeamName = teamNumber.ToString();
    }

    public Team(int teamNumber, string teamName)
    {
        TeamUnits = new List<GameObject>();
        IsActive = false;
        this.TeamNumber = teamNumber;
        this.TeamName = teamName;

    }

    public List<GameObject> TeamUnits = new List<GameObject>();

    public bool IsActive;
    public int TeamNumber;
    public string TeamName;
}
