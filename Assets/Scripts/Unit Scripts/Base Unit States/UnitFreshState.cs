using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreativeSpore.SuperTilemapEditor;

public class UnitFreshState : UnitState {

    public UnitFreshState() : base()
    {

    }

    public override void OnEnter()
    {
        unitDetails = Machine.actor.GetComponent<PlayerUnit>();
        unitTilemap = Machine.actor.transform.parent.GetComponent<STETilemap>();
    }

    public override void OnAcceptInput()
    {
        if (MoveCursor.Instance.transform.position == Machine.actor.transform.position)
        {
            Machine.Push(new PlayerUnitSelectedState());
        }
    }
}
