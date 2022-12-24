using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    float lastSpawned;
    float interval = 5f;
    float xRange = 16f;
    float yRange = 8f;
    [SerializeField] GameObject[] items;
    // Start is called before the first frame update
    void Start()
    {
        lastSpawned = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(SpawnItem()){
            int random = Random.Range(0, items.Length);
            float randomX = Random.Range(-xRange, xRange);
            float randomY = Random.Range(0, yRange);
            Vector3 spawnPos = new Vector3(transform.position.x + randomX, transform.position.y + randomY, 0);
            Instantiate(items[random], spawnPos, items[random].transform.rotation);
            lastSpawned = Time.time;
        }
    }

    bool SpawnItem(){
        if(Time.time >= lastSpawned + interval){
            return true;
        }
        return false;
    }
}
