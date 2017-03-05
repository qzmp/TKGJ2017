using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellController : MonoBehaviour {

    public GameObject waspNestPrefab;
    public float waspCooldown = 5;
    private float lastWaspCast = 0;
    public Image waspCdImage;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        waspCdImage.fillAmount = Mathf.Clamp((Time.time - lastWaspCast) /waspCooldown, 0, 1);
        if (Input.GetMouseButtonDown(0) && (Time.time - lastWaspCast) > waspCooldown)
        {
            lastWaspCast = Time.time;
            var newNest = Instantiate(waspNestPrefab, transform.position + transform.forward * 2, Quaternion.identity);
            newNest.transform.rotation = transform.rotation;
            newNest.transform.parent = gameObject.transform;
        }
    }
}
