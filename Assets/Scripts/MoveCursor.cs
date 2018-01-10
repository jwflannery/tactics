using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreativeSpore.SuperTilemapEditor;

public class MoveCursor : MonoBehaviour {

    public STETilemap ground;
    public Tile currentTile;
    public int currentGridX;
    public int currentGridY;
    public Vector2 currentLocation;
    public static MoveCursor instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update () {
        currentGridX = TilemapUtils.GetMouseGridX(ground, Camera.main);
        currentGridY = TilemapUtils.GetMouseGridY(ground, Camera.main);
        currentTile = ground.GetTile(currentGridX, currentGridY);
        if (currentTile != null)
        {
            transform.position = TilemapUtils.GetGridWorldPos(ground, TilemapUtils.GetMouseGridX(ground, Camera.main), TilemapUtils.GetMouseGridY(ground, Camera.main));
        }
        currentLocation = transform.position;
    }
}
