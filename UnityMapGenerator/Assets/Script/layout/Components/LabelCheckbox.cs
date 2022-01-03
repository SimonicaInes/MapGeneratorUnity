using UnityEditor;
using UnityEngine.UIElements;


public class LabelCheckbox : EditorWindow
{
    private VisualElement mainContainer;
    private Label label;
    private string labelText;
    public UnityEngine.UIElements.Toggle checkbox;
    public LabelCheckbox(string labelText)
    {
        this.labelText = labelText;
        label = new Label(labelText);
        this.CreateGUI();
    }

    private void CreateGUI()
    {
        mainContainer = new VisualElement()
        {
            style=
            {
                flexDirection = FlexDirection.Row,
            }
        };
        checkbox = new UnityEngine.UIElements.Toggle()
        {
            style=
            {
            }
        };

        mainContainer.Add(checkbox);
        mainContainer.Add(label);

        
    }
    public VisualElement GetVisualElement()
    {
        return this.mainContainer;
    }


}
