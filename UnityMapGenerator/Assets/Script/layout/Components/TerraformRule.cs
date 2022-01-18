using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.Collections;

public class TerraformRule : EditorWindow
{

    public VisualElement mainContainer;
    public VisualElement firstContainer;
    public VisualElement secondContainer;

    public int id;
    public DeleteChildEvent evt;
    public Button deleteRuleButton;
    public DropdownField dropdownFieldTileA;
    public DropdownField dropdownFieldTileB;
    public DropdownField dropdownFieldResultingTile;
    public DropdownField dropdownFieldCasesPresets;

    List<string> casesPresets = new List<string>();

    private TerraformingList terraformingList;
    List<string> terraformRuleStrings;
    //public List<TerrainChoice> terrainChoices = new List<TerrainChoice>();


    private Color darkGray = new Color(0.12f, 0.12f, 0.12f);
        private float borderWidth = 2f;

    private Hashtable hashtable;
    public void Init(int id, DeleteChildEvent evt, List<string> terraformRuleStrings)
    {
        this.terraformRuleStrings = terraformRuleStrings;
        //Debug.Log(terraformingList.terrainsStringList[0]);


        this.id = id;
        CreateGUI();
        this.evt = evt;

    }

    private void CreateGUI()
    {

       casesPresets.Add("Any neighbour"); 
       casesPresets.Add("Cross neighbours"); 
       casesPresets.Add("All neighbours"); 
       casesPresets.Add("Corner neighbours"); 

       mainContainer = new VisualElement()
        {
            style=
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
                justifyContent =Justify.SpaceAround
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
        deleteRuleButton = new Button()
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

        deleteRuleButton.clickable.clicked += () =>
        {
            evt.Invoke(this.id);
        };

        dropdownFieldTileA = new DropdownField("FROM Tile A")
        {
            style=
            {
                
            }
        };
        dropdownFieldTileA.labelElement.style.minWidth = 45;
        dropdownFieldTileA.choices = terraformRuleStrings;

        dropdownFieldTileB = new DropdownField("WITH NEIGHBOURS Tile B")
        {
            style=
            {
                
            }
        };
        dropdownFieldTileB.labelElement.style.minWidth = 45;
        dropdownFieldTileB.choices = terraformRuleStrings;



        dropdownFieldResultingTile = new DropdownField("Resulting Tile")
        {
            style=
            {
                
            }
        };
        dropdownFieldResultingTile.labelElement.style.minWidth = 45;
        dropdownFieldResultingTile.choices = terraformRuleStrings;


        dropdownFieldCasesPresets = new DropdownField("Scenario")
        {
            style=
            {
                
            }
        };
        dropdownFieldCasesPresets.labelElement.style.minWidth = 45;
        dropdownFieldCasesPresets.choices = casesPresets;






        secondContainer.Add(dropdownFieldCasesPresets);
        secondContainer.Add(dropdownFieldTileA);
        secondContainer.Add(dropdownFieldTileB);
        secondContainer.Add(dropdownFieldResultingTile);


        mainContainer.Add(firstContainer);
        mainContainer.Add(secondContainer);
        firstContainer.Add(deleteRuleButton);
        mainContainer.Add(secondContainer);

    }
    public VisualElement GetVisualElement()
    {
        return this.mainContainer;
    }


}

