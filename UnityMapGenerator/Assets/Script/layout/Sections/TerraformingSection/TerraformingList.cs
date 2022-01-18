using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;


public class TerraformingList : Editor 
{
    
    public VisualElement mainContainer;
    public VisualElement listContainer;

    public Button addTerrainButton;
    public DeleteChildEvent eventListener;
    public List<TerrainChoice> terrains = new List<TerrainChoice>();

    //public List<string> terrainsStringList = new List<string>();
    // private Color darkGray = new Color(0.12f, 0.12f, 0.12f);
    //     private float borderWidth = 2f;

    private string[] s = new string[200];
    public void Init()
    {


        this.eventListener = new DeleteChildEvent();
        eventListener.AddListener(buttonListener);
        CreateGUI();
    }

    public void CreateGUI()
    {
        mainContainer = new VisualElement()
        {
            style=
            {
                flexDirection = FlexDirection.Column,

            }
        };
        listContainer = new VisualElement()
        {
            style=
            {
                flexDirection = FlexDirection.Column,
            //     borderBottomColor = darkGray,
            //     borderLeftColor = darkGray,
            //     borderRightColor = darkGray,
            //     borderTopColor = darkGray,
            //     borderBottomWidth = borderWidth,
            //     borderLeftWidth = borderWidth,
            //     borderRightWidth = borderWidth,
            //     borderTopWidth = borderWidth,
            }
        };
        addTerrainButton = new Button();
        addTerrainButton.text = "Add Terrain type";

        mainContainer.Add(addTerrainButton);
        mainContainer.Add(listContainer);
        addTerrainButton.clickable.clicked += () =>
        {
            TerrainChoice tc = EditorWindow.CreateInstance("TerrainChoice") as TerrainChoice;
            tc.Init(terrains.Count, eventListener);
            terrains.Add(tc);
            listContainer.Add(tc.GetVisualElement());
            //terrainsStringList.Add(terrains[terrains.Count-1].terrainName);
            
           
        };
        
    }

    public VisualElement GetVisualElement()
    {
        return this.mainContainer;
    }
    private void buttonListener(int id)
    {
        DestroyImmediate(terrains.Find(item => item.terrainCodeID == id), true);
        terrains.Remove(terrains.Find(item => item.terrainCodeID == id));
        
        redrawList();
    }

    private void redrawList()
    {
        while (listContainer.childCount > 0)
        {
            listContainer.RemoveAt(listContainer.childCount - 1);
            
        }

        int index = 0;
        foreach(TerrainChoice t in terrains)
        {
            listContainer.Add(terrains[index++].GetVisualElement());
            t.terrainCodeID = index-1;
           // Debug.Log(t.terrainCodeID);
        }


    }

    public TerrainChoice GetTerrainChoice(string s)
    {
        TerrainChoice tc;
        tc = terrains.Find(x => x.terrainName.Equals(s));
        return tc;
    }
}