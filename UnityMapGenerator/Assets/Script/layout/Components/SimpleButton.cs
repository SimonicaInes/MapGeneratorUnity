using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;



public class SimpleButton : EditorWindow
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
                // borderBottomColor = Color.gray,
                // borderLeftColor = Color.gray,
                // borderRightColor = Color.gray,
                // borderTopColor = Color.gray,
                // borderBottomWidth = borderWidth,
                // borderLeftWidth = borderWidth,
                // borderRightWidth = borderWidth,
                // borderTopWidth = borderWidth,
                backgroundImage = EditorGUIUtility.FindTexture( "Record Off" ),
                height = EditorGUIUtility.FindTexture( "Record Off" ).height,
                width = EditorGUIUtility.FindTexture( "Record Off" ).width,
                marginLeft = -8
                                
            }
        };


    }   


    public Button GetVisualElement()
    {
        return this.button;
    }

    

}