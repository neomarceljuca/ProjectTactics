using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLogic: IComparable<TileLogic>
{
    public Vector3Int pos;

    public Vector3 worldPos;

    public GameObject content;

    public Floor floor;

    public int contentOrder;

    public int movementCost;

    #region pathfinding

    public TileLogic prev;

    public float distance;

    #endregion


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

        movementCost = MovementCost(cellPos, tempFloor);
    }

    public static TileLogic Create(Vector3Int cellPos, Vector3 worldPosition, Floor tempFloor) {
        TileLogic tileLogic = new TileLogic(cellPos, worldPosition, tempFloor);
        return tileLogic;
    }

    private int MovementCost(Vector3Int cellPos,Floor tempFloor) 
    {
         String tileType = floor.tilemap.GetTile(cellPos).name;
        switch (tileType)
        {
            case "sand":
                return 2;
            case "grass":
                return 1;
            default:
                Debug.LogWarning("No registered tile value assigned. Tile received default movementCost 1");
                return 1;
        }
    }


    //IComparer
    public int Compare(TileLogic first, TileLogic second)
    {
        if (first.distance == second.distance)
            return 0;

        if (first.distance < second.distance)
            return -1;

        if (first.distance > second.distance)
            return 1;
        //nunca irá chegar, porém necessário para implementação
        return 0;
    }

    //IComparable
    public int CompareTo(TileLogic other)
    {
        if (this.distance == other.distance)
            return 0;

        if (this.distance < other.distance)
            return -1;

        if (this.distance > other.distance)
            return 1;
        //nunca irá chegar, porém necessário para implementação
        return 0;
    }
}
