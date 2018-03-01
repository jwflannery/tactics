using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tiled2Unity;

[Tiled2Unity.CustomTiledImporter]
public class CustomImporter : Tiled2Unity.ICustomTiledImporter
{
    public void HandleCustomProperties(GameObject gameObject,
        IDictionary<string, string> customProperties)
    {
        if (customProperties.ContainsKey("Name"))
        {
            // Add the terrain tile game object
            Tile tile = gameObject.AddComponent<Tile>();
            tile.TileName = customProperties["Name"];
        }
    }

    public void CustomizePrefab(GameObject prefab)
    {
    }
}
