using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAgent : MonoBehaviour
{
    public Camera cam;
    public Transform targ;
    Transform curObject;
    NavMeshAgent agent;
    bool cover = false;
    bool turn = false;

    Lerper Pos;
    Lerper Rot;
    Lerper agentRot;

    RaycastHit hitinfo = new RaycastHit();

    private void Start()
    {
        /*lerpers because
          
          1. for the position of the camera    --- \
                                                    |----> these both for the custom angling and positioning of the camera.
          2. for the rotation of the camera    --- /
          
          3. for the agents rotation towards the cover cube when it reaches the cover

         */
        Pos = new Lerper();
        Rot = new Lerper();
        agentRot = new Lerper();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            //as wrote it in your TrialClickToMove
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hitinfo))
            {
                cover = false;
                if (hitinfo.transform.gameObject.tag == "Floor")
                {
                    curObject = hitinfo.transform;
                    agentRot.Reuse();
                    targ.position = hitinfo.point;
                }
                if (hitinfo.transform.gameObject.tag == "Cover")
                {
                    curObject = hitinfo.transform.parent.parent;
                    agentRot.Reuse();
                    targ.position = hitinfo.transform.GetChild(0).position;
                }
            }
        }
        agent.SetDestination(targ.position);

        if (curObject?.gameObject.tag == "Cover")
        {
            //Debug.Log("<color=red>Test</color>");
            if (Vector3.Distance(agent.transform.position, agent.destination) <= 0.1f)
            {
                if (!agentRot.finishedLerp)
                {
                    cover = true;
                    transform.rotation = agentRot.LerpPerfect(transform.rotation, curObject.rotation, 5f);
                }
            }
        }

        //for calculating the inclination of the navmesh link and putting it into the player.
        if (agent.isOnOffMeshLink)
        {
            Vector3 test;
            test.x = agent.currentOffMeshLinkData.endPos.x - agent.currentOffMeshLinkData.startPos.x;
            test.y = agent.currentOffMeshLinkData.endPos.y - agent.currentOffMeshLinkData.startPos.y;
            test.z = agent.currentOffMeshLinkData.endPos.z - agent.currentOffMeshLinkData.startPos.z;
            transform.forward = test;
            turn = true;
        }

        if (turn)
        {
            // here they are predefinied gameobjects .... to be converted to ratios but these have thier advantages (consider this as lazy approch)
            int dir = 0;
            if (curObject?.parent.tag == "Left")
            {
                dir = 3;
            }
            if (curObject?.parent.tag == "Right")
            {
                dir = 2;
            }
            if (curObject?.parent.tag == "Down")
            {
                dir = 1;
            }
            cam.transform.localRotation = Rot.LerpPerfect(cam.transform.localRotation, cam.transform.parent.GetChild(dir).localRotation, 0.25f);
            cam.transform.localPosition = Pos.LerpPerfect(cam.transform.localPosition, cam.transform.parent.GetChild(dir).localPosition, 0.25f);

            if (Rot.finishedLerp && Pos.finishedLerp)
            {
                Rot.Reuse(); // if the lerp finihsed we stop lerping and get ready for the next time the player times
                Pos.Reuse(); // same reason as ^ 
                turn = false;
            }
        }

        GetComponent<Animator>().SetBool("isInCover", cover);
    }

}

// class because to know if the lerping finsihed and to lerp at the same time.
public class Lerper
{
    float lerpTime = 1f;
    public float currentLerpTime;
    float perc = 0f;
    public bool finishedLerp = false;

    public Quaternion LerpPerfect(Quaternion from, Quaternion to, float speed)
    {
        Quaternion obj;
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime > lerpTime)
        {
            currentLerpTime = lerpTime;
        }
        perc = currentLerpTime / lerpTime;

        obj = Quaternion.Lerp(from, to, perc * speed);

        if (currentLerpTime == 1f)
        {
            obj = to;
            finishedLerp = true;
        }

        return obj;
    }

    public Vector3 LerpPerfect(Vector3 from, Vector3 to, float speed)
    {
        Vector3 obj;
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime > lerpTime)
        {
            currentLerpTime = lerpTime;
        }
        perc = currentLerpTime / lerpTime;

        obj = Vector3.Lerp(from, to, perc * speed);
        if (currentLerpTime == 1f)
        {
            obj = to;
            finishedLerp = true;
        }

        return obj;
    }

    public void Reuse()
    {
        currentLerpTime = 0f;
        finishedLerp = false;
    }
}
