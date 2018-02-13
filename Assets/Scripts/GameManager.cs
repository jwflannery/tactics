using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public List<GameObject> AllUnits = new List<GameObject>();
    public List<Team> AllTeams = new List<Team>();
    public Queue<Team> teamOrder = new Queue<Team>();
    public GameObject moveTilePrefab;
    public GameObject pathTilePrefab;
    public GameObject attackTilePrefab;
    public TurnTextScript turnText;
    public Team currentActiveTeam;
    

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

    public void AddUnitToTeam(GameObject unit, int _teamNumber)
    {
        Team correctTeam = AllTeams.Find(x => x.teamNumber == _teamNumber);
        correctTeam.teamUnits.Add(unit);
    }

    public Team playerTeam = new Team(0, "Player");
    public Team enemyTeam = new Team(1, "Enemy");

    public void RefreshNextTeam(Team OldTeam)
    {
        currentActiveTeam = teamOrder.Dequeue();
        turnText.DisplayText(currentActiveTeam.teamName);
        foreach (GameObject unit in AllUnits)
        {
            unit.GetComponent<UnitStateManager>().stateMachine.ReplaceTop(new PlayerUnitFreshState());
        }
        teamOrder.Enqueue(OldTeam);

    }

    public GameObject FindUnitOnTile(int gridX, int gridY)
    {
        GameObject unit = AllUnits.Find(x => x.GetComponent<PlayerUnit>().CurrentGridX == gridX && x.GetComponent<PlayerUnit>().CurrentGridY == gridY);
        return unit;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        AllTeams.Add(playerTeam);
        AllTeams.Add(enemyTeam);

        currentActiveTeam = playerTeam;
        teamOrder.Enqueue(enemyTeam);
    }
	
	// Update is called once per frame
	void Update () {
		if (!currentActiveTeam.teamUnits.Exists(u => u.GetComponent<UnitStateManager>().stateMachine.TopState.GetType() != typeof(PlayerUnitExhaustedState)))
        {
            RefreshNextTeam(currentActiveTeam);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (currentActiveTeam.teamUnits.Exists(x => x.GetComponent<UnitStateManager>().Active)) {
                foreach (GameObject u in currentActiveTeam.teamUnits.FindAll(x => x.GetComponent<UnitStateManager>().Active))
                {
                    u.GetComponent<UnitStateManager>().stateMachine.OnAccept();
                }
            }
            else
            {
                foreach (GameObject u in currentActiveTeam.teamUnits)
                {
                    u.GetComponent<UnitStateManager>().stateMachine.OnAccept();
                }
            }

        }
        if (Input.GetMouseButtonDown(1))
        {
            if (currentActiveTeam.teamUnits.Exists(x => x.GetComponent<UnitStateManager>().Active)) {
                foreach (GameObject u in currentActiveTeam.teamUnits.FindAll(x => x.GetComponent<UnitStateManager>().Active))
                {
                    u.GetComponent<UnitStateManager>().stateMachine.OnCancel();
                }
            }
            else
            {
                foreach (GameObject u in currentActiveTeam.teamUnits)
                {
                    u.GetComponent<UnitStateManager>().stateMachine.OnCancel();
                }
            }
        }
	}
}
