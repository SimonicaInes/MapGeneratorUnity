using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System.Collections;

public class CharacterController : MonoBehaviour {
    
    [SerializeField] private float defaultSpeed = 2;
    [SerializeField] private float MaxHealth = 100;
    [SerializeField] private int damageTakingSpeed = 0;
	[SerializeField] private Image healthBarImage;
	[SerializeField] private Text healthBarText;

    private Rigidbody2D m_rigidbody;
    private Collider2D collisionBox;
    float horizontal, vertical;
    private TileData tileData;
    private Tilemap tileMap;
    private float currentHealth;
    private bool timePassed = true;


    private bool alive = true;
    private void Awake() 
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        tileData = (TileData)GameObject.FindGameObjectWithTag("tileMap").GetComponent(typeof(TileData));
        tileMap = tileData.tilemap;
        collisionBox =  GetComponent<Collider2D>();
        currentHealth = MaxHealth;
        healthBarText.text = MaxHealth.ToString();
    }
    private void Start() 
    {
 
    }
    private void Update() 
    {
        if(alive)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
            

            Vector3 currentCharacterPosition = m_rigidbody.position;
            // Debug.Log(currentCharacterPosition);
            // Debug.Log(tileMap.WorldToCell(currentCharacterPosition));
            float speedMultiplier = 1;
            float damageMultiplier = 0;


            Tile t = (Tile)tileMap.GetTile(new Vector3Int(tileMap.WorldToCell(currentCharacterPosition).x, tileMap.WorldToCell(currentCharacterPosition).y, 1));
            ExtraTile extraTile = tileData.GetExtraTile(t);


            if(extraTile != null)
            {
                speedMultiplier = extraTile.speed;
                damageMultiplier = extraTile.damage;

            }

            m_rigidbody.velocity = new Vector2(horizontal * defaultSpeed * speedMultiplier , vertical * defaultSpeed * speedMultiplier);

            

            //float currentSpeed = rigidbody.Get
            
            if(damageMultiplier > 0 )
            {

                if(timePassed)
                {
                    Debug.Log("TIME PASSED");
                    currentHealth -= damageMultiplier;
                    timePassed = false;
                    healthBarText.text = currentHealth.ToString();
                    //Debug.Log("TOOK " + damageMultiplier + " DMG!     HP: " + currentHealth);
                    StartCoroutine(WaitTime(damageTakingSpeed));
                }
                
                
            }

            UpdateHealthBar();
  
        }
        if(currentHealth <= 0)
        {
            Debug.Log("DED");
            m_rigidbody.velocity = Vector2.zero;
            healthBarText.text = "DEAD";
            alive = false;
        }

        
        
    }

    IEnumerator WaitTime( int s)
    {
        yield return new WaitForSeconds(s);
        timePassed = true;

    }

    private void UpdateHealthBar()
    {
        healthBarImage.fillAmount = currentHealth/MaxHealth; 
    }

}