using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System;


public class MapPropertiesSectionLayout : EditorWindow
{
    public LabelTextBox xContainer, yContainer;

    public int mapSizeX, mapSizeY;
    public float noiseScale;
    public LabelTextBox noiseScaleContainer;
    public int id;
    private VisualElement measurementsContainer;

    VisualElement root;
    public void Init(VisualElement root, int id)
    {
        mapSizeX =1;
        mapSizeY=1;
        noiseScale = 8.6f;
        this.id = id;
        this.root = root;
        this.CreateGUI();
    }

    private void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement mapProperties = new VisualElement()
        {
            style = 
            {
                flexDirection = FlexDirection.Column,
                borderTopColor = Color.gray,
                borderTopWidth = 0.1f,
                marginBottom = 20,
                paddingBottom = 5,
                paddingTop = 5,
                justifyContent = Justify.SpaceAround
            }
        };
        // VisualElements objects can contain other VisualElement following a tree hierarchy.




        //ROOT
        root.style.flexDirection = FlexDirection.Column;

        //MAPSIZE


         measurementsContainer = new VisualElement()
        {
            style = 
            {
                flexDirection = FlexDirection.Row,
                flexGrow = 1
            }
        };  
        measurementsContainer.Add(new Label("Map size")
            {
                style = 
                {
                    paddingRight = 50
                }
            }
        );

        mapProperties.Add(new Label("Map Properties")
        {
            style=
            {
                paddingBottom = 5,
                paddingTop = 0,
                unityFontStyleAndWeight = FontStyle.Bold
            }
        });
        xContainer = EditorWindow.CreateInstance("LabelTextBox") as LabelTextBox;
        xContainer.Init("X");
        yContainer = EditorWindow.CreateInstance("LabelTextBox") as LabelTextBox;
        yContainer.Init("Y");
        noiseScaleContainer = EditorWindow.CreateInstance("LabelTextBox") as LabelTextBox;
        noiseScaleContainer.Init("Noise Scale");


        //label updates
        xContainer.valueField.RegisterCallback<ChangeEvent<string>>(e =>
        {
            //Debug.Log(labelTextBox.valueField.value.ToString());
            if(xContainer.valueField.value != null)
            {
                try
                {
                    mapSizeX = Int32.Parse(xContainer.valueField.value);
                }
                catch(FormatException)
                {
                    mapSizeX = 1;
                    return;
                }
                
            }
            else
            {
                mapSizeX = 1;
            }

         
        });

        //label updates
        yContainer.valueField.RegisterCallback<ChangeEvent<string>>(e =>
        {
            //Debug.Log(labelTextBox.valueField.value.ToString());
            if(yContainer.valueField.value != null)
            {
                try
                {
                    mapSizeY = Int32.Parse(yContainer.valueField.value);
                }
                catch(FormatException)
                {
                    mapSizeY = 1;
                    return;
                }
                
            }
            else
            {
                mapSizeY = 1;
            }

            
        });

        //label updates
        noiseScaleContainer.valueField.RegisterCallback<ChangeEvent<string>>(e =>
        {
            //Debug.Log( noiseScale);
            if(noiseScaleContainer.valueField.value != null)
            {
                try
                {
                    noiseScale = float.Parse(noiseScaleContainer.valueField.value);
                    
                }
                catch(FormatException)
                {
                    noiseScale = 20f;
                    return;
                }
                
            }
            else
            {
                noiseScale = 8.6f;
            }

            
        });

        measurementsContainer.Add(mapProperties);
        measurementsContainer.Add(xContainer.GetVisualElement());
        measurementsContainer.Add(yContainer.GetVisualElement());
        mapProperties.Add(measurementsContainer);
        mapProperties.Add(noiseScaleContainer.GetVisualElement());
        root.Add(mapProperties);
    }


}
