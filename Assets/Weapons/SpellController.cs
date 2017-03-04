using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour {

    public GameObject waspNestPrefab;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            var newNest = Instantiate(waspNestPrefab, transform.position + transform.forward, Quaternion.identity);
            newNest.transform.rotation = transform.rotation;
            newNest.transform.parent = gameObject.transform;
        }
    }
}
