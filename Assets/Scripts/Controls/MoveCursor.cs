using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreativeSpore.SuperTilemapEditor;
using InControl;

public class MoveCursor : MonoBehaviour {

    public STETilemap GroundTilemap;
    public Tile CurrentTile;
    public int CurrentGridX;
    public int CurrentGridY;
    public Vector2 CurrentLocation;
    public static MoveCursor Instance = null;
    public bool IsMouseControlling = false;


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

        CheckMouseControl();
        if (IsMouseControlling)
        {
            Debug.Log("Mouse is active.");
            CurrentGridX = TilemapUtils.GetMouseGridX(GroundTilemap, Camera.main);
            CurrentGridY = TilemapUtils.GetMouseGridY(GroundTilemap, Camera.main);
            CurrentTile = GroundTilemap.GetTile(CurrentGridX, CurrentGridY);
            if (CurrentTile != null)
            {
                transform.position = TilemapUtils.GetGridWorldPos(GroundTilemap, TilemapUtils.GetMouseGridX(GroundTilemap, Camera.main), TilemapUtils.GetMouseGridY(GroundTilemap, Camera.main));
            }
        }
        else
        {
            if (InputManager.ActiveDevice.DPadLeft.WasPressed)
            {
                CurrentGridX = CurrentGridX - 1;
            }
            if (InputManager.ActiveDevice.DPadRight.WasPressed)
            {
                CurrentGridX = CurrentGridX + 1;
            }
            if (InputManager.ActiveDevice.DPadUp.WasPressed)
            {
                CurrentGridY = CurrentGridY + 1;
            }
            if (InputManager.ActiveDevice.DPadDown.WasPressed)
            {
                CurrentGridY = CurrentGridY - 1;
            }
            transform.position = TilemapUtils.GetGridWorldPos(GroundTilemap, CurrentGridX, CurrentGridY);
        }

        CurrentTile = GroundTilemap.GetTile(CurrentGridX, CurrentGridY);
        if (CurrentTile != null)
        {
            //transform.position = TilemapUtils.GetGridWorldPos(GroundTilemap, TilemapUtils.GetMouseGridX(GroundTilemap, Camera.main), TilemapUtils.GetMouseGridY(GroundTilemap, Camera.main));
        }
        CurrentLocation = transform.position;
    }

    private void CheckMouseControl()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            IsMouseControlling = true;
        }
        else if (InputManager.ActiveDevice.AnyButton)
            IsMouseControlling = false;

    }

}
