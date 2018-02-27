using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CreativeSpore.SuperTilemapEditor;

public class TileInfo : MonoBehaviour {

    public STETilemap objectMap;
    public Text InfoText;

    private string[] parameters = new string[]
    {
        "Name",
        "Defence"
    };
    private string info;
    private Tile currentTile;

    private void Update()
    {
        if (GameManager.IsPaused)
            return;
        currentTile = objectMap.GetTile(TilemapUtils.GetMouseGridX(objectMap, Camera.main), TilemapUtils.GetMouseGridY(objectMap, Camera.main));
        info = "";
        if (currentTile != null)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                if (currentTile.paramContainer.FindParam(parameters[i]) != null)
                {
                    info += parameters[i] + ": " + currentTile.paramContainer.FindParam(parameters[i]).ToString() + "\n";
                }
            }
        }
        InfoText.text = info;
    }
}
