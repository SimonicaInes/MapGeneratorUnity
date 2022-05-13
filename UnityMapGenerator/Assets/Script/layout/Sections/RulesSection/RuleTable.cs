using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


public class RuleTable : EditorWindow
{

    public VisualElement columnContainer;
    public VisualElement row1Container;
    public VisualElement row2Container;
    public VisualElement row3Container;
    private Button button;

    private float borderWidth = 0.1f;
    private float marginValue = 10f;

    public TableButtonElement[] t = new TableButtonElement[9];



    public void Init()
    {
        this.CreateGUI();
    }

    private void CreateGUI()
    {
        columnContainer = new VisualElement()
        {
            style = 
            {
                flexDirection = FlexDirection.Column,
                flexGrow = 1,
                alignContent = Align.Stretch,
                alignItems = Align.Center,
                justifyContent = Justify.SpaceAround
            }
        };
        row1Container = new VisualElement()
        {
            style = 
            {
                flexDirection = FlexDirection.Row,
                alignItems = Align.Center,


            }
        };
        row2Container = new VisualElement()
        {
            style = 
            {
                flexDirection = FlexDirection.Row,
                alignItems = Align.Center
            }
        };
        row3Container = new VisualElement()
        {
            style = 
            {
                flexDirection = FlexDirection.Row,
                alignItems = Align.Center
            }
        };

        columnContainer.Add(row1Container);
        columnContainer.Add(row2Container);
        columnContainer.Add(row3Container);

        for(int i= 0; i < 9; i++)
        {
            if(i!=4)
            {
            t[i] = EditorWindow.CreateInstance("TableButtonElement") as TableButtonElement;
            t[i].Init();
            }
            else
            {
                t[4] = null;
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
                        backgroundImage = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Resources/Icons/middleIcon.png", typeof(Texture2D))
                        
                    }
                };
                //button.
            }

            
        }

        row1Container.Add(t[0].GetVisualElement());
        row1Container.Add(t[1].GetVisualElement());
        row1Container.Add(t[2].GetVisualElement());

        row2Container.Add(t[3].GetVisualElement());
        row2Container.Add(button);
        row2Container.Add(t[5].GetVisualElement());

        row3Container.Add(t[6].GetVisualElement());
        row3Container.Add(t[7].GetVisualElement());
        row3Container.Add(t[8].GetVisualElement());




     }

    
    public VisualElement GetVisualElement()
    {
        return this.columnContainer;
    }
}
//solved error by using instances