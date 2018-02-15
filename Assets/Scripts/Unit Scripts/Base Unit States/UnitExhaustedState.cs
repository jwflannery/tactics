using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreativeSpore.SuperTilemapEditor;

public class UnitExhaustedState : UnitState {

    public override void OnEnter()
    {
        unitDetails = Machine.actor.GetComponent<UnitDetails>();
        unitTilemap = Machine.actor.transform.parent.GetComponent<STETilemap>();
        Machine.actor.GetComponent<SpriteRenderer>().color = Color.grey;
        Machine.actor.GetComponent<UnitStateManager>().Active = false;
        base.OnEnter();
    }

    public override void OnExit()
    {
        Machine.actor.GetComponent<SpriteRenderer>().color = Color.white;
        base.OnExit();
    }
}
