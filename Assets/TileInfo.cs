using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CreativeSpore.SuperTilemapEditor;

public class TileInfo : MonoBehaviour {

    public STETilemap objectMap;

    private string[] Parameters = new string[]
    {
        "Name",
        "Defence"
    };

    public Text infoText;
    string info;

    private Tile currentTile;
    private void Update()
    {
        currentTile = objectMap.GetTile(TilemapUtils.GetMouseGridX(objectMap, Camera.main), TilemapUtils.GetMouseGridY(objectMap, Camera.main));
        info = "";
        if (currentTile != null)
        {
            for (int i = 0; i < Parameters.Length; i++)
            {
                if (currentTile.paramContainer.FindParam(Parameters[i]) != null)
                {
                    info += Parameters[i] + ": " + currentTile.paramContainer.FindParam(Parameters[i]).ToString() + "\n";
                }
            }
        }
        infoText.text = info;

        //if (currentTile != null && currentTile.paramContainer.FindParam("Name") != null)
        //{
        //    var tileName = infoText.text = currentTile.paramContainer.FindParam("Name").GetAsString();
        //    infoText.text = info;
        //}
    }
}
