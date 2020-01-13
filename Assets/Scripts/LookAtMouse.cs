using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    public Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos =new Vector3() ;
        
        mousePos = camera.ScreenToViewportPoint(Input.mousePosition);
        mousePos.z = camera.transform.position.z;
        this.transform.LookAt(mousePos);
    }
}
