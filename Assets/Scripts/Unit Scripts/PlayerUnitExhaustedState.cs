using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreativeSpore.SuperTilemapEditor;

public class PlayerUnitExhaustedState : PlayerUnitState {

    public override void OnEnter()
    {
        unitDetails = Machine.actor.GetComponent<PlayerUnit>();
        unitTilemap = Machine.actor.transform.parent.GetComponent<STETilemap>();
        Machine.actor.GetComponent<SpriteRenderer>().color = Color.grey;
        base.OnEnter();
    }

    public override void OnExit()
    {
        Machine.actor.GetComponent<SpriteRenderer>().color = Color.white;
        base.OnExit();
    }
}
