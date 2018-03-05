using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitFreshState : UnitFreshState {

    public override void OnAcceptInput()
    {
        Machine.ReplaceTop(new EnemyUnitPathingState());
    }
}
