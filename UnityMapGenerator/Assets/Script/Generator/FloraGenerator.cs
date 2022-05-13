using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class FloraGenerator : ScriptableObject {
    public GameObject parentObject;
    public TerraformingList terraformingList;
    public Hashtable hashmap;
    private Tilemap terrainTileMap;
    private Tilemap floraTileMap;
    private Grid grid;
    private Texture2D floraNoiseMap;
    private TilemapRenderer tilemapRenderer;
    private GameObject tileMapFlora;
    public void Init(GameObject parentObject, TerraformingList terraformingList, Hashtable hashmap, Tilemap terrainTileMap)
    {
        this.parentObject = parentObject;
        this.terraformingList = terraformingList;
        this.hashmap = hashmap;
        this.terrainTileMap = terrainTileMap;
        CreateObjects();
        BuildPerlinNoise();
        BuildFlora();
    }



    private void CreateObjects()
    {
        tileMapFlora = new GameObject("FLORA_Tilemap");
        parentObject.gameObject.AddComponent(typeof(Grid));
        grid = parentObject.gameObject.GetComponent(typeof(Grid)) as Grid;
        grid.cellSize = new Vector3(1,1,0);
        
        tileMapFlora.transform.SetParent(parentObject.transform);
        tileMapFlora.AddComponent(typeof(Tilemap));
      
        floraTileMap = tileMapFlora.gameObject.GetComponent(typeof(Tilemap)) as Tilemap;
 
        tileMapFlora.AddComponent(typeof(TilemapRenderer));
        tilemapRenderer = tileMapFlora.gameObject.GetComponent(typeof(TilemapRenderer)) as TilemapRenderer;
        tilemapRenderer.sortingOrder = 1;
    }


    

    private void BuildPerlinNoise()
    {
        PerlinNoiseGenerator p = new PerlinNoiseGenerator();
        floraNoiseMap = p.GeneratePerlinNoiseMap(terrainTileMap.size.x, terrainTileMap.size.y, 0.51f, 4f);
        floraNoiseMap.filterMode = FilterMode.Point;
        floraNoiseMap.Apply();

               
        Sprite tempSprite = Sprite.Create(floraNoiseMap,
        new Rect(0.0f, 0.0f, floraNoiseMap.width, floraNoiseMap.height),
        new Vector2(0.5f, 0.5f), 200f);

        ((SpriteRenderer)GameObject.FindGameObjectWithTag("Img").GetComponent(typeof(SpriteRenderer))).sprite = tempSprite;
    }

    private void BuildFlora()
    {
        foreach(TerrainChoice tc in terraformingList.terrains)
        {
            
            if(tc.floraCheckbox.checkbox.value)
            {
                ArrayList coordinatesList = GetCoordinatesListOfTerrainType(tc.terrainCodeID);

                foreach(var x in coordinatesList)
                {
                    int[] c = (int[])x;   //   COORDINATE(X,Y) = (c[0],c[1])
                    if(floraNoiseMap.GetPixel(c[0], c[1]) == new Color(1,1,1))
                    {
                        floraTileMap.SetTile(new Vector3Int(c[0], c[1], 1), tc.floraTile);
                    }
                }
            }
        }
    }

    private ArrayList GetCoordinatesListOfTerrainType(int terrainType)
    {
        ArrayList coordinatesList = new ArrayList();
        foreach (DictionaryEntry d in hashmap)
        {
            if ( (((ArrayList)((DictionaryEntry)d).Value).ToArray().Length > 0) && ((int)d.Key == terrainType) )
            {
                coordinatesList = (ArrayList)d.Value;
            }
        }
        return coordinatesList;
    }
}