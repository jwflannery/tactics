using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreativeSpore.SuperTilemapEditor;

public class PlayerUnitMovingState : PlayerUnitState {

    private Vector2 nextLocation;
    private Stack<PlayerUnitSelectedState.Node> pathToTarget;
    private float moveSpeed = 2.0f;

    public PlayerUnitMovingState(Stack<PlayerUnitSelectedState.Node> pathToTarget)
    {
        this.pathToTarget = pathToTarget;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        unitDetails = Machine.actor.GetComponent<PlayerUnit>();
        unitTilemap = Machine.actor.transform.parent.GetComponent<STETilemap>();
        Debug.Log("Entered Moving State.");
        nextLocation = TilemapUtils.GetGridWorldPos(MoveCursor.instance.ground, (int)pathToTarget.Peek().position.x, (int)pathToTarget.Peek().position.y);
    }

    public override IEnumerator Tick()
    {
        if (pathToTarget.Count >= 0)
        {
            unitDetails.gameObject.transform.position = Vector3.MoveTowards(unitDetails.gameObject.transform.position, nextLocation, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(unitDetails.gameObject.transform.position, nextLocation) < Mathf.Epsilon && pathToTarget.Count > 0)
            {
                var nextTile = pathToTarget.Pop();
                nextLocation = TilemapUtils.GetGridWorldPos(MoveCursor.instance.ground, (int)nextTile.position.x, (int)nextTile.position.y);
            }
            else if (Vector2.Distance(unitDetails.gameObject.transform.position, nextLocation) < Mathf.Epsilon && pathToTarget.Count == 0)
                Machine.ReplaceTop(new PlayerUnitWaitingState());
                //Machine.Pop();
        }
        return base.Tick();
    }

    public override void OnCancelInput()
    {
        Machine.Pop();
    }

}