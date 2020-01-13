using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrolPoints : MonoBehaviour
{
    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.root.GetComponent<AiPatrolPointHolder>().pPoints.Add(transform.GetChild(i).gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.DrawWireSphere(transform.GetChild(i).position, 0.25f);
        }
    }
}
