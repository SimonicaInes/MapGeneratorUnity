using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;



public class MapPropertiesSectionLayout : EditorWindow
{
    public LabelTextBox xContainer, yContainer;
    public LabelTextBox mapSeed;
    public int id;
    private VisualElement measurementsContainer;

    VisualElement root;
    public void Init(VisualElement root, int id)
    {
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
        mapSeed = EditorWindow.CreateInstance("LabelTextBox") as LabelTextBox;
        mapSeed.Init("Seed");

        measurementsContainer.Add(mapProperties);
        measurementsContainer.Add(xContainer.GetVisualElement());
        measurementsContainer.Add(yContainer.GetVisualElement());
        mapProperties.Add(measurementsContainer);
        mapProperties.Add(mapSeed.GetVisualElement());
        root.Add(mapProperties);
    }


}
