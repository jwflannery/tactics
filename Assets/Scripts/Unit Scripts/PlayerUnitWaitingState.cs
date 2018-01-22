using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreativeSpore.SuperTilemapEditor;

public class PlayerUnitWaitingState : PlayerUnitState {

    private List<GameObject> unitsInRange = new List<GameObject>();
    private int tempGridX;
    private int tempGridY;

    public override void OnEnter()
    {
        Debug.Log("Entered Waiting State.");
        unitDetails = Machine.actor.GetComponent<PlayerUnit>();
        unitTilemap = Machine.actor.transform.parent.GetComponent<STETilemap>();
        tempGridX = TilemapUtils.GetGridX(unitTilemap, unitDetails.transform.position);
        tempGridY = TilemapUtils.GetGridY(unitTilemap, unitDetails.transform.position);

        unitsInRange = FindUnitsInRange(unitDetails.AttackRange, tempGridX, tempGridY);

        if (unitsInRange.Exists(u => u != null))
        {
            foreach (GameObject unit in unitsInRange)
            {
                //unit.GetComponent<PlayerUnit>().Health -= 10;
                //Debug.Log("Adjacent unit found");
                unit.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }

    }

    List<GameObject> FindUnitsInRange(int range, int originGridX, int originGridY)
    {
        List<GameObject> units = new List<GameObject>();

        foreach (GameObject u in GameManager.instance.units)
        {
            if (u == unitDetails.gameObject)
                continue;
            PlayerUnit unitInfo = u.GetComponent<PlayerUnit>();
            if (unitInfo.CurrentGridX == originGridX + 1 && unitInfo.CurrentGridY == originGridY)
            {
                units.Add(u);
            }
            if (unitInfo.CurrentGridX == originGridX - 1 && unitInfo.CurrentGridY == originGridY)
            {
                units.Add(u);
            }
            if (unitInfo.CurrentGridX == originGridX && unitInfo.CurrentGridY == originGridY + 1)
            {
                units.Add(u);
            }
            if (unitInfo.CurrentGridX == originGridX && unitInfo.CurrentGridY == originGridY - 1)
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
        unitDetails.CurrentGridX = TilemapUtils.GetGridX(unitTilemap, unitDetails.transform.position);
        unitDetails.CurrentGridY = TilemapUtils.GetGridY(unitTilemap, unitDetails.transform.position);
        OnExit();
        Machine.Clear();
        Machine.Push(new PlayerUnitExhaustedState());
    }

    public override void OnExit()
    {
        foreach (GameObject unit in unitsInRange)
        {
            unit.GetComponent<SpriteRenderer>().color = Color.white;
        }

        unitsInRange.Clear();
        base.OnExit();
    }

}
