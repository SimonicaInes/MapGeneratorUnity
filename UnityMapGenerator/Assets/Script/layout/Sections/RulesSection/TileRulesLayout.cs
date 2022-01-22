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
    public Button addRuleSetButton;
    private int i = 0;
    private TerraformingList terraformingList;
    private List<string> terraformRuleStrings = new List<string>();

    public void Init(VisualElement root, int id, TerraformingList terraformingList)
    {
        this.id = id;
        this.root = root;
        this.terraformingList = terraformingList;
        this.testEventListener = new DeleteChildEvent();
        testEventListener.AddListener(buttonListener);
        this.CreateGUI();
    }

    public List<RuleSet> tileRuleSets = new List<RuleSet>();

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

        Button refreshButton = new Button();
        refreshButton.text = "REFRESH DROPDOWN LISTS";
        refreshButton.clickable.clicked += () =>
        {
            RefreshStringList();
        };

        root.Add(mainContainer);
        mainContainer.Add(title);

        mainContainer.Add(tileFolderContainer);

        mainContainer.Add(refreshButton);
        mainContainer.Add(addRuleSetButton);
        mainContainer.Add(ruleContainer);



        addRuleSetButton.clickable.clicked += () =>
        {
            RuleSet t = EditorWindow.CreateInstance("RuleSet") as RuleSet;
            t.Init(terraformRuleStrings, i, testEventListener);
            tileRuleSets.Add(t);
            ruleContainer.Add(tileRuleSets[tileRuleSets.Count-1].GetVisualElement());
            i++;    
            RefreshStringList();        
            
        };




    }


    private void buttonListener(int id)
    {
        DestroyImmediate(tileRuleSets.Find(item => item.id == id), true);
        tileRuleSets.Remove(tileRuleSets.Find(item => item.id == id));
        RefreshStringList();
        redrawtileRule();
    }

    private void redrawtileRule()
    {
        while (ruleContainer.childCount > 0)
        {
            ruleContainer.RemoveAt(ruleContainer.childCount - 1);
            
        }

        int index = 0;
        foreach(RuleSet t in tileRuleSets)
        {
            ruleContainer.Add(tileRuleSets[index++].GetVisualElement());//
        }
    }

        public void RefreshStringList()
        {
        terraformRuleStrings.Clear();
        foreach(TerrainChoice tc in terraformingList.terrains)
        {
            terraformRuleStrings.Add(tc.terrainName);
        }
        
        
        
    }

}