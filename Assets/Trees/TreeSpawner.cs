using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour {

    public GameObject tree;
    public float radius;
    public int amount;
    public float minDistance;

	// Use this for initialization
	void Start () {

        for (int i = 0; i < amount; i++)
        {
            Vector3 newPos;
            do
            {
                newPos = randomizePosition();
            }
            while (nearestTreeDist(newPos) < minDistance);

            Instantiate(tree, newPos, Quaternion.identity);
        }
	}

    Vector3 randomizePosition()
    {
        float x = transform.position.x + Random.Range(-radius, radius);
        float z = transform.position.z + Random.Range(-radius, radius);
        RaycastHit hit;
        Physics.Raycast(new Vector3(x, 1000,z), Vector3.down, out hit, Mathf.Infinity);
        return hit.point;
    }

    float nearestTreeDist(Vector3 currentPos)
    {
        GameObject nearestTree = GameObject.FindGameObjectWithTag("Tree");
        if (nearestTree == null)
            return Mathf.Infinity;
        float minDistance = countDistanceTo(nearestTree, currentPos);

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Tree"))
        {
            if (countDistanceTo(go, currentPos) < minDistance)
            {
                minDistance = countDistanceTo(go,currentPos);
                nearestTree = go;
            }
        }
        return minDistance;
    }
    float countDistanceTo(GameObject tree, Vector3 currentPos)
    {
        return Vector3.Distance(currentPos, tree.transform.position);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
