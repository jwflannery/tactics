using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreativeSpore.SuperTilemapEditor;
using InControl;

public class MoveCursor : MonoBehaviour {

    private STETilemap backgroundTilemap;
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

    private void Start()
    {
        backgroundTilemap = ObjectReferences.Instance.BackgroundTilemap;
    }

    // Update is called once per frame
    void Update () {
        if (GameManager.IsPaused)
            return;

        CheckMouseControl();
        if (IsMouseControlling)
        {
            Debug.Log("Mouse is active.");
            //CurrentGridX = TilemapUtils.GetMouseGridX(backgroundTilemap, Camera.main);
            CurrentGridX = MouseGridX();
            //CurrentGridY = TilemapUtils.GetMouseGridY(backgroundTilemap, Camera.main);
            CurrentGridY = MouseGridY();
            CurrentTile = backgroundTilemap.GetTile(CurrentGridX, CurrentGridY);
            if (CurrentTile != null)
            {
                transform.position = TilemapUtils.GetGridWorldPos(backgroundTilemap, TilemapUtils.GetMouseGridX(backgroundTilemap, Camera.main), TilemapUtils.GetMouseGridY(backgroundTilemap, Camera.main));
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
            transform.position = TilemapUtils.GetGridWorldPos(backgroundTilemap, CurrentGridX, CurrentGridY);
        }

        CurrentTile = backgroundTilemap.GetTile(CurrentGridX, CurrentGridY);
        if (CurrentTile != null)
        {
            //transform.position = TilemapUtils.GetGridWorldPos(GroundTilemap, TilemapUtils.GetMouseGridX(GroundTilemap, Camera.main), TilemapUtils.GetMouseGridY(GroundTilemap, Camera.main));
        }
        CurrentLocation = transform.position;
    }

    private int MouseGridX()
    {
        Vector2 locPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return Mathf.FloorToInt((locPos.x + Vector2.kEpsilon) / 0.16f);
    }

    private int MouseGridY()
    {
        Vector2 locPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return Mathf.FloorToInt((locPos.y + Vector2.kEpsilon) / 0.16f);
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
