using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;



public class TableButtonElement : EditorWindow
{

    private float borderWidth = 0.1f;
    private float marginValue = 10f;
    private Texture2D noNeighbourTexture;
    private Texture2D hasNeighbourTexture;
    private Texture2D anyNeighbourTexture;

    public Button button;
    public int state; // 0 = must no, 1 = must yes, 2 = ?
    public void Init()
    {
        hasNeighbourTexture = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Resources/Icons/HasNeighbour.png", typeof(Texture2D));
        noNeighbourTexture= (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Resources/Icons/NoNeighbour.png", typeof(Texture2D));
        anyNeighbourTexture= (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Resources/Icons/questionMark.png", typeof(Texture2D));

        state = 0;
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
            if(state+1 > 2)
            {
                state = 0;
            }
            else
            {
                state++;
            }
            Debug.Log(state);
            SwitchState(state);
            
            
        };


    }   

//idk
    public Button GetVisualElement()
    {
        return this.button;
    }

    private void SwitchState(int state)
    {
        switch(state)
        {
            case 0:
            {
                button.style.backgroundImage = noNeighbourTexture;
                break;
            }
            case 1:
            {
                button.style.backgroundImage = hasNeighbourTexture;
                break;
            }
            case 2:
            {
                button.style.backgroundImage = anyNeighbourTexture;
                break;
            }
            default:
            {
                button.style.backgroundImage = null;
                break;
            }
        }
    }

}