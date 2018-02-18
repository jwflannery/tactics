using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreativeSpore.SuperTilemapEditor;

public class EnemyUnitMovingState : UnitMovingState {
    private GameObject target;
    public EnemyUnitMovingState(Stack<UnitPathingState.Node> _pathToTarget, GameObject _target) : base(_pathToTarget)
    {
        pathToTarget = _pathToTarget;
        target = _target;
    }

    public override void OnEnter()
    {
        unitDetails = Machine.actor.GetComponent<EnemyUnit>();
        unitTilemap = Machine.actor.transform.parent.GetComponent<STETilemap>();
        nextLocation = TilemapUtils.GetGridWorldPos(MoveCursor.Instance.GroundTilemap, (int)pathToTarget.Peek().position.x, (int)pathToTarget.Peek().position.y);
    }

    public override IEnumerator Tick()
    {
        if (pathToTarget.Count >= 0)
        {
            unitDetails.gameObject.transform.position = Vector3.MoveTowards(unitDetails.gameObject.transform.position, nextLocation, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(unitDetails.gameObject.transform.position, nextLocation) < Mathf.Epsilon && pathToTarget.Count > 0)
            {
                var nextTile = pathToTarget.Pop();

                //TODO take this function call outside the loop. It's way too slow to be doing mid-movement. 
                //Probably should compute them all in the previous state, and pass the stack of transforms.
                nextLocation = TilemapUtils.GetGridWorldPos(MoveCursor.Instance.GroundTilemap, (int)nextTile.position.x, (int)nextTile.position.y);
            }
            else if (Vector2.Distance(unitDetails.gameObject.transform.position, nextLocation) < Mathf.Epsilon && pathToTarget.Count == 0)
                Machine.ReplaceTop(new EnemyUnitAttackingState(target));
        }
        yield return null;
    }
}
