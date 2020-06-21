using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLiveControl : MonoBehaviour{

    [Header("Movement Variables")]
    public float liveSpeedXMovement;

    [Header("Other Variables")]
	private Rigidbody2D rbLive;

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
		rbLive.velocity = new Vector2 (-liveSpeedXMovement,
         rbLive.velocity.y);
	}

    public void GetInitialComponent () {
		rbLive = GetComponent<Rigidbody2D> ();
	}
}
