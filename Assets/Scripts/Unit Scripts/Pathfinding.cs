using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CreativeSpore.SuperTilemapEditor;

public class Pathfinding : MonoBehaviour
{
    public int startGridX;
    public int startGridY;

    private float interpolationSpeed = 2.50F;
    private int range = 5;
    private Vector2 nextLocation;
    private PlayerUnit unit;

    private int currentGridX;
    private int currentGridY;

    private Vector2 targetLocation;
    private Tile lastCursorTile;
    private Tile currentCursorTile;
    private int targetGridX;
    private int targetGridY;

    private List<Node> openTiles = new List<Node>();
    private List<Node> closedTiles = new List<Node>();
    private List<Node> pathTiles = new List<Node>();

    private List<Transform> existingMoveTiles = new List<Transform>();
    private List<Transform> existingPathTiles = new List<Transform>();
    private Stack<Node> pathToTarget = new Stack<Node>();

    public GameObject moveTile;
    public GameObject pathTile;
    public STETilemap foreGround;
    public class Node
    {
        public Vector2 position;
        public Node parent;
        public int cost;

        public Node(Vector2 Pos, Node Parent, int Cost)
        {
            position = Pos;
            parent = Parent;
            cost = Cost;
        }
    }

    private void Start()
    {
        unit = GetComponent<PlayerUnit>();
        targetGridX = 1;
        targetGridY = 1;
        currentGridX = startGridX;
        currentGridY = startGridY;
        targetLocation = TilemapUtils.GetGridWorldPos(MoveCursor.instance.ground, currentGridX, currentGridY);
        //transform.position = TilemapUtils.GetGridWorldPos(MoveCursor.instance.ground, startGridX, startGridY);
        lastCursorTile = MoveCursor.instance.currentTile;
    }

    private void MoveAlongPath()
    {
        if (pathToTarget.Count >= 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextLocation, 1 * Time.deltaTime);
            if (Vector2.Distance(transform.position, nextLocation) < Mathf.Epsilon && pathToTarget.Count > 0)
            {
                var nextTile = pathToTarget.Pop();
                nextLocation = TilemapUtils.GetGridWorldPos(MoveCursor.instance.ground, (int)nextTile.position.x, (int)nextTile.position.y);
            }
            else if (Vector2.Distance(transform.position, nextLocation) < Mathf.Epsilon && pathToTarget.Count == 0)
                unit.state = PlayerUnit.States.waiting;
        }
    }

    public void EnterState()
    {
        //findReachableTiles();
        foreach (Node reachableTile in closedTiles)
        {
            createMoveTile(TilemapUtils.GetGridWorldPos(MoveCursor.instance.ground, (int)reachableTile.position.x, (int)reachableTile.position.y));
        }
        openTiles.Clear();
        closedTiles.Clear();
    }

    //private void findReachableTiles()
    //{
    //    openTiles.Add(new Node(new Vector2(currentGridX, currentGridY), null, 0));
    //    addAdjacent(new Node(new Vector2(currentGridX, currentGridY), null, 0));
    //    while (openTiles.Count > 0)
    //    {
    //        addAdjacent(getMinNode());
    //    }
    //}

    //private void addAdjacent(Node centre)
    //{
    //    if (centre.cost < range && !closedTiles.Exists(t => t.position == centre.position))
    //    {
    //        var newPos = new Vector2(centre.position.x, centre.position.y - 1);
    //        var tile = foreGround.GetTile((int)newPos.x, (int)newPos.y);
    //        var impassable = tile != null && tile.collData.type != eTileCollider.None;
    //        if (!closedTiles.Exists(t => t.position == newPos) && !impassable)
    //        {
    //            openTiles.Add(new Node(newPos, centre, centre.cost + 1));
    //        }
    //        newPos = new Vector2(centre.position.x, centre.position.y + 1);
    //        tile = foreGround.GetTile((int)newPos.x, (int)newPos.y);
    //        impassable = tile != null && tile.collData.type != eTileCollider.None;
    //        if (!closedTiles.Exists(t => t.position == newPos) && !impassable)
    //        {
    //            openTiles.Add(new Node(newPos, centre, centre.cost + 1));
    //        }
    //        newPos = new Vector2(centre.position.x + 1, centre.position.y);
    //        tile = foreGround.GetTile((int)newPos.x, (int)newPos.y);
    //        impassable = tile != null && tile.collData.type != eTileCollider.None;
    //        if (!closedTiles.Exists(t => t.position == newPos) && !impassable)
    //        {
    //            openTiles.Add(new Node(newPos, centre, centre.cost + 1));
    //        }
    //        newPos = new Vector2(centre.position.x - 1, centre.position.y);
    //        tile = foreGround.GetTile((int)newPos.x, (int)newPos.y);
    //        impassable = tile != null && tile.collData.type != eTileCollider.None;
    //        if (!closedTiles.Exists(t => t.position == newPos) && !impassable)
    //        {
    //            openTiles.Add(new Node(newPos, centre, centre.cost + 1));
    //        }
    //    }
    //    closedTiles.Add(centre);
    //    openTiles.Remove(centre);
    //}

    public void deselectUnit()
    {
        GetComponent<SpriteRenderer>().color = Color.grey;
        clearTiles(existingMoveTiles);
        clearTiles(existingPathTiles);
    }

    private void clearTiles(List<Transform> tiles)
    {
        foreach (Transform tile in tiles)
        {
            GameObject.Destroy(tile.gameObject);
        }
        tiles.Clear();
    }

    private void updateTarget()
    {
        targetGridX = MoveCursor.instance.currentGridX;
        targetGridY = MoveCursor.instance.currentGridY;
        targetLocation = MoveCursor.instance.currentLocation;
    }

    private void Update()
    {
        if (unit.state == PlayerUnit.States.moving)
        {
            MoveAlongPath();
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (!existingMoveTiles.Exists(t => MoveCursor.instance.transform.position == t.position))
                return;
            updateTarget();
            deselectUnit();
            unit.state = PlayerUnit.States.moving;
        }

        //if (unit.state == PlayerUnit.States.selected)
        //{
        //    if (existingMoveTiles.Exists(t => MoveCursor.instance.transform.position == t.position))
        //    {
        //        currentCursorTile = MoveCursor.instance.currentTile;
        //        if (lastCursorTile != currentCursorTile)
        //        {
        //            clearTiles(existingPathTiles);
        //            pathToTarget.Clear();
        //        }
        //        findPathToTarget(new Vector2(currentGridX, currentGridY), new Vector2(MoveCursor.instance.currentGridX, MoveCursor.instance.currentGridY));
        //    }
        //}
    }

    private void createMoveTile(Vector3 position)
    {
        var tile = Instantiate(moveTile, position, Quaternion.identity);
        existingMoveTiles.Add(tile.transform);
    }

    private void findPathToTarget(Vector2 source, Vector2 target)
    {
        if (source == target)
            return;

        openTiles.Add(new Node(new Vector2(currentGridX, currentGridY), null, 0));

        while (openTiles.Count > 0)
        {
            //addAdjacent(getMinNode());
            if (closedTiles.Exists(t => t.position == target))
            {
                createPathTiles(closedTiles.Find(n => n.position == target));
                openTiles.Clear();
                closedTiles.Clear();
                break;
            }
        }
    }

    private Node getMinNode()
    {
        Node lowestCostNode = new Node(new Vector2(), null, 1000);
        foreach (Node node in openTiles)
        {
            if (node.cost <= lowestCostNode.cost)
            {
                lowestCostNode = node;
            }
        }
        return lowestCostNode;
    }

    private void createPathTiles(Node target)
    {
        Node current = target;
        do
        {
            pathToTarget.Push(current);
            var tile = Instantiate(pathTile, TilemapUtils.GetGridWorldPos(MoveCursor.instance.ground, (int)current.position.x, (int)current.position.y), Quaternion.identity);
            existingPathTiles.Add(tile.transform);
            current = current.parent;
        } while (current.parent != null);
        nextLocation = TilemapUtils.GetGridWorldPos(MoveCursor.instance.ground, (int)pathToTarget.Peek().position.x, (int)pathToTarget.Peek().position.y);
    }
}
