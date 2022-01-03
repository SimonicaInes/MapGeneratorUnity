using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class ObjectPicker : EditorWindow
{
    public int id;
    public ObjectField objectField;
    public VisualElement mainContainer;
    public void Init(int id)
    {
        this.id = id;
        CreateGUI();
    }

    private void CreateGUI()
    {
        mainContainer = new VisualElement()
        {
            style = 
            {
                flexDirection = FlexDirection.Column,

            }
        };

        objectField = new ObjectField();
        mainContainer.Add(objectField);




    }

    public VisualElement GetVisualElement()
    {
        return this.mainContainer;
        
    }


}