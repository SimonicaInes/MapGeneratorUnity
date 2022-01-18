using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;


public class TerraformingRules : Editor 
{
    
    public VisualElement mainContainer;
    public VisualElement rulesContainer;

    public Button addRuleButton;
    private TerraformingList terraformingList;
    private Button refreshButton;
    public DeleteChildEvent eventListener;

    public List<TerraformRule> terrainRules = new List<TerraformRule>();
    private List<string> terraformRuleStrings = new List<string>();
    public void Init(TerraformingList terraformingList)
    {
        this.terraformingList = terraformingList;
        this.eventListener = new DeleteChildEvent();
        eventListener.AddListener(buttonListener);

        CreateGUI();
    }

    public void CreateGUI()
    {
        mainContainer = new VisualElement()
        {
            style=
            {
                flexDirection = FlexDirection.Column,

            }
        };
        rulesContainer = new VisualElement()
        {
            style=
            {
                flexDirection = FlexDirection.Column,

            }
        };

        refreshButton = new Button();
        refreshButton.text = "REFRESH DROPDOWN LISTS";
        refreshButton.clickable.clicked += () =>
        {
            RefreshStringList();
        };

        addRuleButton = new Button();
        addRuleButton.text = "Add rule";

        addRuleButton.clickable.clicked += () =>
        {
            foreach(TerrainChoice tc in terraformingList.terrains)
            {
                //Debug.Log(tc.terrainName);
                terraformRuleStrings.Add(tc.terrainName);
            }
            TerraformRule tr = EditorWindow.CreateInstance("TerraformRule") as TerraformRule;
            tr.Init(terrainRules.Count, eventListener, terraformRuleStrings);
            terrainRules.Add(tr);
            rulesContainer.Add(tr.GetVisualElement());
            RefreshStringList();

        };


        mainContainer.Add(refreshButton);
        mainContainer.Add(addRuleButton);
        mainContainer.Add(rulesContainer);
    }

    public VisualElement GetVisualElement()
    {
        return this.mainContainer;
    }

    private void buttonListener(int id)
    {
        //terraformRuleStrings.Remove(terraformRuleStrings.Find(x=> x.Contains(terraformingList.terrains[id].terrainName)));
        DestroyImmediate(terrainRules.Find(item => item.id == id), true);
        terrainRules.Remove(terrainRules.Find(item => item.id == id));
        RefreshStringList();
        redrawList();
    }

    private void redrawList()
    {
        while (rulesContainer.childCount > 0)
        {
            rulesContainer.RemoveAt(rulesContainer.childCount - 1);
            
        }

        int index = 0;
        foreach(TerraformRule t in terrainRules)
        {
            rulesContainer.Add(terrainRules[index++].GetVisualElement());
            t.id = index-1;
           // Debug.Log(t.terrainCodeID);
        }
    }

    public void RefreshStringList()
    {
        terraformRuleStrings.Clear();
        foreach(TerrainChoice tc in terraformingList.terrains)
        {
            terraformRuleStrings.Add(tc.terrainName);
        }
        Debug.Log("DONE");
        
        
    }

}