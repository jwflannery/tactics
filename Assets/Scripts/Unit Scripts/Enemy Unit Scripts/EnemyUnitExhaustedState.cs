using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyUnitExhaustedState : UnitExhaustedState {
    public override void OnEnter()
    {
        base.OnEnter();
        GameManager.Instance.ActivateNextEnemyUnit();
    }
}
