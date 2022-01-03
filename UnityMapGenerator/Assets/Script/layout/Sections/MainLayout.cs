using UnityEditor;
using UnityEngine.UIElements;
public class MainLayout : EditorWindow
{
    VisualElement root;
    public MapSizeSectionLayout mapSizeSectionLayout;
    public TilePropertiesLayout tilePropertiesLayout;
    public TileRulesLayout tileRulesLayout;
    //public MapSizeSectionLayout[] mapSizesSections;   //array of visual elements
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
        // draw all sections
        // 1. Map size section
        mapSizeSectionLayout = EditorWindow.CreateInstance("MapSizeSectionLayout") as MapSizeSectionLayout;
        mapSizeSectionLayout.Init(root,0);
        //2. Tile data section
        tilePropertiesLayout = EditorWindow.CreateInstance("TilePropertiesLayout") as TilePropertiesLayout;
        tilePropertiesLayout.Init(root, 0);
        //3. Tile Rule Section
        tileRulesLayout = EditorWindow.CreateInstance("TileRulesLayout") as TileRulesLayout;
        tileRulesLayout.Init(root, 0);
    }


}
