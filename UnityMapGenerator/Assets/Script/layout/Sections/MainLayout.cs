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

    }

    private void CreateGUI()
    {
        // draw all sections
        // 1. Map size section
        mapSizeSectionLayout = EditorWindow.CreateInstance("MapSizeSectionLayout") as MapSizeSectionLayout;
        mapSizeSectionLayout.Init(root,0);
        //tilePropertiesLayout = new TilePropertiesLayout(root, 0);
        // VisualElement e = new VisualElement();
        //e.Add(new DropdownMenu());
        tileRulesLayout = EditorWindow.CreateInstance("TileRulesLayout") as TileRulesLayout;
        tileRulesLayout.Init(root, 0);
    }


}
