using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{   
    public Transform TargetObject;
    public float followDistance = 5f;
    public float followHeight = 2f;
    public bool smoothedFollow = false;         //toggle this for hard or smoothed follow
    public float smoothSpeed = 5f;
    public bool useFixedLookDirection = false;      //optional different camera mode... fixed look direction
    public Vector3 fixedLookDirection = Vector3.one;

    // Use this for initialization
    void Start ()
    {
        //do something when game object is activated .. if you want to

    }
    
    // Update is called once per frame
    void Update ()
    {
        Vector3 lookToward = TargetObject.position - transform.position;
        if(useFixedLookDirection )
                lookToward  = fixedLookDirection ;


        Vector3 newPos;
        newPos =  TargetObject.position - lookToward.normalized * followDistance;
        newPos.y = TargetObject.position.y + followHeight ;

        transform.position += (newPos - transform.position) * Time.deltaTime * smoothSpeed;


        lookToward = TargetObject.position - transform.position;

        transform.forward = lookToward.normalized;
    }
}
