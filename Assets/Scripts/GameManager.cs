using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance = null;
    public List<GameObject> AllUnits = new List<GameObject>();
    public List<Team> AllTeams = new List<Team>();
    public Queue<Team> TeamOrder = new Queue<Team>();
    public Stack<GameObject> UnitOrder = new Stack<GameObject>();
    private Team prevTeam; //TODO get rid of this once game pauses upon dialogue.

    public Team PlayerTeam = new Team(0, "Player");
    public Team EnemyTeam = new Team(1, "Enemy");
    public Team DialogueTeam = new Team(-1, "Dialogue"); //TODO get rid of this one, too.

    public GameObject MoveTilePrefab;
    public GameObject PathTilePrefab;
    public GameObject AttackTilePrefab;

    public TurnTextScript TurnText;
    public Team CurrentActiveTeam;
    public DialogueLines DummyLines; //TODO Get rid of this one once we've got some actual dialogue.
    public DialogueHandler DialogueHandler;
          
    public void AddUnitToTeam(GameObject unit, int teamNumber)
    {
        GetTeamByNumber(teamNumber).TeamUnits.Add(unit);
    }

    public Team GetTeamByName(string teamName)
    {
        return AllTeams.Find(x => x.TeamName.Equals(teamName));
    }

    public Team GetTeamByNumber(int teamNumber)
    {
        return AllTeams.Find(x => x.TeamNumber == teamNumber);
    }

    public void RefreshNextTeam(Team oldTeam)
    {
        UnitOrder.Clear();
        CurrentActiveTeam = TeamOrder.Dequeue();
        TurnText.DisplayText(CurrentActiveTeam.TeamName);

        foreach (GameObject unit in AllUnits)
        {
            unit.GetComponent<UnitStateManager>().RefreshUnit();
            UnitOrder.Push(unit);
        }
        if (CurrentActiveTeam == EnemyTeam)
        {
            ActivateNextEnemyUnit();
        }
        TeamOrder.Enqueue(oldTeam);
    }
    
    public GameObject FindUnitOnTile(int gridX, int gridY)
    {
        GameObject unit = AllUnits.Find(x => x.GetComponent<UnitDetails>().CurrentGridX == gridX && x.GetComponent<UnitDetails>().CurrentGridY == gridY);
        return unit;
    }

    public void ActivateNextEnemyUnit()
    {
        if (UnitOrder.Count > 0)
            UnitOrder.Pop().GetComponent<UnitStateManager>().StateMachine.OnAccept();
    }

    //This whole function has gotta go.
    public void ToggleDialogue()
    {
        if (CurrentActiveTeam != DialogueTeam)
        {
            Debug.Log("Entering Dialogue.");
            DialogueHandler.ReadLines(DummyLines.lines);
            prevTeam = CurrentActiveTeam;
            CurrentActiveTeam = DialogueTeam;
        }
        else if (DialogueHandler.lineQueue.Count > 0)
        {
            DialogueHandler.DisplayNextLine();
        }
        else
        {
            Debug.Log("Exiting Dialogue");
            DialogueHandler.HidePanel();
            CurrentActiveTeam = prevTeam;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        AllTeams.Add(PlayerTeam);
        AllTeams.Add(EnemyTeam);

        CurrentActiveTeam = PlayerTeam;
        TeamOrder.Enqueue(EnemyTeam);
    }

    void Update () {

        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleDialogue();
        }

        if (PlayerTeam.TeamUnits.Count <= 0 || EnemyTeam.TeamUnits.Count <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

		if (!CurrentActiveTeam.TeamUnits.Exists(u => !u.GetComponent<UnitStateManager>().StateMachine.TopState.GetType().IsSubclassOf(typeof(UnitExhaustedState))))
        {
            if (CurrentActiveTeam != DialogueTeam)
                RefreshNextTeam(CurrentActiveTeam);
        }

        if (CurrentActiveTeam == PlayerTeam)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (CurrentActiveTeam.TeamUnits.Exists(x => x.GetComponent<PlayerUnitStateManager>().Active))
                {
                    foreach (GameObject u in CurrentActiveTeam.TeamUnits.FindAll(x => x.GetComponent<PlayerUnitStateManager>().Active))
                    {
                        u.GetComponent<PlayerUnitStateManager>().StateMachine.OnAccept();
                    }
                }
                else
                {
                    foreach (GameObject u in CurrentActiveTeam.TeamUnits)
                    {
                        u.GetComponent<PlayerUnitStateManager>().StateMachine.OnAccept();
                    }
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                if (CurrentActiveTeam.TeamUnits.Exists(x => x.GetComponent<PlayerUnitStateManager>().Active))
                {
                    foreach (GameObject u in CurrentActiveTeam.TeamUnits.FindAll(x => x.GetComponent<PlayerUnitStateManager>().Active))
                    {
                        u.GetComponent<PlayerUnitStateManager>().StateMachine.OnCancel();
                    }
                }
                else
                {
                    foreach (GameObject u in CurrentActiveTeam.TeamUnits)
                    {
                        u.GetComponent<PlayerUnitStateManager>().StateMachine.OnCancel();
                    }
                }
            }
        }

        else if (CurrentActiveTeam == EnemyTeam)
        {            
            foreach (GameObject unit in EnemyTeam.TeamUnits)
            {
                unit.GetComponent<EnemyUnitStateManager>();
            }
        }
	}
}
