using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellController : MonoBehaviour {

    public GameObject waspNestPrefab;
    public float waspCooldown = 5;
    private float lastWaspCast = 0;
    public Image waspCdImage;

    public Animator anim;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        waspCdImage.fillAmount = Mathf.Clamp((Time.time - lastWaspCast) /waspCooldown, 0, 1);
        if (Input.GetMouseButtonDown(0) && (Time.time - lastWaspCast) > waspCooldown)
        {
            anim.SetTrigger("summon");
            lastWaspCast = Time.time;
            var newNest = Instantiate(waspNestPrefab, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z) + transform.forward * 2, Quaternion.identity);
            newNest.GetComponent<WaspNest>().anim = gameObject.GetComponent<Animator>();
            newNest.transform.rotation = transform.rotation;
            newNest.transform.parent = gameObject.transform.parent.GetChild(0);
        }
    }
}
