using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TileInfo : MonoBehaviour {

    private GameObject foregroundTilemap;
    private Text InfoText;
    private TileDetails currentTile;

    private string[] parameters = new string[]
    {
        "Name",
        "Defence"
    };
    private string info;


    private void Start()
    {
        foregroundTilemap = ObjectReferences.Instance.ForegroundTilemap;
        InfoText = ObjectReferences.Instance.TileInfoText;
    }

    private void Update()
    {
        if (GameManager.IsPaused)
            return;
        currentTile = MoveCursor.Instance.CurrentTile;
        info = "";
        if (currentTile != null)
        {
            info = "Name: " + currentTile.TileName;
        }
        InfoText.text = info;
    }
}
