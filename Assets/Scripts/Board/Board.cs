﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
 

    //Acesso do mapa:
    //COORDENADAS X e Y: tiles( Vector3(x,y,0) )
    //COORDENADA Z para ALTURA: floor
    public Dictionary<Vector3Int, TileLogic> tiles;
    public List<Floor> floors;
    public static Board instance;

    [HideInInspector]
    public Grid grid;
    public List<Tile> highlights;
    [HideInInspector]
    public Vector3Int[] dirs = new Vector3Int[4] {Vector3Int.up , Vector3Int.down,Vector3Int.left, Vector3Int.right };


    public void Awake()
    {
        tiles = new Dictionary<Vector3Int, TileLogic>();
        instance = this;
        grid = GetComponent<Grid>();
    }

    public IEnumerator InitSequence(LoadState loadState)
    {
        yield return StartCoroutine(LoadFloors(loadState));
        yield return null;
        ShadowOrdering();
        yield return null;
    }

    IEnumerator LoadFloors(LoadState loadState)
    {
        for (int i = 0; i < floors.Count; i++){
            
            List<Vector3Int> floorTiles = floors[i].LoadTiles();
            yield return null;
            for (int j = 0; j< floorTiles.Count; j++){
                if (!tiles.ContainsKey(floorTiles[j])){
                    
                    CreateTile(floorTiles[j], floors[i]);
                }
            }
           
        }

        
    }

    void CreateTile(Vector3Int pos, Floor floor) {
        Vector3 worldPos = grid.CellToWorld(pos);
        worldPos.y += (floor.tilemap.tileAnchor.y/2) ;
        TileLogic tileLogic = new TileLogic(pos,worldPos, floor);
        tiles.Add(pos, tileLogic);
    }

    void ShadowOrdering() {
        foreach (TileLogic t in tiles.Values)
        {
            int floorIndex = floors.IndexOf(t.floor);
            floorIndex -= 2;

            if (floorIndex >= floors.Count || floorIndex < 0)
            {
                continue;
            }
            Floor floorToCheck = floors[floorIndex];

            Vector3Int pos = t.pos;
            IsNECheck(floorToCheck, t, pos+Vector3Int.right);
            IsNECheck(floorToCheck, t, pos + Vector3Int.up);
            IsNECheck(floorToCheck, t, pos + Vector3Int.right + Vector3Int.up) ;

        }

    }

    void IsNECheck(Floor floor, TileLogic t, Vector3Int NEPosition)
    {
        if (floor.tilemap.HasTile(NEPosition)) {
            t.contentOrder = floor.order;
        }
    }

    public static TileLogic GetTile(Vector3Int pos)
    {
        TileLogic tile = null;

        instance.tiles.TryGetValue(pos, out tile);

        return tile;
    }

    public void SelectTiles(List<TileLogic> tiles, int AllianceIndex)
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].floor.highlight.SetTile(tiles[i].pos, highlights[AllianceIndex]);
        }
    }

    public void DeselectTiles(List<TileLogic> tiles)
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].floor.highlight.SetTile(tiles[i].pos, null);
        }
    }

    public List<TileLogic> Search(TileLogic start)
    {
        Movement m = Turn.unit.GetComponent<Movement>();

        List<TileLogic> tilesSearch = new List<TileLogic>();

        tilesSearch.Add(start);

        ClearSearch();

        Queue<TileLogic> checkNext = new Queue<TileLogic>();
        Queue<TileLogic> checkNow = new Queue<TileLogic>();

        start.distance = 0;
        checkNow.Enqueue(start);

        while (checkNow.Count > 0)
        {
            TileLogic t = checkNow.Dequeue();
            for (int i = 0; i < 4; i++)
            {
                TileLogic next = GetTile(t.pos + dirs[i]);
                if(next == null || next.distance <= t.distance + 1 || t.distance + 1 > Turn.unit.GetStat(StatEnum.MOVE) || m.ValidateMovement(t,next))
                {
                    continue;  
                }
                //possivel checagem adicional
                next.distance = t.distance + 1;
                next.prev = t;
                checkNext.Enqueue(next);
                tilesSearch.Add(next);
            }
            if (checkNow.Count == 0)
            {
                SwapReference(ref checkNow, ref checkNext);
            }
        }

        return tilesSearch;
    }

    void SwapReference(ref Queue<TileLogic> now, ref Queue<TileLogic> next)
    {
        Queue<TileLogic> temp = now;
        now = next;
        next = temp;

    }


    void ClearSearch()
    {
        foreach (TileLogic t in tiles.Values)
        {
            t.prev = null;
            t.distance = int.MaxValue;
        }
    }

}
