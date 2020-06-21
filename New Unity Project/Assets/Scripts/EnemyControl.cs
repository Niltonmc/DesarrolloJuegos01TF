using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour{

    [Header("Movement Variables")]
    public float enemySpeedXMovement;

    [Header("Other Variables")]
	private Rigidbody2D rbEnemy;

    [Header("Enemy Container Variables")]
    public AllEnemiesContainerControl allEnemiesContainer;

    [Header("Detect Player Variables")]
     public Transform checkPlayerTransform;
    RaycastHit2D detectPlayerRaycast;
	public float distanceOfPlayerCheck;
	public LayerMask whatIsPlayer;
    public bool playerDetected;
    
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

    [Header("Live Variables")]
    public int enemyLive;

    [Header("Explosion Prefab Variables")]
    public GameObject explosionPref;

    [Header("SFX Variables")]
    public SFXControl sfxShoot;
    public SFXControl sfxDie;

    [Header("Score Variables")]
    public int scoreToAdd;

    void Awake(){
        GetInitialComponent();
    }

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        CheckForPlayer();
        
        if(playerDetected == true && canShoot == true){
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
		rbEnemy.velocity = new Vector2 (-enemySpeedXMovement,
         rbEnemy.velocity.y);
	}

    public void GetInitialComponent () {
		rbEnemy = GetComponent<Rigidbody2D> ();
	}

    public void SelectSpeed(float minSpeed, float maxSpeed){
        enemySpeedXMovement = Random.Range(minSpeed,maxSpeed);
    }

    public void SetEnemyContainer(AllEnemiesContainerControl tmp){
        allEnemiesContainer = tmp;
    }

    public void RemoveFromList(){
        allEnemiesContainer.RemoveFromList(this.gameObject.GetComponent<EnemyControl>());
    }

    void CheckForPlayer(){
		detectPlayerRaycast = Physics2D.Raycast (checkPlayerTransform.position, new Vector2 (transform.localScale.x, 0),
			                                 distanceOfPlayerCheck, whatIsPlayer);
		Debug.DrawRay (checkPlayerTransform.position,  new Vector2 (transform.localScale.x, 0)*distanceOfPlayerCheck, Color.blue);
		
        if (detectPlayerRaycast.collider == true) {
			playerDetected = true;
        }else{
            playerDetected = false;
        }
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

    public void SetSFXs(SFXControl shoot, SFXControl die){
        sfxShoot = shoot;
        sfxDie = die;
    }

    public void GetDamage(int val){
        enemyLive = enemyLive - 1;
        sfxDie.PlaySound();
        if(enemyLive <= 0){
            Die();
        }
    }

    public void Die(){
        allEnemiesContainer.gmManager.UpdateScore(scoreToAdd);
        Instantiate(explosionPref,transform.position,explosionPref.transform.rotation);
        Destroy(this.gameObject);
    }
}
