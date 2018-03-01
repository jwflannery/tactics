using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CreativeSpore.SuperTilemapEditor;

public class TileInfo : MonoBehaviour {

    private GameObject foregroundTilemap;
    private Text InfoText;

    private string[] parameters = new string[]
    {
        "Name",
        "Defence"
    };
    private string info;
    private Tile currentTile;

    private void Start()
    {
        foregroundTilemap = ObjectReferences.Instance.ForegroundTilemap;
        InfoText = ObjectReferences.Instance.TileInfoText;
    }

    private void Update()
    {
    //    if (GameManager.IsPaused)
    //        return;
    //    currentTile = foregroundTilemap.GetTile(TilemapUtils.GetMouseGridX(foregroundTilemap, Camera.main), TilemapUtils.GetMouseGridY(foregroundTilemap, Camera.main));
    //    info = "";
    //    if (currentTile != null)
    //    {
    //        for (int i = 0; i < parameters.Length; i++)
    //        {
    //            if (currentTile.paramContainer.FindParam(parameters[i]) != null)
    //            {
    //                info += parameters[i] + ": " + currentTile.paramContainer.FindParam(parameters[i]).ToString() + "\n";
    //            }
    //        }
    //    }
    //    InfoText.text = info;
    }
}
