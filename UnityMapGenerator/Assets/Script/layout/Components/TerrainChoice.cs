using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEngine.Tilemaps;


public class TerrainChoice : EditorWindow
{

    public VisualElement mainContainer;
    public int terrainCodeID;
    public ObjectField objectField;

    public Tile terrainTile;
    public string terrainName;
    public DeleteChildEvent evt;
    public Button deleteTerrainButton;

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

        LabelTextBox labelTextBox = EditorWindow.CreateInstance("LabelTextBox") as LabelTextBox;
        labelTextBox.Init("Name");

        //terrain name updates upon change.
        labelTextBox.valueField.RegisterCallback<ChangeEvent<string>>(e =>
        {
            //Debug.Log(labelTextBox.valueField.value.ToString());
            terrainName = labelTextBox.valueField.value.ToString();
        });

        mainContainer.Add(labelTextBox.GetVisualElement());
        mainContainer.Add(objectField);

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

        mainContainer.Add(deleteTerrainButton);

    }


    public VisualElement GetVisualElement()
    {
        return this.mainContainer;
    }
}
