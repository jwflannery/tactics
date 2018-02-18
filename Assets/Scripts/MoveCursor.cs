using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreativeSpore.SuperTilemapEditor;

public class MoveCursor : MonoBehaviour {

    public STETilemap GroundTilemap;
    public Tile CurrentTile;
    public int CurrentGridX;
    public int CurrentGridY;
    public Vector2 CurrentLocation;
    public static MoveCursor Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update () {
        if (GameManager.IsPaused)
            return;
        CurrentGridX = TilemapUtils.GetMouseGridX(GroundTilemap, Camera.main);
        CurrentGridY = TilemapUtils.GetMouseGridY(GroundTilemap, Camera.main);
        CurrentTile = GroundTilemap.GetTile(CurrentGridX, CurrentGridY);
        if (CurrentTile != null)
        {
            transform.position = TilemapUtils.GetGridWorldPos(GroundTilemap, TilemapUtils.GetMouseGridX(GroundTilemap, Camera.main), TilemapUtils.GetMouseGridY(GroundTilemap, Camera.main));
        }
        CurrentLocation = transform.position;
    }
}
