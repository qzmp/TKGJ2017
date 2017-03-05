using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject waveInformer;
    private float shownInfo = 0;

    private int currentWave = 0;
    private int spawnersTurnedOn = 0;
    private float spawningStart = 0;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        InvokeRepeating("nextWave", 2.0f, 10f);
        if(Time.time - shownInfo > 5)
        {
            waveInformer.SetActive(false);
        }
    }

    void nextWave()
    {
        if (GameObject.FindGameObjectsWithTag("AliveEnemy").Length == 0 && Time.time - spawningStart > 10)
        {
            currentWave++;
            waveInformer.GetComponent<Text>().text = "Wave " + currentWave;
            waveInformer.SetActive(true);
            
            shownInfo = Time.time;


            spawningStart = Time.time;
            spawnersTurnedOn = 0;
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Spawner"))
            {
                go.GetComponent<EnemySpawning>().active = false;
                if (spawnersTurnedOn < currentWave)
                {
                    go.GetComponent<EnemySpawning>().reset();
                    spawnersTurnedOn++;
                }
            }
        }
    }
}
