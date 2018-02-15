using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitFreshState : UnitFreshState {

    // Use this for initialization
    public override void OnEnter()
    {
        //Machine.ReplaceTop(new EnemyUnitPathingState());
    }

    public override void OnAcceptInput()
    {
        Debug.Log("Registered Accept Input in EnemyUnitFreshState.");
        Machine.ReplaceTop(new EnemyUnitPathingState());
    }
}
