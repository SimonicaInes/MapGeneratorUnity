using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;



public class MapGenerator : ScriptableObject
{
    Grid grid;
    GameObject rootObject;
    GameObject tileMapObject;
    Tilemap tileMap;
    TilemapRenderer tilemapRenderer;
    Texture2D noiseMap;

    TerraformingLayout terraformingLayout;
    Terraform terraform;



    public void Init(TerraformingLayout terraformingLayout)
    {
        //DEBUG TILES
        Debug.Log("MAP!");
        this.terraformingLayout = terraformingLayout;

    
        //END DEBUG TILES


        GenerateGameObject();
        InitializeMap(tileMap, 40, 40);
        BuildPerlinNoise();
        //BuildMap(noiseMap,t1,t2);

        // TERRAFORM
        ApplyTerraform();
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
                tileMap.SetTile(new Vector3Int(i,j,1), t);
            }
        }
    }


    private void BuildPerlinNoise()
    {
        PerlinNoiseGenerator p = new PerlinNoiseGenerator();
        noiseMap = p.GeneratePerlinNoiseMap(40, 40, 0.51f, 8.6f);
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


}

