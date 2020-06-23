using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootControl : MonoBehaviour{

    [Header("Movement Variables")]
    public float speedXMovement;

    [Header("Damage Variables")]
    public int damage;

    [Header("Other Variables")]
	private Rigidbody2D rbShoot;
     private SpriteRenderer rendShoot;

    [Header("Collider Tag Variables")]
    public List<string> allColliderTags;

    [Header("Shoot Type Variables")]
    public bool wasPlayerShoot;
    public string shootTypeString;
    public int shootType;

    void Awake(){
        GetInitialComponent();
    }

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    void FixedUpdate () {
		rbShoot.velocity = new Vector2 (speedXMovement,rbShoot.velocity.y);
	}

    public void SetSpeed(float speed){
        speedXMovement = speed;
    }

    void GetInitialComponent () {
		rbShoot = GetComponent<Rigidbody2D> ();
        rendShoot = GetComponent<SpriteRenderer> ();
	}

    void OnTriggerEnter2D(Collider2D other){
        if(allColliderTags.IndexOf(other.gameObject.tag) != -1){
            switch(other.gameObject.tag){
                case "Enemy":
                if(shootType == other.gameObject.GetComponent<EnemyControl>().enemyType){
                    other.gameObject.GetComponent<EnemyControl>().GetDamage(damage);
                }
                break;
                 case "Player":
                    other.gameObject.GetComponent<PlayerControl>().GetDamage(damage);
                 break;
            }
            Destroy(this.gameObject);
        }
    }

     public void SetShootType(bool player, Sprite spr, int shootVal, string shootStr){
         wasPlayerShoot = player;
         rendShoot.sprite = spr;
         shootType = shootVal;
         shootTypeString = shootStr;
     }

}
