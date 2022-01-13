using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;



public class TableButtonElement : EditorWindow
{

    private float borderWidth = 0.1f;
    public Button button;
    public void Init()
    {
        this.CreateGUI();
    }
    private void CreateGUI()
    {

        button = new Button()
        {
            style=
            {
                borderBottomColor = Color.gray,
                borderLeftColor = Color.gray,
                borderRightColor = Color.gray,
                borderTopColor = Color.gray,
                borderBottomWidth = borderWidth,
                borderLeftWidth = borderWidth,
                borderRightWidth = borderWidth,
                borderTopWidth = borderWidth,                
            }
        };

    }   

//idk
    public Button GetVisualElement()
    {
        return this.button;
    }

}