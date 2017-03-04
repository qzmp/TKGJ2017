﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lumberjack : MonoBehaviour {

    private int treeLayer = 10; //growingTree
    public LayerMask treeMask;
    private Animator anim;
    private bool cutting = false;

    public float moveSpeed;
    public float rotationSpeed;
    private Transform currentTarget;

    public float restTime = 0;
    private float restEnd;

	// Use this for initialization
	void Start () {
        //treeMask = 1 << treeLayer;
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if(nearTree())
        {
            currentTarget = null;
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
                restEnd = Time.time + restTime;
            }
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Moving"))
            {
                goToTree();
            }
        }
	}

    bool nearTree()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        return Physics.Raycast(transform.position, fwd, 0.5f, treeMask);
    }

    void goToTree()
    {
        if(currentTarget == null)
        {
            currentTarget = findNearestTree();
        }
        //rotate to look at the tree
        Vector3 targetDir = currentTarget.position - transform.position;
        float step = rotationSpeed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
        Debug.DrawRay(transform.position, newDir, Color.red);
        transform.rotation = Quaternion.LookRotation(newDir);
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(currentTarget.position - transform.position), rotationSpeed * Time.deltaTime);

        //move towards the tree
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    Transform findNearestTree()
    {
        GameObject nearestTree = GameObject.FindGameObjectWithTag("Tree");
        if (nearestTree == null)
            return null;
        double minDistance = countDistanceTo(nearestTree);

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Tree"))
        {
            if(countDistanceTo(go) < minDistance)
            {
                minDistance = countDistanceTo(go);
                nearestTree = go;
            }
        }
        return nearestTree.transform;
    }

    double countDistanceTo(GameObject tree)
    {
        return Vector3.Distance(transform.position, tree.transform.position);
    }
}
