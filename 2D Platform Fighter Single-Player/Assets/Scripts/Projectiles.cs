using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    float speed = 20f;
    float maxDistance = 20f;
    Vector3 spawnPos;
    // Start is called before the first frame update
    void Start()
    {
        spawnPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left*speed*Time.deltaTime);
        if(Vector3.Distance(spawnPos, transform.position) > maxDistance){
            Destroy(gameObject);
        }
    }
}
