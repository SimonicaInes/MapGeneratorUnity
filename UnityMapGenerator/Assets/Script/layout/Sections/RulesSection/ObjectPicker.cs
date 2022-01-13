using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEngine.Tilemaps;



public class ObjectPicker : Editor
{
    public int id;
    public VisualElement mainContainer;
    public VisualElement displaySprite;
    public VisualElement pickerContainer;
    public ObjectField objectField;
    public Tile obj;
    private Sprite defaultTileSprite;




    public void Init(int id)
    {
        this.id = id;

        Texture2D defaultTexture = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Resources/Tiles/Unexplored.png", typeof(Texture2D));
        defaultTileSprite = Sprite.Create(defaultTexture, new Rect(0, 0, defaultTexture.width, defaultTexture.height), new Vector2(0.5f, 0.5f));
        //ObjectPicker wnd = GetWindow<ObjectPicker>();
        //wnd.Close();
        CreateGUI();
    }


    private void CreateGUI()
    {

        mainContainer = new VisualElement()
        {
            style = 
            {
                flexDirection = FlexDirection.Column,
                flexGrow = 1,
                //backgroundColor = Color.cyan,
                flexWrap = Wrap.Wrap

            }
        };

        objectField =  new ObjectField()
        {
            style=
            {
                flexGrow = 2,
                alignContent = Align.Center,
                maxWidth = 130,
            }
        };
        objectField.objectType = typeof(Tile);

        objectField.RegisterCallback<ChangeEvent<UnityEngine.Object>>(e =>
        {
            if (objectField.value != null)
            {
                // 4
                reassignValue();
            }

                    
        });


        pickerContainer = new VisualElement()
        {
            style = 
            {
                flexDirection = FlexDirection.Row,
                justifyContent = Justify.Center,
                alignItems = Align.Center,
                width = mainContainer.style.width,
                

                

            }
        };
        displaySprite = new VisualElement()
        {
            style = 
            {
                alignSelf = Align.Center,
                width = defaultTileSprite.texture.width*2,
                height = defaultTileSprite.texture.height*2,
                backgroundImage = defaultTileSprite.texture,

            }
        };

    
        mainContainer.Add(displaySprite);
        mainContainer.Add(pickerContainer);

        //pickerContainer.Add(textField);
        //pickerContainer.Add(pickerButton.GetVisualElement());
        pickerContainer.Add(objectField);
  

        
        
    }


    public VisualElement GetVisualElement()
    {
        return this.mainContainer;
        
    }

    private void reassignValue()
    {
        obj = (Tile)objectField.value;
        Debug.Log(obj);
        //float spriteScale = obj.sprite.spriteAtlasTextureScale;


        Rect r = obj.sprite.textureRect;

        Texture2D t = Texture2D.Instantiate(obj.sprite.texture);
        Color[] croppedPixels = t.GetPixels(((int)r.x), (int)r.y, (int)r.width, (int)r.height);
        Texture2D croppedNewTexture = new Texture2D((int)r.width, (int)r.height);
        for(int i=0; i < (int)r.width; i++)
        {
            for(int j=0; j < (int)r.height; j++)
            {
                croppedNewTexture.SetPixel(i,j, croppedPixels[i*(int)r.width + j]);
            }
        }
        croppedNewTexture.Apply();
        displaySprite.style.backgroundImage = croppedNewTexture;
    }
}