using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

using System.Collections.Generic;

public class TilePropertiesLayout : EditorWindow
{

    public DeleteChildEvent testEventListener;

    public int id;
    public VisualElement root;
    public VisualElement mainContainer;
    public VisualElement firstContainer;
    public Button addTileProperty;
    public List<TilePropertyBox> tileProperties = new List<TilePropertyBox>();
    private int i = 0;
    private List<string> terrainsStrings = new List<string>();
    private TerraformingList terraformingList;

    public void Init(VisualElement root, int id, TerraformingList terraformingList)
    {
        this.terraformingList = terraformingList;
        this.id = id;
        this.root = root;
        this.testEventListener = new DeleteChildEvent();
        testEventListener.AddListener(buttonListener);
        this.CreateGUI();

    }


    private void CreateGUI()
    {

        Button refreshButton = new Button();
        refreshButton.text = "REFRESH DROPDOWN LISTS";
        refreshButton.clickable.clicked += () =>
        {
            RefreshStringList();
        };


        mainContainer = new VisualElement()
        {
            style = 
            {
                flexDirection = FlexDirection.Column,
                borderTopColor = Color.gray,
                borderTopWidth = 0.1f,
                marginBottom = 20,
                paddingBottom = 5,
                paddingTop = 0
     
            }
        }; 

        firstContainer = new VisualElement()
        {
            style = 
            {
                flexDirection = FlexDirection.Column,
                paddingTop = 5,
                paddingBottom = 5  
            }
        }; 

        addTileProperty = new Button ()
        {
            text = "Add Property",
            style = 
            {
                
            }
        };
        root.Add(mainContainer);
        mainContainer.Add(new Label("Tile Properties")
        {
            style=
            {
                paddingBottom = 5,
                paddingTop = 2,
                unityFontStyleAndWeight = FontStyle.Bold
            }
        });
        mainContainer.Add(refreshButton);
        mainContainer.Add(addTileProperty);
        mainContainer.Add(firstContainer);
        addTileProperty.clickable.clicked += () =>
        {
            RefreshStringList();
            TilePropertyBox t = EditorWindow.CreateInstance("TilePropertyBox") as TilePropertyBox;
            t.Init(root, i, testEventListener, terrainsStrings);
            tileProperties.Add(t);
            firstContainer.Add(tileProperties[tileProperties.Count-1].GetVisualElement());
            i++;           
            
        };



    }
//
    private void buttonListener(int id)
    {
        DestroyImmediate(tileProperties.Find(item => item.id == id), true);
        tileProperties.Remove(tileProperties.Find(item => item.id == id));
        redrawTileProperties();
        RefreshStringList();
    }

    private void redrawTileProperties()
    {
        while (firstContainer.childCount > 0)
        {
            firstContainer.RemoveAt(firstContainer.childCount - 1);
        }

        int index = 0;
        foreach(TilePropertyBox t in tileProperties)
        {
            firstContainer.Add(tileProperties[index++].GetVisualElement());//
        }
    }

     public void RefreshStringList()
    {
        terrainsStrings.Clear();
        foreach(TerrainChoice tc in terraformingList.terrains)
        {
            terrainsStrings.Add(tc.terrainName);
        }

    }
}

public class DeleteChildEvent : UnityEvent<int>
{

}