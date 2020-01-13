using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPatroler : MonoBehaviour
{
    Vector3 startPos;
    Vector3 endPos;
    Vector3 target;
    NavMeshAgent agent;
    GameObject[] patrolPoints;

    void Start()
    {
       // Debug.Log(GameObject.FindGameObjectWithTag("The Prism").GetComponent<AiPatrolPointHolder>().pPoints.Count);
        patrolPoints = new GameObject[GameObject.FindGameObjectWithTag("The Prism").GetComponent<AiPatrolPointHolder>().pPoints.Count];
        patrolPoints = GameObject.FindGameObjectWithTag("The Prism").GetComponent<AiPatrolPointHolder>().pPoints.ToArray();
        agent = GetComponent<NavMeshAgent>();
        int s = Random.Range(0,patrolPoints.Length-1);
        int e = Random.Range(0,patrolPoints.Length-1);

        startPos = patrolPoints[s].transform.position;
        endPos = patrolPoints[e].transform.position;
        target = endPos;
        
        agent.SetDestination(target);
    }
    
    void Update()
    {
        if(Vector3.Distance(agent.transform.position, agent.destination) <= 0.1f)
        {
            endPos = startPos;
            startPos = target;
            target = endPos;
        }
        agent.SetDestination(target);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(startPos, endPos);
    }
}
