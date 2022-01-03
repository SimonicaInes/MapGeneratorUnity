using UnityEditor;
using UnityEngine.UIElements;


public class RuleTable : EditorWindow
{

    public VisualElement columnContainer;
    public VisualElement row1Container;
    public VisualElement row2Container;
    public VisualElement row3Container;



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
            }
        };
        row1Container = new VisualElement()
        {
            style = 
            {
                flexDirection = FlexDirection.Row,
            }
        };
        row2Container = new VisualElement()
        {
            style = 
            {
                flexDirection = FlexDirection.Row,
            }
        };
        row3Container = new VisualElement()
        {
            style = 
            {
                flexDirection = FlexDirection.Row,
            }
        };

        columnContainer.Add(row1Container);
        columnContainer.Add(row2Container);
        columnContainer.Add(row3Container);

        for(int i= 0; i < 9; i++)
        {
            t[i] = EditorWindow.CreateInstance("TableButtonElement") as TableButtonElement;
            t[i].Init();
            
        }

        row1Container.Add(t[0].GetVisualElement());
        row1Container.Add(t[1].GetVisualElement());
        row1Container.Add(t[2].GetVisualElement());

        row2Container.Add(t[3].GetVisualElement());
        row2Container.Add(t[4].GetVisualElement());
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