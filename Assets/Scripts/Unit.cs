//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using TMPro;
//using CreativeSpore.SuperTilemapEditor;
//using ;

//public class Unit : MonoBehaviour {

//    private int health = 100;
//    private int damage = 25;
//    private int range = 1;
//    private int currentGridX;
//    private int currentGridY;
//    private Tile currentTile;
//    private TextMeshPro text;
//    private ;
//    private STETilemap unitMapLayer;
//    private LayerMask unitLayerMask;

//    public enum States
//    {
//        fresh = 0,
//        selected = 1,
//        moving = 2,
//        waiting = 3,
//        exhausted = 4,
//        attacking = 5
//    };
//    public States state = 0;

//    public int Health
//    {
//        get
//        {
//            return health;
//        }

//        set
//        {
//            health = value;
//        }
//    }

//    public int Damage
//    {
//        get
//        {
//            return damage;
//        }

//        set
//        {
//            damage = value;
//        }
//    }

//    public int CurrentGridX
//    {
//        get
//        {
//            return currentGridX;
//        }

//        set
//        {
//            currentGridX = value;
//        }
//    }

//    public int CurrentGridY
//    {
//        get
//        {
//            return currentGridY;
//        }

//        set
//        {
//            currentGridY = value;
//        }
//    }

//    // Use this for initialization
//    void Start () {
//        text = GetComponentInChildren<TextMeshPro>();
//        pathfinding = GetComponent<Pathfinding>();
//        unitMapLayer = transform.parent.gameObject.GetComponent<STETilemap>();
//        unitLayerMask = LayerMask.GetMask("Units");
//    }
	
//	// Update is called once per frame
//	void Update () {
//        CurrentGridX = TilemapUtils.GetGridX(unitMapLayer, transform.position);
//        currentGridY = TilemapUtils.GetGridY(unitMapLayer, transform.position);
//        text.text = Health.ToString();

//        if (Input.GetMouseButtonDown(1) && state == States.selected)
//        {
//            FindUnitsInRange(range, currentGridX, currentGridY);
//        }

//        if (MoveCursor.instance.transform.position == transform.position && state == States.waiting && Input.GetMouseButtonDown(0))
//        {
//            state = States.exhausted;
//        }        
//    }

//    void FindUnitsInRange(int range, int targetGridX, int targetGridY)
//    {
//        List <GameObject> units = new List<GameObject>();

//        foreach (GameObject u in GameManager.instance.units)
//        {
//            Unit unitInfo = u.GetComponent<Unit>();
//            if (unitInfo.CurrentGridX == targetGridX + 1 && unitInfo.currentGridY == targetGridY)
//            {
//                units.Add(u);
//            }
//            if (unitInfo.CurrentGridX == targetGridX - 1 && unitInfo.currentGridY == targetGridY)
//            {
//                units.Add(u);
//            }
//            if (unitInfo.CurrentGridX == targetGridX && unitInfo.currentGridY == targetGridY + 1)
//            {
//                units.Add(u);
//            }
//            if (unitInfo.CurrentGridX == targetGridX && unitInfo.currentGridY == targetGridY - 1)
//            {
//                units.Add(u);
//            }
//        }

//        if (units.Exists(u => u != null))
//        {
//            foreach (GameObject unit in units)
//            {
//                unit.GetComponent<Unit>().health -= 10;
//                state = States.exhausted;
//                pathfinding.deselectUnit();
//                Debug.Log("Adjacent unit found");
//            }
//            units.Clear();
//        }

//    }

//    GameObject GetUnitOnTile(Vector2 transform)
//    {
//        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform, Vector2.one, 0);

//        if (colliders.Length > 0)
//        {
//            return colliders[0].gameObject;
//        }

//        return null;
//    }
//}
