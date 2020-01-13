using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spwaners : MonoBehaviour
{
    public GameObject coverObj;
    void Start()
    {
        initNavAndSpwan();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.DrawSphere(transform.GetChild(i).position + new Vector3(0, 1, 0), 0.15f);
        }
    }
    
    void initNavAndSpwan()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            bool spwan = (Random.value > 0.5f);
            if (spwan)
            {
                GameObject g = Instantiate(coverObj, transform.GetChild(i));
                g.transform.localPosition = new Vector3(0f, 1f, 0f);
                g.transform.localScale = new Vector3(1.25f, 3.5f, 0.0625f);
            }
        }
        transform.parent.GetComponent<NavMeshSurface>().BuildNavMesh();
    }
}
