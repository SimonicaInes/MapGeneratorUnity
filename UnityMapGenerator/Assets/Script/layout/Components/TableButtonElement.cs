using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;



public class TableButtonElement : EditorWindow
{

    private float borderWidth = 0.1f;
    private float marginValue = 10f;
    private Texture2D noNeighbourTexture;
    private Texture2D hasNeighbourTexture;

    public Button button;
    public bool state; // in which TRUE = has neighbour and FALSE = doesn't have neighbour
    public void Init()
    {
        hasNeighbourTexture = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Resources/Icons/HasNeighbour.png", typeof(Texture2D));
        noNeighbourTexture= (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Resources/Icons/NoNeighbour.png", typeof(Texture2D));

        state = false;
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
                scale= new Scale(new Vector3(1.6f,1.4f,1f)),
                marginLeft = marginValue,               
                marginTop = marginValue,               
                marginBottom = marginValue,               
                marginRight = marginValue,
                backgroundImage = noNeighbourTexture
   

                
            }
        };

        button.clickable.clicked+= () =>
        {
            state = !state;
            if(state)
            {
                button.style.backgroundImage = hasNeighbourTexture;
               // Debug.Log(currentTexture);
            }
            else
            {
                button.style.backgroundImage = noNeighbourTexture;
                //Debug.Log(currentTexture);
            }
            
            
        };


    }   

//idk
    public Button GetVisualElement()
    {
        return this.button;
    }

}