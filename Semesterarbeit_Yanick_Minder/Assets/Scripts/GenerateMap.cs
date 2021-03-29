using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateMap : MonoBehaviour
{
    public Tilemap MainTilemap;
    public Tilemap[] MapParts;
    List<Tilemap> order = new List<Tilemap>();
    int NextPart;
    public Transform playertransform;
    private void Start()
    {
        Debug.Log(order.Count);
        CreateMapPart(MapParts[0], MainTilemap, order.Count);
    }
    private void Update()
    {
        if ((order.Count * 19 - 19) < playertransform.position.x && playertransform.position.x < (order.Count * 19 ))
        {
            NextPart = Random.Range(0, MapParts.Length);
            CreateMapPart(MapParts[NextPart], MainTilemap, order.Count);
        }
    }

    void CreateMapPart(Tilemap ScrTilemap, Tilemap DestTilemap, int partparam)
    {
        int part;
        int Destposx;
        int Destposy;
        int xdistance = 0 - ScrTilemap.cellBounds.min.x; 
        int ydistance = (-10) - ScrTilemap.cellBounds.min.y; 

        
        part = order.Count + 1;
        
        
        
        

        //Create new map part-start
        for (int Srcposx = ScrTilemap.cellBounds.min.x; Srcposx < ScrTilemap.cellBounds.max.x; Srcposx++)
        {
            for (int Srcposy = ScrTilemap.cellBounds.min.y; Srcposy < ScrTilemap.cellBounds.max.y; Srcposy++)
            {   
                Destposx = Srcposx + xdistance + (part * 19);
                Destposy = Srcposy + ydistance;

                Vector3Int SrcPos = new Vector3Int(Srcposx, Srcposy, 0);
                Vector3Int DestPos = new Vector3Int(Destposx, Destposy, 0);

                var Tile = ScrTilemap.GetTile(SrcPos);
                DestTilemap.SetTile(DestPos, Tile);
            }
        }
        //Create new map part-end
        order.Add(ScrTilemap);
    }
    void CreateRandom()
    {
        NextPart = Random.Range(0, MapParts.Length);
        CreateMapPart(MapParts[NextPart], MainTilemap, order.Count + 1);
    }
}
