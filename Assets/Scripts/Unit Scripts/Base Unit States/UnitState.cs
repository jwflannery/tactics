using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreativeSpore.SuperTilemapEditor;

public class UnitState : State {

    protected UnitDetails unitDetails;
    protected STETilemap unitTilemap;

    public UnitState() : base()
    {

    }

    public override void OnEnter()
    {
        Machine.Actor.GetComponent<UnitStateManager>().Active = true;

        unitDetails = Machine.Actor.GetComponent<UnitDetails>();
        unitTilemap = Machine.Actor.transform.parent.GetComponent<STETilemap>();
    }

}
