using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public class MainLayout : EditorWindow
{
    VisualElement root;
    ScrollView main;
    public MapPropertiesSectionLayout mapPropertiesSectionLayout;
    public TilePropertiesLayout tilePropertiesLayout;

    public TerraformingLayout terraformingLayout;
    public TileRulesLayout tileRulesLayout;
    //public MapSizeSectionLayout[] mapSizesSections;   //array of visual elements
    public Button generateButton;
    public void Init(VisualElement root)
    {
        this.root = root;

        this.CreateGUI();
        root.style.paddingRight = 5;
        root.style.paddingLeft = 5;
        root.style.overflow = Overflow.Visible;
        

    }

    private void CreateGUI()
    {

        main = new ScrollView();
        root.Add(main);
        // draw all sections
        // 1. Map size section
        mapPropertiesSectionLayout = EditorWindow.CreateInstance("MapPropertiesSectionLayout") as MapPropertiesSectionLayout;
        mapPropertiesSectionLayout.Init(main,0);
        //2. Tile data section
        tilePropertiesLayout = EditorWindow.CreateInstance("TilePropertiesLayout") as TilePropertiesLayout;
        tilePropertiesLayout.Init(main, 0);

        //3. Terraforming Section
        terraformingLayout = EditorWindow.CreateInstance("TerraformingLayout") as TerraformingLayout;
        terraformingLayout.Init(main, 0);

        //4. Tile Rule Section
        // tileRulesLayout = EditorWindow.CreateInstance("TileRulesLayout") as TileRulesLayout;
        // tileRulesLayout.Init(main, 0);
        //4.Generate button
        generateButton = new Button()
        {
            text = "Generate",
        };
        root.Add(generateButton);

        generateButton.clickable.clicked += () =>
        {
            MapGenerator mapGenerator = ScriptableObject.CreateInstance("MapGenerator") as MapGenerator;
            mapGenerator.Init(terraformingLayout);
        };


    }



}
