﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreativeSpore.SuperTilemapEditor;

public class PlayerUnitMovingState : UnitMovingState {

    public PlayerUnitMovingState(Stack<UnitPathingState.Node> _pathToTarget) : base(_pathToTarget)
    {
        pathToTarget = _pathToTarget;
    }

    public override void OnEnter()
    {
        unitDetails = Machine.actor.GetComponent<PlayerUnit>();
        unitTilemap = Machine.actor.transform.parent.GetComponent<STETilemap>();
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

                //TODO take this function call outside the loop. It's way too slow to be doing mid-movement. 
                //Probably should compute them all in the previous state, and pass the stack of transforms.
                nextLocation = TilemapUtils.GetGridWorldPos(MoveCursor.instance.ground, (int)nextTile.position.x, (int)nextTile.position.y);
            }
            else if (Vector2.Distance(unitDetails.gameObject.transform.position, nextLocation) < Mathf.Epsilon && pathToTarget.Count == 0)
                Machine.ReplaceTop(new PlayerUnitWaitingState());
        }
        return base.Tick();
    }

    public override void OnCancelInput()
    {
        Machine.Pop();
    }

}