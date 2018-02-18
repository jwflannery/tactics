using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreativeSpore.SuperTilemapEditor;

public class EnemyUnitMovingState : UnitMovingState {
    private GameObject target;
    public EnemyUnitMovingState(Stack<Vector2> pathToTarget, GameObject target) : base(pathToTarget)
    {
        this.pathToTarget = pathToTarget;
        this.target = target;
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    protected override void OnReachedDestination()
    {
        Machine.ReplaceTop(new EnemyUnitAttackingState(target));
    }
}
