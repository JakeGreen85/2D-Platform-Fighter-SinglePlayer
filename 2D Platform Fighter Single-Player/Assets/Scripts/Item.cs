using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int ID;
    private void Awake() {
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            Destroy(gameObject);
            Activate(other.gameObject);
        }
    }

    public virtual void Activate(GameObject player){

    }
}
