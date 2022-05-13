using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;



public class TerraformingLayout : EditorWindow
{
    public VisualElement mainContainer;
    public VisualElement root;

    public int id;
    
    public TerraformingList terraformingList;
    public TerraformingRules terraformingRules;


    public void Init(VisualElement root, int id)
    {
        this.id = id;
        this.root= root;
        CreateGUI();
    }

    private void CreateGUI()
    {
        mainContainer = new VisualElement()
        {
            style=
            {
                flexDirection = FlexDirection.Column,
                borderTopColor = Color.gray,
                borderTopWidth = 0.1f,
                marginBottom = 20,
                paddingBottom = 5,
                paddingTop = 0
            }
        };
        root.Add(mainContainer);
        mainContainer.Add(new Label("Terraforming")
        {
            style=
            {
                paddingBottom = 5,
                paddingTop = 2,
                unityFontStyleAndWeight = FontStyle.Bold            
            }
        });

        terraformingList = EditorWindow.CreateInstance("TerraformingList") as TerraformingList;
        terraformingList.Init();

        
        terraformingRules = EditorWindow.CreateInstance("TerraformingRules") as TerraformingRules;
        terraformingRules.Init(terraformingList);

        mainContainer.Add(terraformingList.GetVisualElement());
        mainContainer.Add(terraformingRules.GetVisualElement());




    }



}