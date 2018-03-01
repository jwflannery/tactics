using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreativeSpore.SuperTilemapEditor;

public class PlayerUnitConfirmTargetState : UnitState {

    private List<GameObject> unitsInRange = new List<GameObject>();
    private List<Transform> existingAttackTiles = new List<Transform>();
    private int tempGridX;
    private int tempGridY;

    public override void OnEnter()
    {
        unitDetails = Machine.Actor.GetComponent<UnitDetails>();
        unitTilemap = ObjectReferences.Instance.UnitTilemap;
        tempGridX = MapUtils.GetGridX(unitDetails.transform.position);
        tempGridY = MapUtils.GetGridY(unitDetails.transform.position);
        unitsInRange = FindUnitsInRange(unitDetails.AttackRange, tempGridX, tempGridY);

        if (unitsInRange.Exists(u => u != null))
        {
            foreach (GameObject unit in unitsInRange)
            {
                var tile = GameObject.Instantiate(GameManager.Instance.AttackTilePrefab, unit.transform.position, Quaternion.identity);
                existingAttackTiles.Add(tile.transform);
            }
        }

    }

    List<GameObject> FindUnitsInRange(int range, int originGridX, int originGridY)
    {
        List<GameObject> units = new List<GameObject>();

        foreach (GameObject u in GameManager.Instance.AllUnits)
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
            unitDetails.CurrentGridX = MapUtils.GetGridX(unitDetails.transform.position);
            unitDetails.CurrentGridY = MapUtils.GetGridY(unitDetails.transform.position);
            Machine.Push(new PlayerUnitExhaustedState());
        }

        else if (existingAttackTiles.Exists(t => MoveCursor.Instance.transform.position == t.position))
        {
            var target = GameManager.Instance.AllUnits.Find(
                x => x.GetComponent<UnitDetails>().CurrentGridX == MoveCursor.Instance.CurrentGridX && x.GetComponent<UnitDetails>().CurrentGridY == MoveCursor.Instance.CurrentGridY);
            unitDetails.CurrentGridX = MapUtils.GetGridX(unitDetails.transform.position);
            unitDetails.CurrentGridY = MapUtils.GetGridY(unitDetails.transform.position);
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
