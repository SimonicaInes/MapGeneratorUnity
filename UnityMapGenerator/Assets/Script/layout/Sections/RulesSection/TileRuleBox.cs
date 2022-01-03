using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;



public class TileRuleBox : EditorWindow
{
    public int id;
    public VisualElement root;

    public DeleteChildEvent evt;

    public LabelCheckboxValue generateFloraOnThisTile;

    private float borderWidth = 2f;
    public VisualElement mainContainer;
    public VisualElement topBorderContainer;
    public VisualElement bodyContainer;
    public VisualElement tableContainer;
    public VisualElement spritePickerContainer;
    public VisualElement optionsContainer;
    public LabelCheckbox generateFloraCheckbox;

    public RuleTable ruleTable;

    public Button deleteTileRule;

    private Color darkGray = new Color(0.12f, 0.12f, 0.12f);

    public TileRuleBox(VisualElement root, int id, DeleteChildEvent evt)
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

        topBorderContainer = new VisualElement()
        {
            style = 
            {
                flexDirection = FlexDirection.Row,
                backgroundColor = darkGray,
                justifyContent = Justify.SpaceBetween
                
            }
        };       

        bodyContainer = new VisualElement()
        {
            style = 
            {
                flexDirection = FlexDirection.Row,
                justifyContent = Justify.SpaceBetween
            }
        };

        tableContainer = new VisualElement()
        {
            style = 
            {
                flexDirection = FlexDirection.Column,
            }
        };

        spritePickerContainer = new VisualElement()
        {
            style = 
            {
                flexDirection = FlexDirection.Column,
                //borderLeftColor = Color.gray,
                //borderLeftWidth = 0.1f, 
                //t               
            }
        };

        optionsContainer = new VisualElement()
        {
            style = 
            {
                flexDirection = FlexDirection.Column,
                //borderLeftColor = Color.gray,
                //borderLeftWidth = 0.1f,   
            }
        };



        // BUTTON CREATION
        deleteTileRule = new Button()
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

        deleteTileRule.clickable.clicked += () =>
        {
            evt.Invoke(this.id);


        };
        //END BUTTON CREATION
        


        this.root.Add(mainContainer);
        mainContainer.Add(topBorderContainer);
        topBorderContainer.Add(deleteTileRule);
        mainContainer.Add(bodyContainer);
        //add the 3 columns
        bodyContainer.Add(tableContainer);
        bodyContainer.Add(spritePickerContainer);
        bodyContainer.Add(optionsContainer);
       //each column test
        //tableContainer.Add(new Label ("DONE"));
        spritePickerContainer.Add(new Label ("DONE"));
        //optionsContainer.Add(new Label ("DONE"));

        //Populate the options container
        generateFloraCheckbox = new LabelCheckbox("Generate Flora ");
        optionsContainer.Add(generateFloraCheckbox.GetVisualElement());

        //populate rule table container
        //ruleTable = new RuleTable();
        //tableContainer.Add(ruleTable.GetVisualElement());


    }
    public VisualElement GetVisualElement()
    {
        return this.mainContainer;
        
    }

}
