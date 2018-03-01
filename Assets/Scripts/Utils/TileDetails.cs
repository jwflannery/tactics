using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDetails : MonoBehaviour {

    private Vector2 gridPos;
    private bool passable;

    public TileDetails(int gridX, int gridY)
    {
        GridPos = new Vector2(gridX, gridY);
        Passable = true;
    }


    public bool Passable
    {
        get
        {
            return passable;
        }

        set
        {
            passable = value;
        }
    }
    public Vector2 GridPos
    {
        get
        {
            return gridPos;
        }

        set
        {
            gridPos = value;
        }
    }
}
