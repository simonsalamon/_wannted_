using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{   
    [SerializeField] Transform car;
    public float SmoothPos = 10f;
    [SerializeField] private float height;
    [SerializeField] private float Distance;
    private void FixedUpdate() {
        Vector3 pos = Vector3.zero;
        pos.x = car.position.x + Distance;
        pos.y = height;
        pos.z = car.position.z;
        transform.position = Vector3.Lerp(transform.position , pos , SmoothPos * Time.deltaTime);
        transform.LookAt(car.transform);
    }
}
