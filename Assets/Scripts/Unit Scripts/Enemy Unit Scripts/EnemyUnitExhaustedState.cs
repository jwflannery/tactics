using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreativeSpore.SuperTilemapEditor;

public class EnemyUnitExhaustedState : UnitExhaustedState {
    public override void OnEnter()
    {
        base.OnEnter();
        unitDetails.CurrentGridX = TilemapUtils.GetGridX(unitTilemap, unitDetails.transform.position);
        unitDetails.CurrentGridY = TilemapUtils.GetGridY(unitTilemap, unitDetails.transform.position);
        GameManager.instance.ActivateNextEnemyUnit();
    }
}
