using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreativeSpore.SuperTilemapEditor;

public class MapDetails {

    public static int xMin = -5;
    public static int yMin = -6;
    public static int xMax = 15;
    public static int yMax = 6;

    public static int xOffset;
    public static int yOffset;

    public static Dictionary<MapKey, TileDetails> Tiles = new Dictionary<MapKey, TileDetails>();
    //Tiles = new TileDetails[ObjectReferences.Instance.BackgroundTilemap.GridWidth, ObjectReferences.Instance.BackgroundTilemap.GridHeight];

    public static void InitialiseTiles(STETilemap tilemap)
    {
        for (int x = xMin; x < xMax + 1; x++)
        {
            for (int y = yMin; y < yMax + 1; y++)
            {
                var passable = CheckForCollider(x, y, tilemap);
                Tiles.Add(new MapKey(x, y), new TileDetails(x, y, passable));
            }
        }
    }

    private static bool CheckForCollider(int x, int y, STETilemap tilemap)
    {
        var tile = tilemap.GetTile(x, y);
        return !(tile != null && tile.collData.type != eTileCollider.None);
    }

    public static TileDetails GetTileDetails(int gridX, int gridY)
    {
        TileDetails details;
        if (Tiles.TryGetValue(new MapKey(gridX, gridY), out details))
        {
            return details;
        }
        return null;
    }

    public struct MapKey
    {
        internal readonly int a;
        internal readonly int b;

        public MapKey(int a, int b)
        {
            this.a = a;
            this.b = b;
        }

        internal class Comparer : IEqualityComparer<MapKey>
        {
            internal static readonly Comparer Instance = new Comparer();
            private Comparer() { }

            public bool Equals(MapKey x, MapKey y)
            {
                return x.a == y.a && x.b == y.b;
            }

            public int GetHashCode(MapKey x)
            {
                return x.GetHashCode();
            }
        }
    }

}
