using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public class TilePropertyBox : EditorWindow
{
    public int id;
    public VisualElement root;

    public DeleteChildEvent evt;
    public LabelTextBox resourceFolderName;
    public VisualElement columnContainerChoices;

    public LabelCheckboxValue damageField;
    public LabelCheckboxValue speedField;
    public LabelCheckboxValue regenerationField;
    public LabelCheckbox diggingField;

    private float borderWidth = 2f;
    public VisualElement mainContainer;
    public VisualElement firstContainer;
    public VisualElement secondContainer;

    public Button deleteTileProperty;

    private Color darkGray = new Color(0.12f, 0.12f, 0.12f);

    public TilePropertyBox(VisualElement root, int id, DeleteChildEvent evt)
    {
        this.id = id;
        this.root = root;
        this.CreateGUI();
        this.evt = evt;
    }

    private void CreateGUI()
    {
        mainContainer = new VisualElement()
        {
            style = 
            {
                flexDirection = FlexDirection.Column,
                borderBottomColor = darkGray,
                borderLeftColor = darkGray,
                borderRightColor = darkGray,
                borderTopColor = darkGray,
                borderBottomWidth = borderWidth,
                borderLeftWidth = borderWidth,
                borderRightWidth = borderWidth,
                borderTopWidth = borderWidth,
                marginBottom = 20,
                paddingBottom = 5,
     
            }
        };

        secondContainer = new VisualElement()
        {
            style = 
            {
                flexDirection = FlexDirection.Row,
            }
        };
        firstContainer = new VisualElement()
        {
            style = 
            {
                flexDirection = FlexDirection.Row,
                backgroundColor = darkGray,
                justifyContent = Justify.SpaceBetween
                
            }
        };        
        columnContainerChoices = new VisualElement()
        {
            style = 
            {
                flexDirection = FlexDirection.Column,
                flexGrow = 1,
                //backgroundColor = Color.blue
            }
        };

        // BUTTON CREATION
        deleteTileProperty = new Button()
        {
            style = 
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

        deleteTileProperty.clickable.clicked += () =>
        {
            evt.Invoke(this.id);


        };


        //END BUTTON CREATION

        resourceFolderName = new LabelTextBox("Tiles resource folder");

        damageField = new LabelCheckboxValue("Damage");
        speedField = new LabelCheckboxValue("Speed");
        regenerationField = new LabelCheckboxValue("Regeneration");
        diggingField = new LabelCheckbox("Dig");

        columnContainerChoices.Add(damageField.GetVisualElement());
        columnContainerChoices.Add(speedField.GetVisualElement());
        columnContainerChoices.Add(regenerationField.GetVisualElement());
        columnContainerChoices.Add(diggingField.GetVisualElement());

        secondContainer.Add(resourceFolderName.GetVisualElement());
        secondContainer.Add(columnContainerChoices);

        // firstContainer.Add(new Label("Tile Property #" + id.ToString())
        // {
        //     style = 
        //     {
        //         fontSize = 16f
        //     }
        // });

        firstContainer.Add(deleteTileProperty);
        mainContainer.Add(firstContainer);
        mainContainer.Add(secondContainer);
        this.root.Add(mainContainer);
    }
    public VisualElement GetVisualElement()
    {
        return this.mainContainer;
    }

}
