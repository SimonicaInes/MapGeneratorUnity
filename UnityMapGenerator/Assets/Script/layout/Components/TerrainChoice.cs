using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEngine.Tilemaps;


public class TerrainChoice : EditorWindow
{

    public VisualElement mainContainer;
    public VisualElement rowContainer1;
    public VisualElement rowContainer2;
    public int terrainCodeID;
    public ObjectField objectField;

    public Tile terrainTile;
    public Tile floraTile;
    public string terrainName;
    public DeleteChildEvent evt;
    public Button deleteTerrainButton;

    public ObjectField floraTileObjectField;
    public LabelCheckbox floraCheckbox;


    public void Init(int id, DeleteChildEvent evt)
    {
        terrainTile = null;
        this.terrainCodeID = id;
        CreateGUI();
        this.evt = evt;

    }

    private void CreateGUI()
    {
        mainContainer = new VisualElement()
        {
            style=
            {
                flexDirection = FlexDirection.Column

            }
        };
        rowContainer1 = new VisualElement()
        {
            style=
            {
                flexDirection = FlexDirection.Row

            }
        };
        rowContainer2 = new VisualElement()
        {
            style=
            {
                flexDirection = FlexDirection.Row

            }
        };
       objectField =  new ObjectField()
        {
            style=
            {
                flexGrow = 2,
                alignContent = Align.Center,
                maxWidth = 130,
            }
        };
        objectField.objectType = typeof(Tile);

       floraTileObjectField =  new ObjectField()
        {
            style=
            {
                flexGrow = 2,
                alignContent = Align.Center,
                maxWidth = 130,
            }
        };
        floraTileObjectField.objectType = typeof(Tile);

        floraTileObjectField.RegisterCallback<ChangeEvent<UnityEngine.Object>>(e =>
        {
            if (floraTileObjectField.value != null)
            {
                // 4
                //Debug.Log("picked smth new");
                floraTile = (Tile)floraTileObjectField.value;
                //Debug.Log(terrainTile.name);
            }
            else
            {
                //Debug.Log("nah");
                floraTile = null;
            }

                    
        });
        objectField.RegisterCallback<ChangeEvent<UnityEngine.Object>>(e =>
        {
            if (objectField.value != null)
            {
                // 4
                //Debug.Log("picked smth new");
                terrainTile = (Tile)objectField.value;
                //Debug.Log(terrainTile.name);
            }
            else
            {
                //Debug.Log("nah");
                terrainTile = null;
            }

                    
        });

        floraCheckbox = EditorWindow.CreateInstance("LabelCheckbox") as LabelCheckbox;
        floraCheckbox.Init("Flora");

        LabelTextBox labelTextBox = EditorWindow.CreateInstance("LabelTextBox") as LabelTextBox;
        labelTextBox.Init("Name");

        //terrain name updates upon change.
        labelTextBox.valueField.RegisterCallback<ChangeEvent<string>>(e =>
        {
            //Debug.Log(labelTextBox.valueField.value.ToString());
            terrainName = labelTextBox.valueField.value.ToString();
        });

        rowContainer1.Add(labelTextBox.GetVisualElement());
        rowContainer1.Add(objectField);

        deleteTerrainButton = new Button()
        {
            style=
            {
                justifyContent = Justify.FlexEnd,
                backgroundImage = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Resources/Icons/cancel.png", typeof(Texture2D)),
                backgroundColor = new Color(0,0,0,0),
                borderBottomWidth = 0,
                borderTopWidth = 0,
                borderRightWidth = 0,
                borderLeftWidth = 0,
                marginBottom = 4,
            }
        };

        deleteTerrainButton.clickable.clicked += () =>
        {
            evt.Invoke(this.terrainCodeID);


        };

        rowContainer1.Add(deleteTerrainButton);
        mainContainer.Add(rowContainer1);
        mainContainer.Add(rowContainer2);
        rowContainer2.Add(floraCheckbox.GetVisualElement());
        rowContainer2.Add(floraTileObjectField);
    }


    public VisualElement GetVisualElement()
    {
        return this.mainContainer;
    }
}
