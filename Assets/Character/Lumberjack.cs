using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lumberjack : MonoBehaviour {

    private int layerMask = 7; //growingTree
    private Animator anim;
    private bool cutting = false;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if(nearTree())
        {
            if(!cutting)
            {
                cutting = true;
                anim.SetBool("nearTree", true);
            }
        }
        else
        {
            if(cutting)
            {
                cutting = false;
                anim.SetBool("nearTree", false);
            }
            goToTree();
        }
	}

    bool nearTree()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        return Physics.Raycast(transform.position, fwd, 1, layerMask);
    }

    void goToTree()
    {
        //TODO
    }
}
