using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateMap : MonoBehaviour
{
    public Tilemap MainTilemap;
    public Tilemap[] MapParts;
    int NextPartID;
    public int partcounter = 1;
    public Transform playertransform;
    private void Start()
    {
        //NextPartID = Random.Range(0, MapParts.Length);
        //CreateMapPart(MapParts[NextPartID], MainTilemap);
    }
    private void Update()
    {
        if ((partcounter * 19 - 19) < playertransform.position.x && playertransform.position.x < (partcounter * 19 ))
        {
            NextPartID = Random.Range(0, MapParts.Length);
            CreateMapPart(MapParts[NextPartID], MainTilemap);
        }
    }
    
    void CreateMapPart(Tilemap ScrTilemap, Tilemap DestTilemap)
    {
        int Destposx;
        int Destposy;
        int xdistance = 0 - ScrTilemap.cellBounds.min.x; 
        int ydistance = (-10) - ScrTilemap.cellBounds.min.y;
        
        //Create new map part-start
        for (int Srcposx = ScrTilemap.cellBounds.min.x; Srcposx < ScrTilemap.cellBounds.max.x; Srcposx++)
        {
            for (int Srcposy = ScrTilemap.cellBounds.min.y; Srcposy < ScrTilemap.cellBounds.max.y; Srcposy++)
            {   
                Destposx = Srcposx + xdistance + (partcounter * 19);
                Destposy = Srcposy + ydistance;

                Vector3Int SrcPos = new Vector3Int(Srcposx, Srcposy, 0);
                Vector3Int DestPos = new Vector3Int(Destposx, Destposy, 0);

                var Tile = ScrTilemap.GetTile(SrcPos);
                DestTilemap.SetTile(DestPos, Tile);
            }
        }
        partcounter++;
    }
}
