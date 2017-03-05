using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspNest : MonoBehaviour {

    public GameObject wasps;
    public GameObject explosion;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            transform.parent = null;
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
        }
    }

    void OnTriggerEnter()
    {
        var w = Instantiate(wasps, transform.position, Quaternion.identity);
        
        var e = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(e, 0.5f);
        Destroy(w, 10);
        Destroy(gameObject);
        
    }
}
