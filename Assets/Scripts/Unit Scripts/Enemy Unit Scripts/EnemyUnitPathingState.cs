using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreativeSpore.SuperTilemapEditor;

public class EnemyUnitPathingState : UnitPathingState {

    private Node closestTarget;
    List<GameObject> possibleAttackTargets = new List<GameObject>();

    public override void OnEnter()
    {
        base.OnEnter();

        closestTarget = FindClosestTarget();
        FindPathToTarget(new Vector2(unitDetails.CurrentGridX, unitDetails.CurrentGridY), closestTarget.Position);
        if (possibleAttackTargets.Count == 0)
        {
            Machine.Push(new EnemyUnitExhaustedState());
        }
        else
        {
            Machine.Push(new EnemyUnitMovingState(pathToTarget, possibleAttackTargets[0]));
            ClearTiles(existingMoveTiles);
            ClearTiles(existingPathTiles);
        }
    }

    Node FindClosestTarget()
    {
        Node lowestCostNode = new Node(new Vector2(unitDetails.CurrentGridX, unitDetails.CurrentGridY), null, 0);
        int lowestCost = 10000;
        List<GameObject> adjacentUnits = new List<GameObject>();        
        foreach (Node n in closedTiles)
        {
            adjacentUnits = FindAdjacentUnits(n);
            if (adjacentUnits.Count > 0)
            {
                if (n.Cost < lowestCost)
                {
                    lowestCostNode = n;
                    lowestCost = n.Cost;
                    possibleAttackTargets = adjacentUnits;
                }
            }
        } 
        return lowestCostNode;
    }

    List<GameObject> FindAdjacentUnits(Node n)
    {
        List<GameObject> units = new List<GameObject>();
        int originGridX = (int)n.Position.x;
        int originGridY = (int)n.Position.y;

        foreach (GameObject u in GameManager.Instance.PlayerTeam.TeamUnits)
        {
            UnitDetails unitInfo = u.GetComponent<UnitDetails>();
            if (unitInfo.CurrentGridX == originGridX + 1 && unitInfo.CurrentGridY == originGridY && unitInfo.TeamNumber != unitDetails.TeamNumber)
            {
                units.Add(u);
            }
            if (unitInfo.CurrentGridX == originGridX - 1 && unitInfo.CurrentGridY == originGridY && unitInfo.TeamNumber != unitDetails.TeamNumber)
            {
                units.Add(u);
            }
            if (unitInfo.CurrentGridX == originGridX && unitInfo.CurrentGridY == originGridY + 1 && unitInfo.TeamNumber != unitDetails.TeamNumber)
            {
                units.Add(u);
            }
            if (unitInfo.CurrentGridX == originGridX && unitInfo.CurrentGridY == originGridY - 1 && unitInfo.TeamNumber != unitDetails.TeamNumber)
            {
                units.Add(u);
            }
        }
        return units;
    }
}
