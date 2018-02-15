using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitFreshState : UnitFreshState {

    // Use this for initialization
    public override void OnEnter()
    {
        Machine.ReplaceTop(new EnemyUnitExhaustedState());
    }

    public override void OnAcceptInput()
    {
        base.OnAcceptInput();
    }
}
