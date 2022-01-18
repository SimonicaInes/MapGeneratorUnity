using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using UnityEditor.UIElements;
public class TileRulesLayout : EditorWindow
{
    public DeleteChildEvent testEventListener;
    public int id;

    VisualElement root;
    public VisualElement mainContainer;
    public VisualElement tileFolderContainer;
    public VisualElement ruleContainer;
    public ObjectField resourceFolderName1;
    public ObjectField resourceFolderName2;
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

    public List<TileRuleBox> tileRuleSets = new List<TileRuleBox>();

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
                paddingBottom = 5,
                justifyContent = Justify.SpaceAround,


            }
        }; 



        Label title = new Label()
        {
            text = "Tile Rules",
            style=
            {
                paddingBottom = 5,
                paddingTop = 2,
                unityFontStyleAndWeight = FontStyle.Bold
            }
        };

        addRuleSetButton = new Button()
        {
            text = "Add Rule Set",
            style = 
            {
              
            }

        };

        //RESOURCE FOLDERS
        resourceFolderName1 = new ObjectField("RF1");
        resourceFolderName1.objectType = typeof(DefaultAsset);
        resourceFolderName1.labelElement.style.minWidth = 20;
        resourceFolderName2 = new ObjectField("RF2");
        resourceFolderName2.objectType = typeof(DefaultAsset);
        resourceFolderName2.labelElement.style.minWidth = 20;


        //END RESOURCE FOLDERS 

        root.Add(mainContainer);
        mainContainer.Add(title);

        mainContainer.Add(tileFolderContainer);

        tileFolderContainer.Add(resourceFolderName1);

        VisualElement tranzitionSpace = new VisualElement();
        tranzitionSpace.Add(new Label("    ---->>    "));
        tileFolderContainer.Add(tranzitionSpace);
        tileFolderContainer.Add(resourceFolderName2);

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