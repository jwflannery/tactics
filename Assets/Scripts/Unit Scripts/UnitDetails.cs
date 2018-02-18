using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CreativeSpore.SuperTilemapEditor;

public class UnitDetails : MonoBehaviour
{
    public enum States
    {
        fresh = 0,
        selected = 1,
        moving = 2,
        waiting = 3,
        exhausted = 4,
        attacking = 5
    };

    #region Variable Declarations
    protected int health = 100;
    protected int damage = 25;
    protected int attackRange = 1;
    protected int moveRange = 5;
    protected int currentGridX;
    protected int currentGridY;
    protected Tile currentTile;
    protected TextMeshPro text;
    protected STETilemap unitTilemap;
    protected LayerMask unitLayerMask;
    protected Animator animator;

    public States state = 0;

    protected Team team;
    public int TeamNumber = 0;

    public int Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
        }
    }

    public int Damage
    {
        get
        {
            return damage;
        }

        set
        {
            damage = value;
        }
    }

    public int startGridX = 1;
    public int startGridY = 1;

    public int CurrentGridX
    {
        get
        {
            return currentGridX;
        }

        set
        {
            currentGridX = value;
        }
    }

    public int CurrentGridY
    {
        get
        {
            return currentGridY;
        }

        set
        {
            currentGridY = value;
        }
    }

    public int MoveRange
    {
        get
        {
            return moveRange;
        }

        set
        {
            moveRange = value;
        }
    }

    public int AttackRange
    {
        get
        {
            return attackRange;
        }

        set
        {
            attackRange = value;
        }
    }

    #endregion
    // Use this for initialization
    protected virtual void Start()
    {
        text = GetComponentInChildren<TextMeshPro>();
        animator = GetComponent<Animator>();

        unitTilemap = transform.parent.gameObject.GetComponent<STETilemap>();
        unitLayerMask = LayerMask.GetMask("Units");
        GameManager.instance.AllUnits.Add(gameObject);
        GameManager.instance.AddUnitToTeam(transform.gameObject, TeamNumber);

        CurrentGridX = TilemapUtils.GetGridX(unitTilemap, transform.position); ;
        CurrentGridY = TilemapUtils.GetGridY(unitTilemap, transform.position); ;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        text.text = Health.ToString();
        if (Health <= 0)
        {
            GameManager.instance.AllUnits.Remove(gameObject);
            //TODO just get the dang team at the start, instance of calling it all the time.
            Team correctTeam = GameManager.instance.AllTeams.Find(x => x.TeamNumber == TeamNumber);
            correctTeam.TeamUnits.Remove(transform.gameObject);

            GameObject.Destroy(transform.gameObject);
        }
    }

    public GameObject GetUnitOnTile(Vector2 transform)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform, Vector2.one, 0);

        if (colliders.Length > 0)
        {
            return colliders[0].gameObject;
        }

        return null;
    }
}
