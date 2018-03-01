using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDetails {

    private Vector2 gridPos;
    private bool passable;
    private string tileName;
    private Tile tileData;

    public TileDetails(int gridX, int gridY)
    {
        GridPos = new Vector2(gridX, gridY);
        Passable = true;
    }

    public TileDetails(int gridX, int gridY, bool passable)
    {
        GridPos = new Vector2(gridX, gridY);
        Passable = passable;
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

    public string TileName
    {
        get
        {
            return tileName;
        }

        set
        {
            tileName = value;
        }
    }

    public Tile TileData
    {
        get
        {
            return tileData;
        }

        set
        {
            tileData = value;
        }
    }

    public void ParseTileData(Tile tileData)
    {
        this.tileData = tileData;
        TileName = tileData.TileName;
    }
}
