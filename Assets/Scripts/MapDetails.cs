using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDetails {

    public static int xMin = 0;
    public static int yMin = -30;
    public static int xMax = 30;
    public static int yMax = 0;

    public static PolygonCollider2D mapCollider;
    public static List<Tile> TileDatas = new List<Tile>();


    public static Dictionary<MapKey, TileDetails> Tiles = new Dictionary<MapKey, TileDetails>();
    //Tiles = new TileDetails[ObjectReferences.Instance.BackgroundTilemap.GridWidth, ObjectReferences.Instance.BackgroundTilemap.GridHeight];

    public static void InitialiseTiles(GameObject tilemap)
    {
        mapCollider = GameObject.Find("/RW_Level1/Colliders/Collision").GetComponent<PolygonCollider2D>();

        for (int x = xMin; x < xMax; x++)
        {
            for (int y = yMin; y < yMax; y++)
            {
                var passable = !CheckForCollider(x, y, tilemap);
                Tiles.Add(new MapKey(x, y), new TileDetails(x, y, passable));
            }
        }
        foreach (Tile tileData in TileDatas)
        {
            tileData.AttachToMap();
        }
    }

    private static bool CheckForCollider(int x, int y, GameObject tilemap)
    {
        if (mapCollider.OverlapPoint(MapUtils.GetGridWorldPos(x, y)))
        {
            return true;
        }

        return false;
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
