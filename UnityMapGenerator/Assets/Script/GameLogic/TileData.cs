using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class TileData : MonoBehaviour {
    
    public Tilemap tilemap;
    public TilePropertiesLayout tilePropertiesLayout;
    public int[,] logicMap;
    public List<ExtraTile> extraTiles;
    public void Init(TilePropertiesLayout tilePropertiesLayout, Tilemap tilemap, int[,] logicMap, List<ExtraTile> extraTiles) 
    {
        this.logicMap = logicMap;
        this.tilePropertiesLayout = tilePropertiesLayout;
        this.tilemap = tilemap;
        this.extraTiles = extraTiles;
    }

    private void Start() 
    {

    }

    public ExtraTile GetExtraTile(Tile t)
    {
        return extraTiles.Find(x => x.tile == t);
    }
}