using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
public class TileRulesLayout : EditorWindow
{
    public DeleteChildEvent testEventListener;
    public int id;

    VisualElement root;
    public VisualElement mainContainer;
    public VisualElement tileFolderContainer;
    public VisualElement ruleContainer;
    public LabelTextBox resourceFolderName1;
    public LabelTextBox resourceFolderName2;
    public Button addRuleSetButton;
    private int i = 0;
    public void Init(VisualElement root, int id)
    {
        this.id = id;
        this.root = root;
        this.testEventListener = new DeleteChildEvent();
        testEventListener.AddListener(buttonListener);
        this.CreateGUI();
    }

    List<TileRuleBox> tileRuleSets = new List<TileRuleBox>();

    private void CreateGUI()
    {

        mainContainer = new VisualElement()
        {
            style = 
            {
                flexDirection = FlexDirection.Column,
                borderTopColor = Color.gray,
                borderTopWidth = 0.1f,
                marginBottom = 20,
                paddingBottom = 5,

    
            }
        }; 

        ruleContainer = new VisualElement()
        {
            style = 
            {
                flexDirection = FlexDirection.Column,
                paddingTop = 5,
                paddingBottom = 5,
            }
        }; 


        tileFolderContainer = new VisualElement()
        {
            style = 
            {
                flexDirection = FlexDirection.Row,
                paddingTop = 5,
                paddingBottom = 5  
            }
        }; 



        Label title = new Label()
        {
            text = "Tile Rules"
        };

        addRuleSetButton = new Button()
        {
            text = "Add Rule Set",
            style = 
            {
              
            }

        };

        
        resourceFolderName1 = EditorWindow.CreateInstance("LabelTextBox") as LabelTextBox;
        resourceFolderName1.Init("Tile Resource Folder 1");
        resourceFolderName2 = EditorWindow.CreateInstance("LabelTextBox") as LabelTextBox;
        resourceFolderName2.Init("Tile Resource Folder 2");

        root.Add(mainContainer);
        mainContainer.Add(title);

        mainContainer.Add(tileFolderContainer);

        tileFolderContainer.Add(resourceFolderName1.GetVisualElement());
        VisualElement tranzitionSpace = new VisualElement();
        tranzitionSpace.Add(new Label("    ---->>    "));
        tileFolderContainer.Add(tranzitionSpace);
        tileFolderContainer.Add(resourceFolderName2.GetVisualElement());

        mainContainer.Add(addRuleSetButton);
        mainContainer.Add(ruleContainer);


//HERE!!!!
        addRuleSetButton.clickable.clicked += () =>
        {
            TileRuleBox t = EditorWindow.CreateInstance("TileRuleBox") as TileRuleBox;
            t.Init(root, i, testEventListener);
            tileRuleSets.Add(t);
            ruleContainer.Add(tileRuleSets[tileRuleSets.Count-1].GetVisualElement());
            i++;           
        };
    }


    private void buttonListener(int id)
    {
        DestroyImmediate(tileRuleSets.Find(item => item.id == id), true);
        tileRuleSets.Remove(tileRuleSets.Find(item => item.id == id));

        redrawtileRule();
    }

    private void redrawtileRule()
    {
        while (ruleContainer.childCount > 0)
        {
            ruleContainer.RemoveAt(ruleContainer.childCount - 1);
            
        }

        int index = 0;
        foreach(TileRuleBox t in tileRuleSets)
        {
            ruleContainer.Add(tileRuleSets[index++].GetVisualElement());//
        }
    }



}