using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerControl : MonoBehaviour{

    [Header("Destroy Tag Variables")]
    public List<string> allDestroyTags;

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }

     void OnTriggerEnter2D(Collider2D other){
        if(allDestroyTags.IndexOf(other.gameObject.tag) != -1){
            switch(other.gameObject.tag){
                case "Enemy":
                    other.gameObject.GetComponent<EnemyControl>().RemoveFromList();
                break;
            }
            Destroy(other.gameObject);
        }
    }
}
