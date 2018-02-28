using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreativeSpore.SuperTilemapEditor;

public abstract class UnitPathingState : UnitState
{
    protected List<Node> openTiles = new List<Node>();
    protected List<Node> closedTiles = new List<Node>();
    protected List<Transform> existingMoveTiles = new List<Transform>();
    protected List<Transform> existingPathTiles = new List<Transform>();
    protected Stack<Vector2> pathToTarget = new Stack<Vector2>();
    protected Vector2 initialPosition = new Vector2();
    protected GameObject moveTile;

    public override void OnEnter()
    {
        base.OnEnter();
        moveTile = GameManager.Instance.MoveTilePrefab;
        Machine.Actor.GetComponent<UnitStateManager>().Active = true;
        FindReachableTiles();
    }

    public override IEnumerator Tick()
    {
        return base.Tick();
    }

    protected virtual void FindPathToTarget(Vector2 source, Vector2 target)
    {
        if (source == target)
        {
            pathToTarget.Push(unitDetails.transform.position);
            return;
        }

        openTiles.Add(new Node(new Vector2(unitDetails.CurrentGridX, unitDetails.CurrentGridY), null, 0));

        while (openTiles.Count > 0)
        {
            AddAdjacent(GetMinNode());
            if (closedTiles.Exists(t => t.Position == target))
            {
                CreatePathTiles(closedTiles.Find(n => n.Position == target));
                openTiles.Clear();
                closedTiles.Clear();
                break;
            }
        }
    }

    protected virtual void CreatePathTiles(Node target)
    {
        Node current = target;
        do
        {
            pathToTarget.Push(TilemapUtils.GetGridWorldPos(unitTilemap, (int)current.Position.x, (int)current.Position.y));
            var tile = GameObject.Instantiate(GameManager.Instance.PathTilePrefab, TilemapUtils.GetGridWorldPos(ObjectReferences.Instance.BackgroundTilemap, (int)current.Position.x, (int)current.Position.y), Quaternion.identity);
            existingPathTiles.Add(tile.transform);
            current = current.Parent;
        } while (current.Parent != null);
    }

    protected virtual void FindReachableTiles()
    {
        openTiles.Add(new Node(new Vector2(unitDetails.CurrentGridX, unitDetails.CurrentGridY), null, 0));
        AddAdjacent(new Node(new Vector2(unitDetails.CurrentGridX, unitDetails.CurrentGridY), null, 0));
        while (openTiles.Count > 0)
        {
            AddAdjacent(GetMinNode());
        }
    }

    protected void AddAdjacent(Node centre)
    {
        if (centre.Cost < unitDetails.MoveRange && !closedTiles.Exists(t => t.Position == centre.Position))
        {
            var newPos = new Vector2(centre.Position.x, centre.Position.y - 1);
            var tile = unitTilemap.GetTile((int)newPos.x, (int)newPos.y);
            var impassable = (tile != null && tile.collData.type != eTileCollider.None || GameManager.Instance.FindUnitOnTile((int)newPos.x, (int)newPos.y));
            if (!closedTiles.Exists(t => t.Position == newPos) && !impassable)
            {
                openTiles.Add(new Node(newPos, centre, centre.Cost + 1));
            }
            newPos = new Vector2(centre.Position.x, centre.Position.y + 1);
            tile = unitTilemap.GetTile((int)newPos.x, (int)newPos.y);
            impassable = (tile != null && tile.collData.type != eTileCollider.None || GameManager.Instance.FindUnitOnTile((int)newPos.x, (int)newPos.y));
            if (!closedTiles.Exists(t => t.Position == newPos) && !impassable)
            {
                openTiles.Add(new Node(newPos, centre, centre.Cost + 1));
            }
            newPos = new Vector2(centre.Position.x + 1, centre.Position.y);
            tile = unitTilemap.GetTile((int)newPos.x, (int)newPos.y);
            impassable = (tile != null && tile.collData.type != eTileCollider.None || GameManager.Instance.FindUnitOnTile((int)newPos.x, (int)newPos.y));
            if (!closedTiles.Exists(t => t.Position == newPos) && !impassable)
            {
                openTiles.Add(new Node(newPos, centre, centre.Cost + 1));
            }
            newPos = new Vector2(centre.Position.x - 1, centre.Position.y);
            tile = unitTilemap.GetTile((int)newPos.x, (int)newPos.y);
            impassable = (tile != null && tile.collData.type != eTileCollider.None || GameManager.Instance.FindUnitOnTile((int)newPos.x, (int)newPos.y));
            if (!closedTiles.Exists(t => t.Position == newPos) && !impassable)
            {
                openTiles.Add(new Node(newPos, centre, centre.Cost + 1));
            }
        }
        closedTiles.Add(centre);
        openTiles.Remove(centre);
    }

    protected void CreateMoveTile(Vector3 position)
    {
        var tile = GameObject.Instantiate(moveTile, position, Quaternion.identity);
        existingMoveTiles.Add(tile.transform);
    }

    protected Node GetMinNode()
    {
        Node lowestCostNode = new Node(new Vector2(), null, 1000);
        foreach (Node node in openTiles)
        {
            if (node.Cost <= lowestCostNode.Cost)
            {
                lowestCostNode = node;
            }
        }
        return lowestCostNode;
    }

    public override void OnAcceptInput()
    {
        base.OnAcceptInput();
    }
    public override void OnExit()
    {
        base.OnExit();
        openTiles.Clear();
        closedTiles.Clear();
        ClearTiles(existingMoveTiles);
        ClearTiles(existingPathTiles);
        Machine.Actor.GetComponent<PlayerUnitStateManager>().Active = false;
    }

    protected void ClearTiles(List<Transform> tiles)
    {
        foreach (Transform tile in tiles)
        {
            GameObject.Destroy(tile.gameObject);
        }
        tiles.Clear();
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
