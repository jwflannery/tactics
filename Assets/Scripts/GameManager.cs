using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using InControl;
using CreativeSpore.SuperTilemapEditor;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance = null;
    public List<GameObject> AllUnits = new List<GameObject>();
    public List<Team> AllTeams = new List<Team>();
    public Queue<Team> TeamOrder = new Queue<Team>();
    public Stack<GameObject> UnitOrder = new Stack<GameObject>();

    private STETilemap foregroundTilemap;
    private STETilemap backgroundTilemap;
    private Team prevTeam; //TODO get rid of this once game pauses upon dialogue.

    public Team PlayerTeam = new Team(0, "Player");
    public Team EnemyTeam = new Team(1, "Enemy");
    public Team DialogueTeam = new Team(-1, "Dialogue"); //TODO get rid of this one, too.

    public static bool IsPaused = false;
    public static bool InDialogue = false;
    public static UnitStateManager CurrentlySelectedUnit = null;

    public GameObject MoveTilePrefab;
    public GameObject PathTilePrefab;
    public GameObject AttackTilePrefab;

    private TurnTextScript TurnTextScript;
    public Team CurrentActiveTeam;
    public DialogueLines DummyLines; //TODO Get rid of this one once we've got some actual dialogue.
    private DialogueHandler DialogueHandler;

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
        TurnTextScript.DisplayText(CurrentActiveTeam.TeamName);

        foreach (GameObject unit in AllUnits)
        {
            unit.GetComponent<UnitStateManager>().RefreshUnit();
        }

        foreach (GameObject unit in CurrentActiveTeam.TeamUnits)
        {
            UnitOrder.Push(unit);
        }
        if (CurrentActiveTeam == EnemyTeam)
        {
            ActivateNextEnemyUnit();
        }
        TeamOrder.Enqueue(oldTeam);
    }

    public void ActivateNextEnemyUnit()
    {
        if (UnitOrder.Count > 0)
            UnitOrder.Pop().GetComponent<UnitStateManager>().StateMachine.OnAccept();
    }

    //This whole function has gotta go.
    public void ToggleDialogue()
    {
        if (!InDialogue)
        {

            InDialogue = true;
            TogglePause();
            DialogueHandler.ReadLines(DummyLines.lines);
        }
        else if (DialogueHandler.lineQueue.Count > 0)
        {
            DialogueHandler.DisplayNextLine();
        }
        else
        {
            InDialogue = false;
            DialogueHandler.HidePanel();
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (IsPaused)
        {
            IsPaused = false;
            Time.timeScale = 1;
        }
        else if (!IsPaused)
        {
            IsPaused = true;
            Time.timeScale = 0;
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

    private void Start()
    {
        foregroundTilemap = ObjectReferences.Instance.ForegroundTilemap;
        backgroundTilemap = ObjectReferences.Instance.BackgroundTilemap;
        TurnTextScript = ObjectReferences.Instance.TurnTextScript;
        DialogueHandler = ObjectReferences.Instance.DialogueHandlerScript;
        MapDetails.InitialiseTiles(foregroundTilemap);
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleDialogue();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }

        if (GameManager.InDialogue)
        {
            if (Input.GetMouseButtonDown(0) || InputManager.ActiveDevice.Action1.WasPressed)
                ToggleDialogue();
            return;
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
            if (InputManager.ActiveDevice.Action1.WasPressed || Input.GetMouseButtonDown(0))
            {
                if (CurrentlySelectedUnit != null)
                {
                    CurrentlySelectedUnit.StateMachine.OnAccept();
                }
                else
                {
                    foreach (GameObject u in CurrentActiveTeam.TeamUnits)
                    {
                        u.GetComponent<PlayerUnitStateManager>().StateMachine.OnAccept();
                    }
                }
            }
            if (InputManager.ActiveDevice.Action2.WasPressed || Input.GetMouseButtonDown(1))
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
