﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreativeSpore.SuperTilemapEditor;

public class EnemyUnitAttackingState : UnitState
{

    private GameObject target;
    private Vector3 originPos;
    private Vector3 targetPos;
    private float distanceToTarget;
    private int direction = 1;
    public EnemyUnitAttackingState(GameObject _target)
    {
        target = _target;
    }

    public override void OnEnter()
    {
        unitDetails = Machine.actor.GetComponent<UnitDetails>();
        unitTilemap = Machine.actor.transform.parent.GetComponent<STETilemap>();
        originPos = unitDetails.gameObject.transform.position;
        targetPos = target.transform.position;
        Debug.Log("Entered EnemyUnitAttackingState");
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
            target.gameObject.GetComponent<UnitDetails>().Health -= 10;
            OnExit();
            Machine.Clear();
            unitDetails.CurrentGridX = TilemapUtils.GetGridX(unitTilemap, unitDetails.transform.position);
            unitDetails.CurrentGridY = TilemapUtils.GetGridY(unitTilemap, unitDetails.transform.position);
            Machine.Push(new EnemyUnitExhaustedState());
        }
        yield return null;
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}