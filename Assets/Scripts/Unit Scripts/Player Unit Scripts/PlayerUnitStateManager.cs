using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitStateManager : UnitStateManager {
    public override void RefreshUnit()
    {
        StateMachine.ReplaceTop(new PlayerUnitFreshState());
    }

    protected override void Start () {
        base.Start();
        StateMachine.Push(new PlayerUnitFreshState());
    }
}
