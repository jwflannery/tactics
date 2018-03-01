using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitState : State {

    protected UnitDetails unitDetails;
    protected GameObject unitTilemap;

    public UnitState() : base()
    {

    }

    public override void OnEnter()
    {
        Machine.Actor.GetComponent<UnitStateManager>().Active = true;

        unitDetails = Machine.Actor.GetComponent<UnitDetails>();
        unitTilemap = ObjectReferences.Instance.UnitTilemap;
    }

}
