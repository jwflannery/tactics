using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tiled2Unity;

public class CameraControls : MonoBehaviour
{

    private TiledMap tileMap;

    private float mapX;
    private float mapY;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    public static Vector3 Up = new Vector3(0, 1, 0);
    public static Vector3 Right = new Vector3(1, 0, 0);
    public static Vector3 Down = new Vector3(0, -1, 0);
    public static Vector3 Left = new Vector3(-1, 0, 0);

    public static float PanSpeed = 3f;

    void Start()
    {

        tileMap = ObjectReferences.Instance.BackgroundTilemap.GetComponent<TiledMap>();
        SetCameraBounds();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            PanCamera(Up, PanSpeed);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            PanCamera(Right, PanSpeed);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            PanCamera(Down, PanSpeed);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            PanCamera(Left, PanSpeed);
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            var direction = -Mathf.Sign(Input.GetAxis("Mouse ScrollWheel"));
            Zoom(direction);
        }
    }

    public void PanCamera(Vector3 direction, float speed)
    {
        Camera.main.transform.position += direction * speed * Time.deltaTime;
        ClampCameraToBounds();
    }
    private void SetCameraBounds()
    {
        var vertExtent = Camera.main.orthographicSize;
        var horzExtent = Camera.main.aspect * vertExtent;

        minX = 0 + horzExtent;
        maxX = tileMap.MapWidthInPixels / 100f - horzExtent;
        minY = -tileMap.MapHeightInPixels / 100f + vertExtent;
        maxY = 0 - vertExtent;
    }


    private void ClampCameraToBounds()
    {
        var v3 = transform.position;
        v3.x = Mathf.Clamp(v3.x, minX, maxX);
        v3.y = Mathf.Clamp(v3.y, minY, maxY);
        transform.position = v3;
    }

    public void Zoom(float direction)
    {
        var testVertExtent = Camera.main.orthographicSize + 0.1f * direction;
        var testHorzExtent = Camera.main.aspect * testVertExtent;

        if (testVertExtent * 2f >= tileMap.MapHeightInPixels / 100f || testHorzExtent * 2f >= tileMap.MapWidthInPixels / 100f)
            return;

        Camera.main.orthographicSize += 0.1f * direction;
        SetCameraBounds();
        ClampCameraToBounds();
    }
}
