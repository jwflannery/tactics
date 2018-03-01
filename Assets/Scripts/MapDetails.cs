using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDetails {

    public static int xMin = -5;
    public static int yMin = -6;
    public static int xMax = 15;
    public static int yMax = 6;

    public static int xOffset;
    public static int yOffset;

    public static TileDetails[,] Tiles = new TileDetails[21, 13];
    //Tiles = new TileDetails[ObjectReferences.Instance.BackgroundTilemap.GridWidth, ObjectReferences.Instance.BackgroundTilemap.GridHeight];

    public static void InitialiseTiles()
    {
        SetOffsets();
        for (int x = 0; x < xMax; x++)
        {
            for (int y = 0; y < yMax; y++)
            {
                Tiles[x, y] = new TileDetails(x, y);
            }
        }
    }

    private static void SetOffsets()
    {
        xOffset = -xMin;
        yOffset = -yMin;

        xMax = xMax + xOffset;
        yMax = yMax + yOffset;
    }
    public static TileDetails GetTileDetails(int gridX, int gridY)
    {
        if (gridX + xOffset > 0 && gridY + yOffset > 0)
        {
            return Tiles[gridX + xOffset, gridY + yOffset];
        }
        return null;
    }

}
