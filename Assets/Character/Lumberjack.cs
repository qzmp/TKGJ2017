using System.Collections;
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

    public int hp = 10;
    private float lastHurtTime = 0;
    public float hurtInterval;

	// Use this for initialization
	void Start () {
        //treeMask = 1 << treeLayer;
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if(hp > 0)
        {
            if (nearTree())
            {
                currentTarget = null;
                if (!cutting)
                {
                    cutting = true;
                    anim.SetBool("nearTree", true);
                }
            }
            else
            {
                if (cutting)
                {
                    currentTarget = null;
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
	}

    bool nearTree()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        return Physics.Raycast(transform.position, fwd, 1f, treeMask);
    }

    void goToTree()
    {
        //if(currentTarget == null)
        //{
            currentTarget = findNearestTree();
        //}
        //rotate to look at the tree
        Vector3 targetDir = currentTarget.position - transform.position;
        float step = rotationSpeed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
        Debug.DrawRay(transform.position, newDir, Color.red);

        var lookPos = currentTarget.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

        //transform.rotation = Quaternion.LookRotation(newDir);
        //transform.rotation = transform.rotation * Quaternion.AngleAxis(0, Vector3.left);
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(currentTarget.position - transform.position), rotationSpeed * Time.deltaTime);

        //move towards the tree
        //transform.position += transform.forward * moveSpeed * Time.deltaTime;

        CharacterController controller = GetComponent<CharacterController>();
        transform.Rotate(0, Input.GetAxis("Horizontal"), 0);
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        
        controller.SimpleMove(forward * moveSpeed);
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

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Damage" && !recentlyHurt())
        {
            if(hp > 0)
                damage();
        }
    }

    void damage()
    {
        hp--;
        
        lastHurtTime = Time.time;
        if(hp <= 0)
        {
            anim.SetTrigger("dying");
            gameObject.tag = "DeadEnemy";
            gameObject.layer = 8;
           // gameObject.GetComponent<Rigidbody>().isKinematic = false;

            //gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
        else
        {
            anim.SetTrigger("hurt");
        }
    }

    bool recentlyHurt()
    {
        return Time.time - lastHurtTime < hurtInterval;
    }

}
