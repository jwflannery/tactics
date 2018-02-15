using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreativeSpore.SuperTilemapEditor;

public class UnitPathingState : UnitState
{

    public GameObject moveTile;
    protected List<Node> openTiles = new List<Node>();
    protected List<Node> closedTiles = new List<Node>();
    protected Stack<Node> pathToTarget = new Stack<Node>();
    protected Vector2 initialPosition = new Vector2();

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

    public override void OnEnter()
    {
        Machine.actor.GetComponent<UnitStateManager>().Active = true;
        moveTile = GameManager.instance.moveTilePrefab;
        unitDetails = Machine.actor.GetComponent<PlayerUnit>();
        unitTilemap = Machine.actor.transform.parent.GetComponent<STETilemap>();
        findReachableTiles();
        openTiles.Clear();
        closedTiles.Clear();
    }

    public override IEnumerator Tick()
    {
        return base.Tick();
    }

    protected virtual void findPathToTarget(Vector2 source, Vector2 target)
    {
        if (source == target)
        {
            pathToTarget.Push(new Node(source, null, 0));
            return;
        }

        openTiles.Add(new Node(new Vector2(unitDetails.CurrentGridX, unitDetails.CurrentGridY), null, 0));

        while (openTiles.Count > 0)
        {
            addAdjacent(getMinNode());
            if (closedTiles.Exists(t => t.position == target))
            {
                openTiles.Clear();
                closedTiles.Clear();
                break;
            }
        }
    }


    protected void findReachableTiles()
    {
        openTiles.Add(new Node(new Vector2(unitDetails.CurrentGridX, unitDetails.CurrentGridY), null, 0));
        addAdjacent(new Node(new Vector2(unitDetails.CurrentGridX, unitDetails.CurrentGridY), null, 0));
        while (openTiles.Count > 0)
        {
            addAdjacent(getMinNode());
        }
    }

    protected void addAdjacent(Node centre)
    {
        if (centre.cost < unitDetails.MoveRange && !closedTiles.Exists(t => t.position == centre.position))
        {
            var newPos = new Vector2(centre.position.x, centre.position.y - 1);
            var tile = unitTilemap.GetTile((int)newPos.x, (int)newPos.y);
            var impassable = (tile != null && tile.collData.type != eTileCollider.None || GameManager.instance.FindUnitOnTile((int)newPos.x, (int)newPos.y));
            if (!closedTiles.Exists(t => t.position == newPos) && !impassable)
            {
                openTiles.Add(new Node(newPos, centre, centre.cost + 1));
            }
            newPos = new Vector2(centre.position.x, centre.position.y + 1);
            tile = unitTilemap.GetTile((int)newPos.x, (int)newPos.y);
            impassable = (tile != null && tile.collData.type != eTileCollider.None || GameManager.instance.FindUnitOnTile((int)newPos.x, (int)newPos.y));
            if (!closedTiles.Exists(t => t.position == newPos) && !impassable)
            {
                openTiles.Add(new Node(newPos, centre, centre.cost + 1));
            }
            newPos = new Vector2(centre.position.x + 1, centre.position.y);
            tile = unitTilemap.GetTile((int)newPos.x, (int)newPos.y);
            impassable = (tile != null && tile.collData.type != eTileCollider.None || GameManager.instance.FindUnitOnTile((int)newPos.x, (int)newPos.y));
            if (!closedTiles.Exists(t => t.position == newPos) && !impassable)
            {
                openTiles.Add(new Node(newPos, centre, centre.cost + 1));
            }
            newPos = new Vector2(centre.position.x - 1, centre.position.y);
            tile = unitTilemap.GetTile((int)newPos.x, (int)newPos.y);
            impassable = (tile != null && tile.collData.type != eTileCollider.None || GameManager.instance.FindUnitOnTile((int)newPos.x, (int)newPos.y));
            if (!closedTiles.Exists(t => t.position == newPos) && !impassable)
            {
                openTiles.Add(new Node(newPos, centre, centre.cost + 1));
            }
        }
        closedTiles.Add(centre);
        openTiles.Remove(centre);
    }

    protected Node getMinNode()
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

    public override void OnAcceptInput()
    {
        base.OnAcceptInput();
        Machine.Push(new PlayerUnitMovingState(pathToTarget));
    }
    public override void OnExit()
    {
        base.OnExit();
        openTiles.Clear();
        closedTiles.Clear();
        Machine.actor.GetComponent<UnitStateManager>().Active = false;
    }

    public override void OnCancelInput()
    {
        base.OnCancelInput();
        Machine.ReplaceTop(new UnitFreshState());
    }

    public override void OnPaused()
    {
        initialPosition = unitDetails.transform.position;
        return;
    }

    public override void OnUnpaused()
    {
        unitDetails.transform.position = initialPosition;
        OnEnter();
    }
}
