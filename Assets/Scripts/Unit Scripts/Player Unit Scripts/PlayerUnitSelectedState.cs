using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreativeSpore.SuperTilemapEditor;

public class PlayerUnitSelectedState : UnitPathingState {

    private Tile lastCursorTile;
    private Tile currentCursorTile;

    public override void OnEnter()
    {

        GameManager.CurrentlySelectedUnit = Machine.Actor.GetComponent<PlayerUnitStateManager>();
        base.OnEnter();
        foreach (Node reachableTile in closedTiles)
        {
            CreateMoveTile(TilemapUtils.GetGridWorldPos(ObjectReferences.Instance.BackgroundTilemap, (int)reachableTile.Position.x, (int)reachableTile.Position.y));
        }
        openTiles.Clear();
        closedTiles.Clear();
    }
    #region Display path to current target

    public override IEnumerator Tick()
    {
        if (existingMoveTiles.Exists(t => MoveCursor.Instance.transform.position == t.position))
        {
            currentCursorTile = MoveCursor.Instance.CurrentTile;
            if (lastCursorTile != currentCursorTile)
            {
                ClearTiles(existingPathTiles);
                pathToTarget.Clear();
            }
            FindPathToTarget(new Vector2(unitDetails.CurrentGridX, unitDetails.CurrentGridY), new Vector2(MoveCursor.Instance.CurrentGridX, MoveCursor.Instance.CurrentGridY));
        }
        return base.Tick();
    }
    #endregion

    public override void OnAcceptInput()
    {
        base.OnAcceptInput();
        if (!existingMoveTiles.Exists(t => MoveCursor.Instance.transform.position == t.position))
            return;
        ClearTiles(existingMoveTiles);
        ClearTiles(existingPathTiles);
        Machine.Push(new PlayerUnitMovingState(pathToTarget));
    }

    public override void OnCancelInput()
    {
        base.OnCancelInput();
        GameManager.CurrentlySelectedUnit = null;
        Machine.ReplaceTop(new PlayerUnitFreshState());
    }
}
