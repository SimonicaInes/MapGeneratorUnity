using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


public class MapUIGenerator : EditorWindow
{
    public MainLayout mainLayout;
    public int xMapValue, yMapValue;

    [MenuItem("Window/UI Toolkit/MapUIGenerator")]
    public static void ShowExample()
    {
        MapUIGenerator wnd = GetWindow<MapUIGenerator>();
        wnd.titleContent = new GUIContent("MapUIGenerator");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;
        mainLayout = EditorWindow.CreateInstance("MainLayout") as MainLayout;
        mainLayout.Init(root);

    }

    void OnGUI() 
    {

        //xMapValue = mainLayout.mapSizeSectionLayout.xContainer.GetFieldValueInt();
        //yMapValue = mainLayout.mapSizeSectionLayout.yContainer.GetFieldValueInt();
        //Object o = 
        //Debug.Log(mainLayout.tileRulesLayout.tileRuleSets[0].objectPicker.objectField.value);

        // if(mainLayout.mapSizesSections.Length != 0)
        // {
        //     for(int i =0; i< mainLayout.mapSizesSections.Length; i++)
        //     {
        //         mainLayout.mapSizesSections[i].xContainer.GetFieldValue();

        //     }
        // }

        

    }

    private void Update() {
        
    }
}
