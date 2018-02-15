using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitMovingState : UnitMovingState {
    public EnemyUnitMovingState(Stack<UnitPathingState.Node> _pathToTarget) : base(_pathToTarget)
    {
        pathToTarget = _pathToTarget;
    }

    public override void OnEnter()
    {
        Machine.ReplaceTop(new EnemyUnitExhaustedState());
    }

    public override IEnumerator Tick()
    {
        yield return null;
    }
}
