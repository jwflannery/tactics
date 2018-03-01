using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitDetails : MonoBehaviour
{
    #region Variable Declarations
    protected int health = 100;
    protected int damage = 25;
    protected int attackRange = 1;
    protected int moveRange = 5;
    protected int currentGridX;
    protected int currentGridY;


    protected TextMeshPro healthText;
    protected GameObject unitTilemap;
    protected LayerMask unitLayerMask;
    protected Animator animator;

    protected Team team;
    public int TeamNumber = 0;

    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public int CurrentGridX
    {
        get { return currentGridX; }
        set { currentGridX = value; }
    }

    public int CurrentGridY
    {
        get { return currentGridY; }
        set { currentGridY = value; }
    }

    public int MoveRange
    {
        get { return moveRange; }
        set { moveRange = value; }
    }

    public int AttackRange
    {
        get { return attackRange; }
        set { attackRange = value; }
    }

    #endregion
    // Use this for initialization
    protected virtual void Start()
    {
        healthText = GetComponentInChildren<TextMeshPro>();
        animator = GetComponent<Animator>();

        unitTilemap = ObjectReferences.Instance.UnitTilemap;
        unitLayerMask = LayerMask.GetMask("Units");
        GameManager.Instance.AllUnits.Add(gameObject);
        GameManager.Instance.AddUnitToTeam(transform.gameObject, TeamNumber);

        CurrentGridX = MapUtils.GetGridX(transform.position);
        CurrentGridY = MapUtils.GetGridY(transform.position);

        team = GameManager.Instance.GetTeamByNumber(TeamNumber);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        healthText.text = Health.ToString();
        if (Health <= 0)
        {
            GameManager.Instance.AllUnits.Remove(gameObject);
            team.TeamUnits.Remove(transform.gameObject);

            GameObject.Destroy(transform.gameObject);
        }
    }
}
