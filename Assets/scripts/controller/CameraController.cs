using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{   
    [SerializeField] Transform car;
    public float SmoothPos = 10f;
    public float SmoothRot = 10f;
    [SerializeField] private float distance;
    [SerializeField] private float height;
    private void FixedUpdate() {
        Vector3 WanntedPos;
        WanntedPos = car.TransformPoint(0,height , -distance);
        transform.position = Vector3.Lerp(transform.position , WanntedPos , SmoothPos * Time.deltaTime);
        Quaternion wantedRot = Quaternion.LookRotation(car.position - transform.position , car.up);
        transform.rotation = Quaternion.Lerp(transform.rotation , wantedRot , SmoothRot * Time.deltaTime );
     }
}
