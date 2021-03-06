﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//TODO Create Parent AttackingState class
public class PlayerUnitAttackingState : UnitState {

    private GameObject target;
    private Vector3 originPos;
    private Vector3 targetPos;
    private float distanceToTarget;
    private int direction = 1;

    private Weapon unitWeapon;


    public PlayerUnitAttackingState(GameObject _target)
    {
        target = _target;
    }

    public override void OnEnter()
    {
        unitDetails = Machine.Actor.GetComponent<UnitDetails>();
        unitTilemap = ObjectReferences.Instance.UnitTilemap;
        originPos = unitDetails.gameObject.transform.position;
        targetPos = target.transform.position;
        unitWeapon = unitDetails.UnitWeapon;
    }

    public override IEnumerator Tick()
    {
        distanceToTarget = Vector3.Distance(unitDetails.gameObject.transform.position, targetPos);
        unitDetails.gameObject.transform.position = (Vector3.MoveTowards(unitDetails.gameObject.transform.position, targetPos, 1.5f * Time.deltaTime * direction));

        if (distanceToTarget <= 0.70f && direction == 1)
        {
            targetPos = originPos;
        }
        if (unitDetails.gameObject.transform.position == originPos)
        {
            unitWeapon.DealDamage(target.gameObject.GetComponent<UnitDetails>());
            OnExit();
            Machine.Clear();
            Machine.Push(new PlayerUnitExhaustedState());
        }
        return base.Tick();
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
