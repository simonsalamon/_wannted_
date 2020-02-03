using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{   
    [SerializeField] Transform car;
    public float Smooth = 10f;
    private Vector3 distance;
    void Start()
    {
        distance = transform.position - car.position ;
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 newPOs = transform.position + distance;

        
        // transform.position = Vector3.Lerp(transform.position , car.position , Smooth * Time.deltaTime);
            
        

        transform.LookAt(car);
         
    }
}
