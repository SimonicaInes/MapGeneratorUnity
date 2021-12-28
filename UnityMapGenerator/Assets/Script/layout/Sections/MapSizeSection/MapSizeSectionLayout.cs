using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public class MapSizeSectionLayout : EditorWindow
{
    public LabelTextBox xContainer, yContainer;
    public int id;

    VisualElement root;
    public MapSizeSectionLayout(VisualElement root, int id)
    {
        this.id = id;
        this.root = root;
        this.CreateGUI();
    }

    private void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement mapSize = new VisualElement()
        {
            style = 
            {
                flexDirection = FlexDirection.Row,
                borderBottomColor = Color.gray,
                borderBottomWidth = 0.1f,
                marginBottom = 20,
                paddingBottom = 5
            }
        };
        VisualElement tileData = new VisualElement();
        VisualElement generateEnvironment = new VisualElement();
        VisualElement ruleSet = new VisualElement();
        // VisualElements objects can contain other VisualElement following a tree hierarchy.

        // ADD VISUAL ELEMENTS TO ROOT
        root.Add(mapSize);

        root.Add(tileData);
        root.Add(generateEnvironment);
        root.Add(ruleSet);
        //END ADDING VE


        //ROOT
        root.style.flexDirection = FlexDirection.Column;

        //MAPSIZE

        mapSize.Add(new Label("Map size:")
            {
                style = 
                {
                    paddingRight = 50
                }
            }
        );
        var measurementsContainer = new VisualElement()
        {
            style = 
            {
                flexDirection = FlexDirection.Row,
                flexGrow = 1
            }
        };  

        xContainer = new LabelTextBox("X");
        yContainer = new LabelTextBox("Y");
        

        measurementsContainer.Add(xContainer.GetVisualElement());
        measurementsContainer.Add(yContainer.GetVisualElement());
        mapSize.Add(measurementsContainer);
    }


}
