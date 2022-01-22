using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System;



public class Terraform : ScriptableObject
{
    TerraformingList terraformingList;
    TerraformingRules terraformingRules;
    public List<TerrainChoice> terrains = new List<TerrainChoice>();
    Tilemap tileMap;
    //private List<Tile> terrainTiles = new List<Tile>();
    Texture2D noiseMap;
    public int[,] logicMap;

    public void Init( TerraformingList terraformingList, TerraformingRules terraformingRules, Tilemap tileMap, Texture2D noiseMap)
    {
        
        this.noiseMap = noiseMap;
        logicMap = new int[noiseMap.width, noiseMap.height];
        this.tileMap = tileMap;
        this.terraformingList = terraformingList;
        this.terraformingRules = terraformingRules;
        this.terrains = terraformingList.terrains;
        
        ApplyTerraforming();
    }

    private void ApplyTerraforming()
    {
        //SetFirstTypesOfTilesInMap();
        if(terrains[0].terrainTile != null && terrains[1].terrainTile != null)
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

        //APPLY ALL ON TILEMAP;

        //PrintLogicMap();

    }

    private void SetFirstTypesOfTilesInMap()
    {
        foreach(TerrainChoice tc in terrains)
        {
            
            
            //terrainTiles.Add(tc.terrainTile);

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
                        logicMap[i,j] = terrains[0].terrainCodeID;
                        //tileMap.SetTile(new Vector3Int(i,j,1), terrainTiles[0]); //ground
                        //Debug.Log(baseGroundTile.sprite);

                    }
                    else
                    {
                        logicMap[i,j] = terrains[1].terrainCodeID;
                        //tileMap.SetTile(new Vector3Int(i,j,1), terrainTiles[1]);
                    }
                }
            }
    }

    private void ApplyRule(string typeOfRule, string nameOfTileA, string nameOfTileB, string nameOfResultingTile)
    {
        Debug.Log("RULE: " + typeOfRule + "  FROM BASE TILE: " + nameOfTileA + "  WITH NEIGHBOURING TILE: " + nameOfTileB + " WILL RESULT IN TILE: " + nameOfResultingTile);

        int tileA =        terrains.Find(x=> x.terrainName.Equals(nameOfTileA)).terrainCodeID;
        int tileB =        terrains.Find(x=> x.terrainName.Equals(nameOfTileB)).terrainCodeID;
        int tileResult =   terrains.Find(x=> x.terrainName.Equals(nameOfResultingTile)).terrainCodeID;



        switch(typeOfRule)
        {
            case "Any neighbour":
            {
                int[] coordsX = { -1, 0, 1, -1, 1, -1, 0, 1 };
                int[] coordsY = { -1, -1, -1, 0, 0, 1, 1, 1 };
                ApplyAnyNeighbourRule(coordsX, coordsY, tileA, tileB, tileResult);
                break;
            }
            case "All neighbours":
            {
                int[] coordsX = { -1, 0, 1, -1, 1, -1, 0, 1 };
                int[] coordsY = { -1, -1, -1, 0, 0, 1, 1, 1 };
                ApplyMandatoryNeighboursRule(coordsX, coordsY, tileA, tileB, tileResult);
                break;               
            }

            case "Any corner neighbour":
            {
                int[] coordsX = { -1, -1, 1, 1 };
                int[] coordsY = { -1, 1, 1, -1 };
                ApplyAnyNeighbourRule(coordsX, coordsY, tileA, tileB, tileResult);
                break;
            }
            case "All corner neighbours":
            {   
                
                int[] coordsX = { -1, -1, 1, 1 };
                int[] coordsY = { -1, 1, 1, -1 };
                ApplyMandatoryNeighboursRule(coordsX, coordsY, tileA, tileB, tileResult);
                break;
            }
            case "Any cross neighbour":
            {
                int[] coordsX = { 0, 1, 0, -1};
                int[] coordsY = { 1, 0, -1, 0};
                ApplyAnyNeighbourRule(coordsX, coordsY, tileA, tileB, tileResult);
                break;
            }
            case "All cross neighbours":
            {
                int[] coordsX = { 0, 1, 0, -1};
                int[] coordsY = { 1, 0, -1, 0};
                ApplyMandatoryNeighboursRule(coordsX, coordsY, tileA, tileB, tileResult);
                break;               
            }

            default:
            {
                Debug.Log("Inexistent Rule");
                break;
            }

        }


    }

    private void ApplyAnyNeighbourRule(int[] coordsX, int[] coordsY, int tileA, int tileB, int tileResult)
    {
        int [,] newLogicMap = new int[logicMap.GetLength(0),logicMap.GetLength(1)];
        Array.Copy(logicMap, newLogicMap, logicMap.Length);

        for(int i = 0; i < logicMap.GetLength(0); i++)
        {
            for( int j = 0; j < logicMap.GetLength(1); j++)
            {
                for( int k=0; k< coordsX.Length; k++)
                {
                    if(logicMap[i,j] == tileA)
                    {
                        if(i + coordsX[k] < logicMap.GetLength(0) && 
                            j + coordsY[k] < logicMap.GetLength(1) &&
                            i + coordsX[k] >= 0 &&
                            j + coordsY[k] >= 0)
                        {
                            //Debug.Log(" i + coordsX[k] :  " + (i + coordsX[k]));
                            //Debug.Log(" j + coordsY[k] :  " + (j + coordsY[k]));
                            if(logicMap[i + coordsX[k], j + coordsY[k]] == tileB)
                            {
                                newLogicMap[i,j] = tileResult;
                                //newTileMap.SetTile(new Vector3Int(i,j,1), tileResult);
                                break;
                            }                            
                        }

                    }
                }
            }
        }
        Array.Copy(newLogicMap, logicMap, logicMap.Length);
    }

    private void ApplyMandatoryNeighboursRule(int[] coordsX, int[] coordsY, int tileA, int tileB, int tileResult)
    {
        int [,] newLogicMap = new int[logicMap.GetLength(0), logicMap.GetLength(1)];
        Array.Copy(logicMap, newLogicMap, logicMap.Length);

        for(int i=0; i< logicMap.GetLength(0); i++)
        {
            for( int j = 0; j<logicMap.GetLength(1); j++)
            {
                bool conditionMet = true;

                    if(logicMap[i,j] == tileA)
                    {
                        for( int k=0; k< coordsX.Length; k++)
                        {
                            if(i + coordsX[k] < logicMap.GetLength(0) && 
                                j + coordsY[k] < logicMap.GetLength(1) &&
                                i + coordsX[k] >= 0 &&
                                j + coordsY[k] >= 0)
                            {
                                if(logicMap[i + coordsX[k], j + coordsY[k]] != tileB)
                                {
                                    //Debug.Log("Neighbour isn't of TILE B ");
                                    conditionMet = false;
                                    break;
                                
                                }
                            }                            

                        }
                        if(conditionMet)
                        {
                            newLogicMap[i,j] = tileResult;
                        }
                    }
                

            }

        }
        Array.Copy(newLogicMap, logicMap, logicMap.Length);

    }



    private void PrintLogicMap()
    {
        string row = ""; 
        string column = ""; 
        
        for( int i = 0; i < logicMap.GetLength(1); i++)
        {

            for( int j = 0; j < logicMap.GetLength(0); j++)
            {
                row += "  " + logicMap[i,j];
            }
            column += row + " /n";
            
        }    

        Console.WriteLine(column);
        
    }

}