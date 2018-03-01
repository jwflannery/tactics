using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerUnitMovingState : UnitMovingState {

    public PlayerUnitMovingState(Stack<Vector2> pathToTarget) : base(pathToTarget)
    {
        this.pathToTarget = pathToTarget;
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    protected override void OnReachedDestination()
    {
        Machine.ReplaceTop(new PlayerUnitWaitingState());
    }

    public override void OnCancelInput()
    {
        Machine.Pop();
    }
}