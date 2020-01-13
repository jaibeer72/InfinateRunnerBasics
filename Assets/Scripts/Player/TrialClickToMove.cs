using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class TrialClickToMove : MonoBehaviour
{
    RaycastHit hitInfo = new RaycastHit();
    NavMeshAgent agent;
    Transform curObject;
    //public Transform targ; 

    public bool cover = false;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
                if (hitInfo.transform.gameObject.tag == "Cover")
                {
                    agent.destination = hitInfo.transform.GetChild(0).position;
                    curObject = hitInfo.transform;
                    //targ = hitInfo.transform; 
                }
                if(hitInfo.transform.gameObject.tag == "Floor")
                {
                    agent.destination = hitInfo.point;
                    curObject = hitInfo.transform;
                   // targ.position = hitInfo.point;
                    cover = false;
                }
        }

        if (curObject?.gameObject.tag == "Cover" )
        {
            if (agent.remainingDistance <= 0.001f)
            {
                transform.localRotation = Quaternion.Lerp(transform.localRotation, curObject.localRotation, Time.deltaTime * 5f);
            }
        }
       

        if (agent.isOnOffMeshLink)
        {
            Vector3 test;
            test.x = agent.currentOffMeshLinkData.endPos.x - agent.currentOffMeshLinkData.startPos.x;
            test.y = agent.currentOffMeshLinkData.endPos.y - agent.currentOffMeshLinkData.startPos.y;
            test.z = agent.currentOffMeshLinkData.endPos.z - agent.currentOffMeshLinkData.startPos.z;
            this.transform.forward = test;
        }
    }
}
