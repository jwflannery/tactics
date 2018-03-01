using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapUtils
{
    public static Vector3 GetGridWorldPos(int gridX, int gridY)
    {
        return new Vector2((gridX + .5f) * ObjectReferences.CellSize.x, (gridY + .5f) * ObjectReferences.CellSize.y);
    }

    public static int GetGridX(Vector2 position)
    {
        return Mathf.FloorToInt((position.x + Vector2.kEpsilon) / ObjectReferences.CellSize.x);
    }

    public static int GetGridY(Vector2 position)
    {
        return Mathf.FloorToInt((position.y + Vector2.kEpsilon) / ObjectReferences.CellSize.y);
    }

    public static int GetMouseGridX()
    {
        Vector2 locPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return Mathf.FloorToInt((locPos.x + Vector2.kEpsilon) / ObjectReferences.CellSize.x);
    }

    public static int GetMouseGridY()
    {
        Vector2 locPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return Mathf.FloorToInt((locPos.y + Vector2.kEpsilon) / ObjectReferences.CellSize.y);
    }

    public static GameObject FindUnitOnTile(int gridX, int gridY)
    {
        GameObject unit = GameManager.Instance.AllUnits.Find(x => x.GetComponent<UnitDetails>().CurrentGridX == gridX && x.GetComponent<UnitDetails>().CurrentGridY == gridY);
        return unit;
    }
}
