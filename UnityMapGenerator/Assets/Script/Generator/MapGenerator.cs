using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
using System.Collections.Generic;



public class MapGenerator : ScriptableObject
{
    private Grid grid;
    private GameObject rootObject;
    private GameObject tileMapObject;
    private Tilemap tileMap;
    private TilemapRenderer tilemapRenderer;
    private Texture2D noiseMap;

    private TerraformingLayout terraformingLayout;
    private MapPropertiesSectionLayout mapPropertiesSectionLayout;
    private TileRulesLayout tileRulesLayout;
    private Terraform terraform;



    public void Init(TerraformingLayout terraformingLayout, MapPropertiesSectionLayout mapPropertiesSectionLayout, TileRulesLayout tileRulesLayout)
    {
        this.tileRulesLayout = tileRulesLayout;
        this.terraformingLayout = terraformingLayout;
        this.mapPropertiesSectionLayout = mapPropertiesSectionLayout;
    
        //END DEBUG TILES


        GenerateGameObject();
        InitializeMap(tileMap, mapPropertiesSectionLayout.mapSizeX, mapPropertiesSectionLayout.mapSizeY);
        BuildPerlinNoise(mapPropertiesSectionLayout.mapSizeX, mapPropertiesSectionLayout.mapSizeY,  mapPropertiesSectionLayout.noiseScale);

        // TERRAFORM
        ApplyTerraform();

        //BUILD
        BuildTileMap();
        
        //TILE RULES
        ApplyTileRules();


        
    }

    private void GenerateGameObject()
    {
        //Scene currentScene = SceneManager.GetActiveScene();
        //grid.gameObject.AddComponent
        rootObject = new GameObject("root");
        tileMapObject = new GameObject("tileMap");

        rootObject.gameObject.AddComponent(typeof(Grid));
        grid = rootObject.gameObject.GetComponent(typeof(Grid)) as Grid;
        grid.cellSize = new Vector3(1,1,0);
        
        tileMapObject.transform.SetParent(rootObject.transform);
        tileMapObject.AddComponent(typeof(Tilemap));
        tileMap = tileMapObject.gameObject.GetComponent(typeof(Tilemap)) as Tilemap;

        tileMapObject.AddComponent(typeof(TilemapRenderer));
        tilemapRenderer = tileMapObject.gameObject.GetComponent(typeof(TilemapRenderer)) as TilemapRenderer;

    }


    private void InitializeMap(Tilemap tileMap, int width, int height)
    {
        Tile t = ScriptableObject.CreateInstance("Tile") as Tile;
        t.sprite= (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Resources/Tiles/DefaultSprite.png", typeof(Sprite));
        //Debug.Log(t.sprite);
        tileMap.size = new Vector3Int(width, height, 0);

        //debug fill
        for( int i=0; i < width; i++)
        {
            for( int j=0; j< height; j++)
            {
                tileMap.SetTile(new Vector3Int(i,j,1), t);//asd
            }
        }
    }


    private void BuildPerlinNoise(int width, int height, float scale)
    {
        PerlinNoiseGenerator p = new PerlinNoiseGenerator();
        noiseMap = p.GeneratePerlinNoiseMap(width, height, 0.51f, scale);
        noiseMap.filterMode = FilterMode.Point;
        noiseMap.Apply();

               
        Sprite tempSprite = Sprite.Create(noiseMap,
        new Rect(0.0f, 0.0f, noiseMap.width, noiseMap.height),
        new Vector2(0.5f, 0.5f), 200f);

        ((SpriteRenderer)GameObject.FindGameObjectWithTag("Img").GetComponent(typeof(SpriteRenderer))).sprite = tempSprite;
    }



//TERRAFORMING PROCESS
    private void ApplyTerraform()
    {
        terraform = ScriptableObject.CreateInstance("Terraform") as Terraform;
        terraform.Init(terraformingLayout.terraformingList, terraformingLayout.terraformingRules, tileMap, noiseMap);
    }


//APPLYING TILE RULES
    private void ApplyTileRules()
    {
            int[,] logicMap = terraform.logicMap;
            TileRulesApplicator tileRulesApplicator = ScriptableObject.CreateInstance("TileRulesApplicator") as TileRulesApplicator;
            TerraformingList terraformingList = terraformingLayout.terraformingList;
            tileRulesApplicator.Init(logicMap, terraformingList, tileRulesLayout, tileMap);
    }

// BUILD TILEMAP
    private void BuildTileMap()
    {
        for( int i = 0; i < terraform.logicMap.GetLength(0); i++)
        {
            for( int j = 0; j < terraform.logicMap.GetLength(1); j++)
            {
                tileMap.SetTile(new Vector3Int(i, j, 1), terraform.terrains[terraform.logicMap[i,j]].terrainTile);
            }            
        }
    }

}

