using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBehaviour : MonoBehaviour
{

    public int maxHp;
    private int hp;

    // Use this for initialization
    void Start()
    {
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if(hp <= 0)
        {
            cutDown();
        }
    }
    void cutDown()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        gameObject.tag = "FellTree";
        gameObject.layer = 8;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Axe")
        {
            hp--;
        }
    }
}

