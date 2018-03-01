using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tiled2Unity;
using CreativeSpore.SuperTilemapEditor;

public class CameraControls : MonoBehaviour
{

    private GameObject tileMap;

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
        var vertExtent = Camera.main.orthographicSize;
        var horzExtent = Camera.main.aspect * vertExtent;

        tileMap = ObjectReferences.Instance.BackgroundTilemap;

        minX = 0 + horzExtent;
        maxX = tileMap.GetComponent<TiledMap>().MapWidthInPixels/100 - horzExtent;
        minY = -tileMap.GetComponent<TiledMap>().MapHeightInPixels/100 + vertExtent;
        maxY = 0 - vertExtent;        
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

        var v3 = transform.position;
        v3.x = Mathf.Clamp(v3.x, minX, maxX);
        v3.y = Mathf.Clamp(v3.y, minY, maxY);
        transform.position = v3;
    }

    public void Zoom(float direction)
    {
        Camera.main.orthographicSize += 0.1f * direction;
    }
}
