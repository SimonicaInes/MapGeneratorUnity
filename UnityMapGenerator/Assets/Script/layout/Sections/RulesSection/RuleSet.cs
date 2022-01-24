using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Collections;

public class RuleSet : EditorWindow
{
    public DeleteChildEvent ruleEvt;

    public DeleteChildEvent testEventListener;
    public int id;
    private int i= 0;
    public VisualElement columnContainer;    
    public VisualElement row1Container;
    public VisualElement headerContainer;
    public VisualElement row2Container;
    public VisualElement row3Container;
    private Button addRuleButton;
    private Button deleteRuleSetButton;

    public DropdownField originalTerrainType, neighbouringTerrainType;


    //private float borderWidth = 0.1f;
    //private float marginValue = 10f;

    public TableButtonElement[] t = new TableButtonElement[9];
    private TerraformingList terraformingList;
    private List<string> terrainStringList = new List<string>();
    public List<TileRuleBox> tileRules = new List<TileRuleBox>();

    public void Init(List<string> terrainStringList, int id, DeleteChildEvent ruleEvt)
    {
        this.id = id;
        this.ruleEvt = ruleEvt;
        this.testEventListener = new DeleteChildEvent();
        testEventListener.AddListener(buttonListener);        
        this.terrainStringList = terrainStringList;
        
        this.CreateGUI();
    }

    private void CreateGUI()
    {
        columnContainer = new VisualElement()
        {
            style = 
            {
                flexDirection = FlexDirection.Column,
                //flexGrow = 1,
                // alignContent = Align.Stretch,
                // alignItems = Align.Center,
                // justifyContent = Justify.SpaceAround,
                // alignSelf = Align.Stretch
                backgroundColor = new Color(0.14f, 0.14f, 0.14f, 0.4f),
                marginBottom = 10,
                marginTop = 10
            }
        };
        headerContainer = new VisualElement()
        {
            style = 
            {
                flexDirection = FlexDirection.Row,
                alignItems = Align.Center,
                alignSelf = Align.Stretch,
                justifyContent = Justify.FlexStart

            }
        };
        row1Container = new VisualElement()
        {
            style = 
            {
                flexDirection = FlexDirection.Row,
                alignItems = Align.Center,
                alignSelf = Align.Stretch,
                justifyContent = Justify.SpaceAround

            }
        };

       // BUTTON CREATION
        deleteRuleSetButton = new Button()
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

        deleteRuleSetButton.clickable.clicked += () =>
        {
            ruleEvt.Invoke(this.id);


        };
        headerContainer.Add(deleteRuleSetButton);
        //END BUTTON CREATION

        row2Container = new VisualElement()
        {
            style = 
            {
                flexDirection = FlexDirection.Column,
            }
        };
        row3Container = new VisualElement()
        {
            style = 
            {
                flexDirection = FlexDirection.Column,
            }
        };
        Label l = new Label()
        {
            text = "RULE SET",
            style=
            {
                paddingBottom = 5,
                paddingTop = 2,
                unityFontStyleAndWeight = FontStyle.Bold
            }
        };
        headerContainer.Add(l);
        columnContainer.Add(headerContainer);
        columnContainer.Add(row1Container);
        columnContainer.Add(row2Container);
        columnContainer.Add(row3Container);

        originalTerrainType = new DropdownField("Original terrain")
        {
            style=
            {
                maxWidth =170
            }
        };
        originalTerrainType.labelElement.style.minWidth = 45;
        originalTerrainType.choices = terrainStringList;
        row1Container.Add(originalTerrainType);


        VisualElement tranzitionSpace = new VisualElement();
        tranzitionSpace.Add(new Label("    ---->>    "));
        row1Container.Add(tranzitionSpace);
        
        
        neighbouringTerrainType = new DropdownField("Neighbouring terrain")
        {
            style=
            {
                maxWidth =170
            }
        };
        neighbouringTerrainType.labelElement.style.minWidth = 45;
        neighbouringTerrainType.choices = terrainStringList;
        row1Container.Add(neighbouringTerrainType);



        addRuleButton = new Button()
        {
            text = "Add new rule",
            style=
            {
            }
        };
        row2Container.Add(addRuleButton);
        addRuleButton.clickable.clicked += () =>
        {
            TileRuleBox t = EditorWindow.CreateInstance("TileRuleBox") as TileRuleBox;
            t.Init(row3Container, i, testEventListener);
            tileRules.Add(t);
            row3Container.Add(tileRules[tileRules.Count-1].GetVisualElement());
            i++;            
        };

    }
    
    public VisualElement GetVisualElement()
    {
        return this.columnContainer;
    }

    private void buttonListener(int id)
        {
            DestroyImmediate(tileRules.Find(item => item.id == id), true);
            tileRules.Remove(tileRules.Find(item => item.id == id));

            redrawtileRule();
        }

    private void redrawtileRule()
    {
        while (row3Container.childCount > 0)
        {
            row3Container.RemoveAt(row3Container.childCount - 1);
            
        }

        int index = 0;
        foreach(TileRuleBox t in tileRules)
        {
            row3Container.Add(tileRules[index++].GetVisualElement());//
        }
    }


}
//solved error by using instances