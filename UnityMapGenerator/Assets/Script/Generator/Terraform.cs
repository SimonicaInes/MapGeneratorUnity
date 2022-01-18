using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
using System.Collections.Generic;




public class Terraform : ScriptableObject
{
    TerraformingList terraformingList;
    TerraformingRules terraformingRules;
    public List<TerrainChoice> terrains = new List<TerrainChoice>();
    Tilemap tileMap;
    private List<Tile> terrainTiles = new List<Tile>();
    Texture2D noiseMap;

    public void Init( TerraformingList terraformingList, TerraformingRules terraformingRules, Tilemap tileMap, Texture2D noiseMap)
    {
        this.noiseMap = noiseMap;
        this.tileMap = tileMap;
        this.terraformingList = terraformingList;
        this.terraformingRules = terraformingRules;
        this.terrains = terraformingList.terrains;
        
        ApplyTerraforming();
    }

    private void ApplyTerraforming()
    {
        SetFirstTypesOfTilesInMap();
        if(terrainTiles[0] != null && terrainTiles[1] != null)
        {
            BuildBaseMap();
        }
        else
        {
            Debug.Log("MISSING! Check if you added at least two terrain tiles and assigned valid tiles!");
        }
        
        //Try and apply rules one at a time. 
        foreach(TerraformRule terraformRule in terraformingRules.terrainRules)
        {
            ApplyRule(terraformRule.dropdownFieldCasesPresets.value, terraformRule.dropdownFieldTileA.value, 
                      terraformRule.dropdownFieldTileB.value, terraformRule.dropdownFieldResultingTile.value);
        }


    }

    private void SetFirstTypesOfTilesInMap()
    {
        foreach(TerrainChoice tc in terrains)
        {
            

            terrainTiles.Add(tc.terrainTile);

        }
    }

    private void BuildBaseMap()
    {
        for(int i=0; i < noiseMap.width; i++)
            {
                for(int j=0; j< noiseMap.height; j++)
                {
                    if( noiseMap.GetPixel(i,j) == Color.black)
                    {
                        tileMap.SetTile(new Vector3Int(i,j,1), terrainTiles[0]); //ground
                        //Debug.Log(baseGroundTile.sprite);

                    }
                    else
                    {
                        tileMap.SetTile(new Vector3Int(i,j,1), terrainTiles[1]);
                    }
                }
            }
    }

    private void ApplyRule(string typeOfRule, string nameOfTileA, string nameOfTileB, string nameOfResultingTile)
    {
        Debug.Log("RULE: " + typeOfRule + "  FROM BASE TILE: " + nameOfTileA + "  WITH NEIGHBOURING TILE: " + nameOfTileB + " WILL RESULT IN TILE: " + nameOfResultingTile);

        Tile tileA =        terrainTiles[terrains.Find(x=> x.terrainName.Equals(nameOfTileA)).terrainCodeID];
        Tile tileB =        terrainTiles[terrains.Find(x=> x.terrainName.Equals(nameOfTileB)).terrainCodeID];
        Tile tileResult =   terrainTiles[terrains.Find(x=> x.terrainName.Equals(nameOfResultingTile)).terrainCodeID];




        int[] coordsX = { -1, 0, 1, -1, 1, -1, 0, 1 };
        int[] coordsY = { -1, -1, -1, 0, 0, 1, 1, 1 };
        switch(typeOfRule)
        {
            case "Any neighbour":
            {
                for(int i=0; i< tileMap.size.x; i++)
                {
                    for( int j = 0; j<tileMap.size.y; j++)
                    {
                        for( int k=0; k< coordsX.Length; k++)
                        {
                            if(tileMap.GetTile(new Vector3Int(i,j,1)) == tileA)
                            {
                                if(tileMap.GetTile(new Vector3Int(i + coordsX[k], j + coordsY[k],1)) == tileB)
                                {
                                    tileMap.SetTile(new Vector3Int(i,j,1), tileResult);
                                    break;
                                }
                            }
                            
                        }
                    }
                }
                break;
            }
            default:
            {
                Debug.Log("Inexistent Rule");
                break;
            }

        }


    }

}