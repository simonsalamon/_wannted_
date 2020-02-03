using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class wheel{
    public WheelCollider wheelR; 
    public WheelCollider wheelB;
    public bool motorTorque;
    public bool sterring;
    public bool breakTorque;
}

public class carController : MonoBehaviour
{
    public List<wheel> wheelsCollider;
    public List<GameObject> wheelMesh;
    public float maxMotorTorque;
    public float maxSteeringTorque;
    public float maxBreakTorque;
    public float inertiaFactor;
    private Rigidbody rigi;
    Quaternion initRot;
    // Start is called before the first frame update
    private void Awake() {
        initRot = transform.rotation;   
    }
    void Start()
    {
        rigi = GetComponent<Rigidbody>();
        rigi.inertiaTensor *= inertiaFactor;
        setCenterOfMass();
    }

    public void setCenterOfMass(){
        Vector3 pos = Vector3.zero;
        pos.y = -0.9f; 
        Debug.Log(rigi.centerOfMass);
    }
    private void FixedUpdate() {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float sterring = maxSteeringTorque * Input.GetAxis("Horizontal");



        foreach (var item in wheelsCollider )
        {
            
            item.wheelB.transform.rotation = initRot;
            item.wheelR.transform.rotation = initRot;
            if (item.motorTorque)
            {
                item.wheelB.brakeTorque = 0;
                item.wheelR.brakeTorque = 0;
                item.wheelB.motorTorque = motor;
                item.wheelR.motorTorque = motor;

            }
            if (item.sterring && (Input.GetAxis("Horizontal") >= 0.2f || Input.GetAxis("Horizontal") <= -0.2f) )
            {
                Quaternion newRot = Quaternion.Euler(0,sterring,0);
                item.wheelB.steerAngle = sterring;
                item.wheelR.steerAngle = sterring;
            }
            if (item.breakTorque && Input.GetKey(KeyCode.Space))
            {
                item.wheelB.brakeTorque = maxBreakTorque;
                item.wheelR.brakeTorque = maxBreakTorque;
            }
            ApplyLocalPositionToVisuals(item.wheelR);
            ApplyLocalPositionToVisuals(item.wheelB);
        }   
    }
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0) {
            return;
        }
     
        Transform visualWheel = collider.transform.GetChild(0);
     
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);
     
        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }
}
