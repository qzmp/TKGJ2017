using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour {

    public int spawnCount = 10;
    private int spawnedEnemies = 0;
    public float spawnFrequency;
    public GameObject enemyPrefab;
    public bool active;

	// Use this for initialization
	void Start () {
	    	
	}
	
	// Update is called once per frame
	void Update () {
	    if(active && Random.value < spawnFrequency && spawnedEnemies < spawnCount)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            spawnedEnemies++;
        }
	}

    public void reset()
    {
        spawnedEnemies = 0;
        active = true;
    }
}
