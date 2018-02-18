using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreativeSpore.SuperTilemapEditor;

public abstract class UnitMovingState : UnitState
{

    protected Vector2 nextLocation;
    protected Stack<Vector2> pathToTarget;
    protected float moveSpeed = 3f;

    public UnitMovingState(Stack<Vector2> pathToTarget)
    {
        this.pathToTarget = pathToTarget;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        nextLocation = pathToTarget.Pop();
    }

    public override IEnumerator Tick()
    {
        if (pathToTarget.Count >= 0)
        {
            unitDetails.gameObject.transform.position = Vector3.MoveTowards(unitDetails.gameObject.transform.position, nextLocation, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(unitDetails.gameObject.transform.position, nextLocation) == 0 && pathToTarget.Count > 0)
            {
                nextLocation = pathToTarget.Pop();
            }
            else if (Vector2.Distance(unitDetails.gameObject.transform.position, nextLocation) == 0 && pathToTarget.Count == 0)
                OnReachedDestination();
        }
        return base.Tick();
    }

    protected abstract void OnReachedDestination();
}