using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitFreshState : UnitFreshState {

    public override void OnAcceptInput()
    {
        Debug.Log("Registered Accept Input in EnemyUnitFreshState.");
        Machine.ReplaceTop(new EnemyUnitPathingState());
    }
}
