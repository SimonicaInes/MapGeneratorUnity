using UnityEditor;
using UnityEngine.UIElements;
using System;


public class LabelCheckboxValue : EditorWindow
{
    private VisualElement mainContainer;
    private VisualElement firstContainer;
    private VisualElement secondContainer;
    private Label label;

    public LabelTextBox defaultValue;
    public UnityEngine.UIElements.Toggle checkbox;

    public float value;


    public void Init(string labelText)
    {
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

        defaultValue = EditorWindow.CreateInstance("LabelTextBox") as LabelTextBox;
        defaultValue.Init("Value");

        firstContainer.Add(checkbox);
        firstContainer.Add(label);
        secondContainer.Add(defaultValue.GetVisualElement());
        mainContainer.Add(firstContainer);
        mainContainer.Add(secondContainer);

         defaultValue.valueField.RegisterCallback<ChangeEvent<string>>(e =>
        {
            //Debug.Log(labelTextBox.valueField.value.ToString());
            if(defaultValue.valueField.value != null)
            {
                try
                {
                    value = float.Parse(defaultValue.valueField.value);
                }
                catch(FormatException)
                {
                    value = 0;
                    return;
                }
                
            }
            else
            {
                value = 1;
            }

         
        });
    }
    public VisualElement GetVisualElement()
    {
        return this.mainContainer;
    }


}
