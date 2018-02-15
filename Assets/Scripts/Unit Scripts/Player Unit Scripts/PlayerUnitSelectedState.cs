using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreativeSpore.SuperTilemapEditor;

public class PlayerUnitSelectedState : UnitPathingState {

    private Tile lastCursorTile;
    private Tile currentCursorTile;
    protected List<Transform> existingMoveTiles = new List<Transform>();
    protected List<Transform> existingPathTiles = new List<Transform>();

    public override void OnEnter()
    {
        Machine.actor.GetComponent<UnitStateManager>().Active = true;
        moveTile = GameManager.instance.moveTilePrefab;
        unitDetails = Machine.actor.GetComponent<PlayerUnit>();
        unitTilemap = Machine.actor.transform.parent.GetComponent<STETilemap>();
        findReachableTiles();
        foreach (Node reachableTile in closedTiles)
        {
            createMoveTile(TilemapUtils.GetGridWorldPos(MoveCursor.instance.ground, (int)reachableTile.position.x, (int)reachableTile.position.y));
        }
        openTiles.Clear();
        closedTiles.Clear();
    }
    #region Display path to current target

    public override IEnumerator Tick()
    {
        if (existingMoveTiles.Exists(t => MoveCursor.instance.transform.position == t.position))
        {
            currentCursorTile = MoveCursor.instance.currentTile;
            if (lastCursorTile != currentCursorTile)
            {
                clearTiles(existingPathTiles);
                pathToTarget.Clear();
            }
            findPathToTarget(new Vector2(unitDetails.CurrentGridX, unitDetails.CurrentGridY), new Vector2(MoveCursor.instance.currentGridX, MoveCursor.instance.currentGridY));
        }
        return base.Tick();
    }

    protected override void findPathToTarget(Vector2 source, Vector2 target)
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
                createPathTiles(closedTiles.Find(n => n.position == target));
                openTiles.Clear();
                closedTiles.Clear();
                break;
            }
        }
    }

    private void createPathTiles(Node target)
    {
        Node current = target;
        do
        {
            pathToTarget.Push(current);
            var tile = GameObject.Instantiate(GameManager.instance.pathTilePrefab, TilemapUtils.GetGridWorldPos(MoveCursor.instance.ground, (int)current.position.x, (int)current.position.y), Quaternion.identity);
            existingPathTiles.Add(tile.transform);
            current = current.parent;
        } while (current.parent != null);
    }
    #endregion

    #region Display all tiles reachable by the unit.

    private void createMoveTile(Vector3 position)
    {
        var tile = GameObject.Instantiate(moveTile, position, Quaternion.identity);
        existingMoveTiles.Add(tile.transform);
    }
    #endregion

    public override void OnAcceptInput()
    {
        base.OnAcceptInput();
        if (!existingMoveTiles.Exists(t => MoveCursor.instance.transform.position == t.position))
            return;
        clearTiles(existingMoveTiles);
        clearTiles(existingPathTiles);
        Machine.Push(new PlayerUnitMovingState(pathToTarget));
    }
    public override void OnExit()
    {
        base.OnExit();
        openTiles.Clear();
        closedTiles.Clear();
        clearTiles(existingMoveTiles);
        clearTiles(existingPathTiles);
        Machine.actor.GetComponent<UnitStateManager>().Active = false;
    }
    private void clearTiles(List<Transform> tiles)
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
