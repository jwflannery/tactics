using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreativeSpore.SuperTilemapEditor;

public class PlayerUnitWaitingState : UnitState {

    private List<GameObject> unitsInRange = new List<GameObject>();
    private List<Transform> existingAttackTiles = new List<Transform>();
    private int tempGridX;
    private int tempGridY;

    public override void OnEnter()
    {
        unitDetails = Machine.actor.GetComponent<UnitDetails>();
        unitTilemap = Machine.actor.transform.parent.GetComponent<STETilemap>();
        tempGridX = TilemapUtils.GetGridX(unitTilemap, unitDetails.transform.position);
        tempGridY = TilemapUtils.GetGridY(unitTilemap, unitDetails.transform.position);
        unitsInRange = FindUnitsInRange(unitDetails.AttackRange, tempGridX, tempGridY);

        if (unitsInRange.Exists(u => u != null))
        {
            foreach (GameObject unit in unitsInRange)
            {
                var tile = GameObject.Instantiate(GameManager.instance.attackTilePrefab, unit.transform.position, Quaternion.identity);
                existingAttackTiles.Add(tile.transform);
            }
        }

    }

    List<GameObject> FindUnitsInRange(int range, int originGridX, int originGridY)
    {
        List<GameObject> units = new List<GameObject>();

        foreach (GameObject u in GameManager.instance.AllUnits)
        {
            if (u == unitDetails.gameObject)
                continue;
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

    public override void OnCancelInput()
    {
        Machine.Pop();
    }

    public override void OnAcceptInput()
    {
        if (existingAttackTiles.Count == 0)
        {
            unitDetails.CurrentGridX = TilemapUtils.GetGridX(unitTilemap, unitDetails.transform.position);
            unitDetails.CurrentGridY = TilemapUtils.GetGridY(unitTilemap, unitDetails.transform.position);
            Machine.Push(new PlayerUnitExhaustedState());
        }

        else if (existingAttackTiles.Exists(t => MoveCursor.Instance.transform.position == t.position))
        {
            var target = GameManager.instance.AllUnits.Find(
                x => x.GetComponent<UnitDetails>().CurrentGridX == MoveCursor.Instance.CurrentGridX && x.GetComponent<UnitDetails>().CurrentGridY == MoveCursor.Instance.CurrentGridY);
            unitDetails.CurrentGridX = TilemapUtils.GetGridX(unitTilemap, unitDetails.transform.position);
            unitDetails.CurrentGridY = TilemapUtils.GetGridY(unitTilemap, unitDetails.transform.position);
            Machine.Push(new PlayerUnitAttackingState(target));
        }
    }

    public override void OnExit()
    {
        clearTiles(existingAttackTiles);
        unitsInRange.Clear();
        base.OnExit();
    }

    private void clearTiles(List<Transform> tiles)
    {
        foreach (Transform tile in tiles)
        {
            GameObject.Destroy(tile.gameObject);
        }
        tiles.Clear();
    }

}
