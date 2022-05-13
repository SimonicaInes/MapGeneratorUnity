using UnityEditor;
using UnityEngine.UIElements;
using System;

public class LabelTextBox : EditorWindow
{
    public TextField valueField;
    private Label label;
    private VisualElement mainContainer;
    private string labelText;
    public void Init(string labelText)
    {
        this.labelText = labelText;
        label = new Label(labelText)
        {
            style = 
            {
                alignSelf = Align.Center,
                paddingTop = 5,
                paddingBottom =5
            }
        };
        this.CreateGUI();
    }

    private void CreateGUI()
    {
        this.mainContainer = new VisualElement()
        {
            style = 
            {
                flexDirection = FlexDirection.Row,
                flexGrow = 1,
                //backgroundColor = new Color(0, 255, 0)
            }
        };

        valueField = new TextField()
        {
            style = 
            {
                flexGrow = 1,
                flexBasis = 1,
                alignSelf = Align.Center
            }
        };

        mainContainer.Add(label);
        mainContainer.Add(valueField);       
    }
    public VisualElement GetVisualElement()
    {
        return this.mainContainer;
    }

    public int GetFieldValueInt()
    {
        int result;

            string text = this.valueField?.text ?? "0";
            try
            {
                result = int.Parse(text);
                //Debug.Log(result);
                return result;
            }
            catch(FormatException)
            {
                //Debug.Log("NOT A NUMBER!");
                return 0;
            }

    }

}
