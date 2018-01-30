using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreativeSpore.SuperTilemapEditor;

public class PlayerUnitWaitingState : PlayerUnitState {

    private List<GameObject> unitsInRange = new List<GameObject>();
    private List<Transform> existingAttackTiles = new List<Transform>();
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
                //unit.GetComponent<SpriteRenderer>().color = Color.red;
                var tile = GameObject.Instantiate(GameManager.instance.attackTilePrefab, unit.transform.position, Quaternion.identity);
                existingAttackTiles.Add(tile.transform);
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

        if (existingAttackTiles.Exists(t => MoveCursor.instance.transform.position == t.position))
        {
            var target = GameManager.instance.units.Find(
                x => x.GetComponent<PlayerUnit>().CurrentGridX == MoveCursor.instance.currentGridX && x.GetComponent<PlayerUnit>().CurrentGridY == MoveCursor.instance.currentGridY);
            target.gameObject.GetComponent<PlayerUnit>().Health -= 10;
        }
        
        unitDetails.CurrentGridX = TilemapUtils.GetGridX(unitTilemap, unitDetails.transform.position);
        unitDetails.CurrentGridY = TilemapUtils.GetGridY(unitTilemap, unitDetails.transform.position);
        OnExit();
        Machine.Clear();
        Machine.Push(new PlayerUnitExhaustedState());
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
