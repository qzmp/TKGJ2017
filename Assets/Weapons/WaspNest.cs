using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspNest : MonoBehaviour {

    public GameObject wasps;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().AddForce(transform.forward * 9999);
        }
    }

    void OnTriggerEnter()
    {
        Instantiate(wasps, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
