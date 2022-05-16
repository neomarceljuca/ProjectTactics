using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLogic
{
    public Vector3Int pos;

    public Vector3 worldPos;

    public GameObject content;

    public Floor floor;

    public int contentOrder;

    #region pathfinding

    public TileLogic prev;

    public float distance;

    #endregion

    //public TileType tileType;    will do later


    //Could use to implement cell destruction
    //[HideInInspector]
    //public int hp;

    //constructors

    public TileLogic() { }

    public TileLogic(Vector3Int cellPos, Vector3 worldPosition, Floor tempFloor) {
        pos = cellPos;
        worldPos = worldPosition;
        floor = tempFloor;
        contentOrder = tempFloor.contentOrder;
    }

    public static TileLogic Create(Vector3Int cellPos, Vector3 worldPosition, Floor tempFloor) {
        TileLogic tileLogic = new TileLogic(cellPos, worldPosition, tempFloor);
        return tileLogic;
    }
}
