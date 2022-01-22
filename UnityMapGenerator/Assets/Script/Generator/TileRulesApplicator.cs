using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine.Tilemaps;


public class TileRulesApplicator : ScriptableObject 
{

    private int[,] logicMap;
    private TerraformingList terraformingList;
    private TileRulesLayout tileRulesLayout;

    private Hashtable hashmap;
    private int[] terrainIDs;
    private Tilemap tilemap ; 

    const int NO_NEIGHBOUR = 0;
    const int HAS_NEIGHBOUR = 1;
    const int ANY_NEIGHBOUR = 2;

     public void Init(int[,] logicMap, TerraformingList terraformingList, TileRulesLayout tileRulesLayout, Tilemap tilemap )
     {
        this.tilemap = tilemap;
        this.tileRulesLayout = tileRulesLayout;
        this.terraformingList = terraformingList;
        this.logicMap = logicMap;
        terrainIDs = new int[terraformingList.terrains.Count];
        ReadExistentTerrains();
        PopulateHashmap();
        ApplyRuleSets();
     }



    private void PopulateHashmap()
    {
        hashmap = new Hashtable();
            Debug.Log("len " + terrainIDs.Length);

        for(int k = 0; k < terrainIDs.Length; k++)
        {
            hashmap.Add(k, new ArrayList());
        }

        
        for(int i=0; i < logicMap.GetLength(0); i++)
        {
            for(int j=0; j< logicMap.GetLength(1); j++)
            {            
                int[] a = {i, j};
                ((ArrayList) hashmap[logicMap[i, j]]).Add(a);
                // Debug.Log("--------------verde " + logicMap[i, j] + ", " + a.GetValue(0));

            }
        }


        //-------  PRINTING HASHTABLE --------

        // foreach (DictionaryEntry d in hashmap)
        // {
        //     Debug.Log("--------------key " + ((DictionaryEntry)d).Key.ToString());
        //     if(((ArrayList)((DictionaryEntry)d).Value).ToArray().Length > 0)
        //     {
        //         ArrayList a = (ArrayList)d.Value;
        //         foreach(var x in a)
        //         {
        //             int[] b = (int[])x;
        //             Debug.Log("val " + b[0] + ", " + b[1]);
        //         }
        //     }
        // }

    }

    private void ReadExistentTerrains()
    {
        foreach(TerrainChoice tc in terraformingList.terrains)
        {
            //we could also just fill in the array after counting the elements of tc, considering that terrainIDs[i] = i always. Not sure which one would be faster.
            terrainIDs[tc.terrainCodeID] = tc.terrainCodeID; 
        }

    }


    private void ApplyRuleSets()
    {
        ArrayList coordinatesList = null;
        foreach(RuleSet rs in tileRulesLayout.tileRuleSets)
        {
            //We are now looking in a rule set.
            //We read the original and resulting tile types:   so if any name from the list of existing terrains coincides with the dropdown choice,
            // we collect and store the id.
            int originalTerrainType =  (terraformingList.terrains.Find(x => x.terrainName == rs.originalTerrainType.value)).terrainCodeID;
            int neighbouringTerrainType =  (terraformingList.terrains.Find(x => x.terrainName == rs.resultingTerrainType.value)).terrainCodeID;
            
            //we save in an arrayList the coordinates of all tiles of originalTerraintype from the hashtable
            foreach (DictionaryEntry d in hashmap)
            {
                if ( (((ArrayList)((DictionaryEntry)d).Value).ToArray().Length > 0) && ((int)d.Key == originalTerrainType) )
                {
                     coordinatesList = (ArrayList)d.Value;
                    // foreach(var x in coordinatesList)
                    // {
                    //     int[] b = (int[])x;
                    //     //Debug.Log("val " + b[0] + ", " + b[1]);
                    // }
                }
            }

            foreach(TileRuleBox tileRuleBox in rs.tileRules)
            {
               //we read each rule box in the rule set.
               //READ RULE
                int[] tableStates = new int[8];
                tableStates = ReadTableRule(tileRuleBox);

                //coordsX and Y are ordered in such a way such that (coordsX[0],coordsY[0]) will be in the same spot as the state tableStates[0] 
                int[] coordsX = { -1, 0, 1, -1, 1, -1, 0, 1 };
                int[] coordsY = { -1, 1, 1, 0, 0, -1, -1, -1 };
                
                
               //APPLY RULE
                foreach(var x in coordinatesList)
                {
                    bool passedConditions = true;
                    int[] c = (int[])x;   //   COORDINATE(X,Y) = (c[0],c[1])
                    for(int i=0; i< tableStates.Length; i++)
                    {
                        if(c[0] + coordsX[i] < logicMap.GetLength(0) && 
                        c[1] + coordsY[i] < logicMap.GetLength(1) &&
                        c[0] + coordsX[i] >= 0 &&
                        c[1] + coordsY[i] >= 0)
                        {
                            switch(tableStates[i])
                            {
                                case NO_NEIGHBOUR:
                                {
                                    if(logicMap[c[0] + coordsX[i], c[1] + coordsY[i]] == neighbouringTerrainType)
                                    {
                                        
                                        passedConditions = false;
                                        break;
                                    }        
                                    break;
                                }
                                case HAS_NEIGHBOUR:
                                {
                                    if(logicMap[c[0] + coordsX[i], c[1] + coordsY[i]] != neighbouringTerrainType)
                                    {
                                        passedConditions = false;
                                        break;  
                                    }
                                    break;
                                }
                                case ANY_NEIGHBOUR:
                                {
                                    break;
                                }
                                default:
                                {
                                    break;
                                }
                            } 
                        }
                        
                    }
                    if(passedConditions)
                    {
                        Debug.Log("PASSED!: " + c[0] + " ; " + c[1]);
                        tilemap.SetTile(new Vector3Int(c[0],c[1],1), (Tile)tileRuleBox.objectPicker.objectField.value);
                    }


                }

                
               //REPLACE TILE
            }

        }
    }


    private int[] ReadTableRule(TileRuleBox tileRuleBox)
    {
        int[] tableStates = new int[8];
        int i=0;
        foreach( TableButtonElement e in tileRuleBox.ruleTable.t)
        {
            if(e != null)
            {
                tableStates[i] = e.state;
                i++;
            }
            
        }
        // foreach(int x in tableStates)
        // {
        //     Debug.Log(x);
        // }
        return tableStates;
    }

}