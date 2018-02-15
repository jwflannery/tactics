using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreativeSpore.SuperTilemapEditor;

public class EnemyUnitPathingState : UnitPathingState {

    private Node closestTarget;
    List<GameObject> possibleAttackTargets = new List<GameObject>();

    public override void OnEnter()
    {
        openTiles.Clear();
        closedTiles.Clear();

        Machine.actor.GetComponent<UnitStateManager>().Active = true;
        unitDetails = Machine.actor.GetComponent<EnemyUnit>();
        unitTilemap = Machine.actor.transform.parent.GetComponent<STETilemap>();
        findReachableTiles();
        closestTarget = FindClosestTarget();
        findPathToTarget(new Vector2(unitDetails.CurrentGridX, unitDetails.CurrentGridY), closestTarget.position);
        if (possibleAttackTargets.Count == 0)
        {
            Machine.Push(new EnemyUnitExhaustedState());
        }
        else
        {
            Machine.Push(new EnemyUnitMovingState(pathToTarget, possibleAttackTargets[0]));
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
                if (n.cost < lowestCost)
                {
                    lowestCostNode = n;
                    possibleAttackTargets = adjacentUnits;
                }
            }
        } 
        return lowestCostNode;
    }

    List<GameObject> FindAdjacentUnits(Node n)
    {
        List<GameObject> units = new List<GameObject>();
        int originGridX = (int)n.position.x;
        int originGridY = (int)n.position.y;

        foreach (GameObject u in GameManager.instance.playerTeam.teamUnits)
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
