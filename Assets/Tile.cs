using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public string TileName;

    public Tile (string tileName)
    {
        this.TileName = tileName;
    }

    public void Start()
    {
        MapDetails.TileDatas.Add(this);
    }

    public void AttachToMap()
    {
        MapDetails.GetTileDetails(MapUtils.GetGridX(transform.localPosition), MapUtils.GetGridY(transform.localPosition)).ParseTileData(this);
    }
}
