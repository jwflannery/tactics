using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CreativeSpore.SuperTilemapEditor;

public class PlayerUnit : MonoBehaviour {
    #region Variable Declarations
    private int health = 100;
    private int damage = 25;
    private int attackRange = 1;
    private int moveRange = 5;
    private int currentGridX;
    private int currentGridY;
    private Tile currentTile;
    private TextMeshPro text;
    private STETilemap unitTilemap;
    private LayerMask unitLayerMask;

    public enum States
    {
        fresh = 0,
        selected = 1,
        moving = 2,
        waiting = 3,
        exhausted = 4,
        attacking = 5
    };
    public States state = 0;

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
    void Start () {
        text = GetComponentInChildren<TextMeshPro>();
        unitTilemap = transform.parent.gameObject.GetComponent<STETilemap>();
        unitLayerMask = LayerMask.GetMask("Units");
        GameManager.instance.units.Add(gameObject);
        CurrentGridX = startGridX;
        CurrentGridY = startGridY;
        transform.position = TilemapUtils.GetGridWorldPos(MoveCursor.instance.ground, startGridX, startGridY);
    }
	
	// Update is called once per frame
	void Update () {
        text.text = Health.ToString();
    }


    GameObject GetUnitOnTile(Vector2 transform)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform, Vector2.one, 0);

        if (colliders.Length > 0)
        {
            return colliders[0].gameObject;
        }

        return null;
    }
}
