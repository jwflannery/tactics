using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitStateManager : UnitStateManager {
    // Use this for initialization
    protected override void Start()
    {
        StartCoroutine(stateMachine.TickRoutine());
        stateMachine.Push(new EnemyUnitFreshState());
    }

    public override void RefreshUnit()
    {
        stateMachine.ReplaceTop(new EnemyUnitFreshState());
    }
}
