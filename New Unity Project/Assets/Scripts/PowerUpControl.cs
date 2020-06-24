using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpControl : MonoBehaviour{
    
    [Header("Movement Variables")]
    public float powerUpSpeedXMovement;

    [Header("Other Variables")]
	private Rigidbody2D rbPowerUp;

    [Header("Power Up Type Variables")]
    public string powerUpTypeStr;
    public int powerUpType;

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
		rbPowerUp.velocity = new Vector2 (-powerUpSpeedXMovement,
         rbPowerUp.velocity.y);
	}

    public void GetInitialComponent () {
		rbPowerUp = GetComponent<Rigidbody2D> ();
	}

    public void SetPowerUpType(string typStr, int typVal){
        powerUpTypeStr = typStr;
        powerUpType = typVal;
    }
}
