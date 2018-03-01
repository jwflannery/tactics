using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreativeSpore.SuperTilemapEditor;

public class UnitExhaustedState : UnitState {

    private SpriteRenderer renderer;

    public override void OnEnter()
    {
        base.OnEnter();
        renderer = Machine.Actor.GetComponent<SpriteRenderer>();
        renderer.color = Color.grey;
        GameManager.CurrentlySelectedUnit = null;

        unitDetails.CurrentGridX = MapUtils.GetGridX(unitDetails.transform.position);
        unitDetails.CurrentGridY = MapUtils.GetGridY(unitDetails.transform.position);
    }

    public override void OnExit()
    {
        renderer.color = Color.white;
        base.OnExit();
    }
}
