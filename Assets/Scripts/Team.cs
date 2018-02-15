using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Team
{
    public Team(int _teamNumber)
    {
        teamUnits = new List<GameObject>();
        active = false;
        teamNumber = _teamNumber;
        teamName = _teamNumber.ToString();
    }

    public Team(int _teamNumber, string _teamName)
    {
        teamUnits = new List<GameObject>();
        active = false;
        teamNumber = _teamNumber;
        teamName = _teamName;

    }

    public List<GameObject> teamUnits = new List<GameObject>();
    public bool active;
    public int teamNumber;
    public string teamName;
}
