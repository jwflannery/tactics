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
            CurrentGridX = MapUtils.GetMouseGridX();
            CurrentGridY = MapUtils.GetMouseGridY();
            CurrentTile = backgroundTilemap.GetTile(CurrentGridX, CurrentGridY);
            if (CurrentTile != null)
            {
                transform.position = MapUtils.GetGridWorldPos(MapUtils.GetMouseGridX(), MapUtils.GetMouseGridY(), ObjectReferences.CellSize);
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
            transform.position = MapUtils.GetGridWorldPos(CurrentGridX, CurrentGridY, ObjectReferences.CellSize);
        }

        CurrentTile = backgroundTilemap.GetTile(CurrentGridX, CurrentGridY);
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
