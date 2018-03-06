using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
        unitsInRange = FindUnitsInRange(unitDetails.UnitWeapon.maxRange, tempGridX, tempGridY);

        if (unitsInRange.Exists(u => u != null))
        {
            foreach (GameObject unit in unitsInRange)
            {
                var tile = GameObject.Instantiate(GameManager.Instance.AttackTilePrefab, unit.transform.position, Quaternion.identity);
                existingAttackTiles.Add(tile.transform);
            }
        }

    }

    List<TileDetails> GetTilesInRange(int range, int sourceX, int sourceY)
    {
        List<TileDetails> tiles = new List<TileDetails>();
        List<TileDetails> openTiles = new List<TileDetails>();
        tiles.Add(MapDetails.GetTileDetails(sourceX, sourceY));


        for (int i = 0; i < range; i++)
        {
            foreach (TileDetails tile in tiles)
            {
                openTiles.AddRange(GetAdjacentTiles((int)tile.GridPos.x, (int)tile.GridPos.y));
            }
            tiles.AddRange(openTiles);
        }
        return tiles;
    }

    List<TileDetails> GetAdjacentTiles(int sourceX, int sourceY)
    {
        List<TileDetails> tiles = new List<TileDetails>
        {
            MapDetails.GetTileDetails(sourceX + 1, sourceY),
            MapDetails.GetTileDetails(sourceX - 1, sourceY),
            MapDetails.GetTileDetails(sourceX, sourceY + 1),
            MapDetails.GetTileDetails(sourceX, sourceY - 1)
        };
        return tiles;
    }

    List<GameObject> FindUnitsInRange(int range, int originGridX, int originGridY)
    {
        List<GameObject> units = new List<GameObject>();
        List<TileDetails> tilesInRange = new List<TileDetails>();
        tilesInRange = GetTilesInRange(2, originGridX, originGridY);

        foreach (GameObject u in GameManager.Instance.AllUnits)
        {
            if (u == unitDetails.gameObject)
                continue;
            UnitDetails unitInfo = u.GetComponent<UnitDetails>();
            if (tilesInRange.Exists(x => IsUnitOnTile(unitInfo, x)) && unitInfo.TeamNumber != unitDetails.TeamNumber)
            {
                units.Add(u);
            }
            //if (unitInfo.CurrentGridX == originGridX - 1 && unitInfo.CurrentGridY == originGridY && unitInfo.TeamNumber != unitDetails.TeamNumber)
            //{
            //    units.Add(u);
            //}
            //if (unitInfo.CurrentGridX == originGridX && unitInfo.CurrentGridY == originGridY + 1 && unitInfo.TeamNumber != unitDetails.TeamNumber)
            //{
            //    units.Add(u);
            //}
            //if (unitInfo.CurrentGridX == originGridX && unitInfo.CurrentGridY == originGridY - 1 && unitInfo.TeamNumber != unitDetails.TeamNumber)
            //{
            //    units.Add(u);
            //}
        }
        return units;
    }
    
    bool IsUnitOnTile(UnitDetails unit, TileDetails tile)
    {
        if (tile != null) 
            return (unit.CurrentGridX == tile.GridPos.x && unit.CurrentGridY == tile.GridPos.y);
        return false;
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
