using UnityEditor;
using UnityEngine.UIElements;
using UnityEngine;
public class MainLayout : EditorWindow
{
    VisualElement root;
    public MapSizeSectionLayout mapSizeSectionLayout;
    public TilePropertiesLayout tilePropertiesLayout;
    //public MapSizeSectionLayout[] mapSizesSections;   //array of visual elements
    public MainLayout(VisualElement root)
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
        mapSizeSectionLayout = new MapSizeSectionLayout(root,0);
        tilePropertiesLayout = new TilePropertiesLayout(root, 0);
        VisualElement e = new VisualElement();
        e.Add(new DropdownMenu());

    }


}
