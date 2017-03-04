using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour {

    public float spawnFrequency;
    public GameObject enemyPrefab;
    public bool active;

	// Use this for initialization
	void Start () {
	    	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Random.value < spawnFrequency)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        }
	}
}
