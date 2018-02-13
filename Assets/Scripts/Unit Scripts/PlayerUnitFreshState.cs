using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreativeSpore.SuperTilemapEditor;

public class PlayerUnitFreshState : PlayerUnitState {

    public PlayerUnitFreshState() : base()
    {

    }

    public override void OnEnter()
    {
        unitDetails = Machine.actor.GetComponent<PlayerUnit>();
        unitTilemap = Machine.actor.transform.parent.GetComponent<STETilemap>();
        //unitDetails.CurrentGridX = TilemapUtils.GetGridX(unitTilemap, unitDetails.transform.position);
        //unitDetails.CurrentGridY = TilemapUtils.GetGridY(unitTilemap, unitDetails.transform.position);
    }

    public override void OnAcceptInput()
    {
        if (MoveCursor.instance.transform.position == Machine.actor.transform.position)
        {
            Machine.Push(new PlayerUnitSelectedState());
        }
    }
}
