using UnityEditor;
using UnityEngine.UIElements;

public class LabelCheckboxValue : EditorWindow
{
    private VisualElement mainContainer;
    private VisualElement firstContainer;
    private VisualElement secondContainer;
    private Label label;
    private string labelText;

    public LabelTextBox defaultValue;
    public UnityEngine.UIElements.Toggle checkbox;
    public LabelCheckboxValue(string labelText)
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
        firstContainer = new VisualElement()
        {
            style=
            {
                flexDirection = FlexDirection.Row,
                flexGrow = 1
            }
        };
        secondContainer = new VisualElement()
        {
            style=
            {
                flexDirection = FlexDirection.Row,
                flexGrow = 1,
                //backgroundColor = Color.yellow

            }
        };                
        checkbox = new UnityEngine.UIElements.Toggle()
        {
            style=
            {
            }
        };

        defaultValue = new LabelTextBox("Default Value");

        firstContainer.Add(checkbox);
        firstContainer.Add(label);
        secondContainer.Add(defaultValue.GetVisualElement());
        mainContainer.Add(firstContainer);
        mainContainer.Add(secondContainer);

        
    }
    public VisualElement GetVisualElement()
    {
        return this.mainContainer;
    }


}
