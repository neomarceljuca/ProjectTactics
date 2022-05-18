using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Blockers : MonoBehaviour
{
    public static Blockers instance;

    void Awake()
    {
        instance = this;
        GetComponent<TilemapRenderer>().enabled = false;
    }


    public List<Vector3Int> GetBlockers() 
    {
        Tilemap tilemap = GetComponent<Tilemap>();
        List<Vector3Int> blockeds = new List<Vector3Int>();

        BoundsInt bounds = tilemap.cellBounds;

        foreach (var pos in bounds.allPositionsWithin) 
        {
            if (tilemap.HasTile(pos)) 
            {
                blockeds.Add(new Vector3Int(pos.x, pos.y, 0));
                //Debug.LogFormat("Tile ({0},{1}) está bloqueado ", pos.x, pos.y);
            }

        }

        return blockeds;
    }
}
