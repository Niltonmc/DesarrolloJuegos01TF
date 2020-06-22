using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour{
   
    [Header("Movement Variables")]
	public float playerSpeedYMovement;
    private float vertical;

    public float playerSpeedXMovement;
    private float horizontal;

    [Header("Position Variables")]
	public float minPosY;
	public float maxPosY;
	private float currentPosY;
    public float minPosX;
	public float maxPosX;
	private float currentPosX;

    [Header("Other Variables")]
	private Rigidbody2D rbPlayer;
    private Animator animPlayer;

	[Header("Lives Variables")]
	public int playerLives;

    [Header("Shoot Prefab Variables")]
    public GameObject shootPref;
    private ShootControl currentShoot;

    [Header("Shoot Position Variables")]
    public Vector3 shootPositionToCreateOffset;
    
    [Header("Shoot Movement Variables")]
    public float shootSpeedMovement;

    [Header("Shoot Time Variables")]
    public bool canShoot;
    public float timeToShoot;
    private float currentTimeToShoot;

    [Header("Game Manager Variables")]
    public GameManagerControl gmManager;
    
    [Header("SFX Variables")]
    public SFXControl sfxShoot;
    public SFXControl sfxDie;
    public SFXControl sfxLives;

    [Header("Collider Tag Variables")]
    public List<string> allColliderTags;

    void Awake(){
        GetInitialComponent();
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update () {
		currentPosY = Mathf.Clamp(rbPlayer.position.y, minPosY, maxPosY);
        currentPosX = Mathf.Clamp(rbPlayer.position.x, minPosX, maxPosX);
		rbPlayer.position = new Vector2 (currentPosX, currentPosY);

        if (Input.GetButton("Jump") && canShoot == true){
            Shoot();
            currentTimeToShoot = 0;
            canShoot = false;
        }

        if(canShoot == false){
           currentTimeToShoot = currentTimeToShoot + Time.deltaTime;
           if(currentTimeToShoot >= timeToShoot){
               canShoot = true;
           }
        }
	}

	void FixedUpdate () {
		vertical = Input.GetAxisRaw ("Vertical");
        horizontal = Input.GetAxisRaw ("Horizontal");
		rbPlayer.velocity = new Vector2 ( playerSpeedXMovement * horizontal,
         playerSpeedYMovement * vertical);
        animPlayer.SetFloat("verticalDirection",vertical);
	}

	void GetInitialComponent () {
		rbPlayer = GetComponent<Rigidbody2D> ();
        animPlayer = GetComponent<Animator> ();
	}

    void Shoot(){
        currentShoot = Instantiate(shootPref,
        new Vector3 (transform.position.x + shootPositionToCreateOffset.x,
        transform.position.y + shootPositionToCreateOffset.y,
        transform.position.z + shootPositionToCreateOffset.z),
        shootPref.transform.rotation).GetComponent<ShootControl>();
        currentShoot.SetSpeed(shootSpeedMovement);
        sfxShoot.PlaySound();
    }

    public void SetInitialVariables(GameManagerControl gm, int lives, float speedX,float speedY, float timeShoot){
        gmManager = gm;
        playerLives = lives;
        playerSpeedXMovement = speedX;
        playerSpeedYMovement = speedY;
        timeToShoot = timeShoot;
    }

    public void SetSFXs(SFXControl shoot, SFXControl die, SFXControl live){
        sfxShoot = shoot;
        sfxDie = die;
        sfxLives = live;
    }
    public void GetDamage(int val){
        playerLives = playerLives - val;
        sfxDie.PlaySound();
        gmManager.UpdateLives(playerLives);
    }

     public void EarnLive(int val){
        playerLives = playerLives + val;
        sfxLives.PlaySound();
        gmManager.EarnLives(playerLives);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(allColliderTags.IndexOf(other.gameObject.tag) != -1){
            switch(other.gameObject.tag){
                case "Enemy":
                    other.gameObject.GetComponent<EnemyControl>().Die();
                    GetDamage(1);
                break;
                 case "PlayerLive":
                    EarnLive(1);
                    Destroy(other.gameObject);
                break;
            }
        }
    }
}
