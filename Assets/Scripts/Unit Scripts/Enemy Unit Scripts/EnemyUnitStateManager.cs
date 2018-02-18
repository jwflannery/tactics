using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitStateManager : UnitStateManager {
    public override void RefreshUnit()
    {
        StateMachine.ReplaceTop(new EnemyUnitFreshState());
    }
    protected override void Start()
    {
        base.Start();
        StateMachine.Push(new EnemyUnitFreshState());
    }
}
