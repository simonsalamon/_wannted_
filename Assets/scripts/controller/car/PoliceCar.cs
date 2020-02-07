using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCar : MonoBehaviour
{
    public Transform PlayerCar;
    public List<wheel> colliders;
    [HideInInspector]
    public float maxMotorTorquo = 1000f;
    public float maxSteertAngle;
    public float maxBreakTorque;
    private float sterring;
    Rigidbody rigi;
    bool flagDestroy = false;
    private void Start() {
        
        rigi.centerOfMass = new Vector3(0,-1.3f,0); 
    }

    private void FixedUpdate() {

        if (!flagDestroy)
        {       
            Vector3 playerPos = findPlayer();
            sterring = maxSteertAngle * playerPos.x;
            foreach (var item in colliders )
            {   
                //      motor torque
                if (item.motorTorque)
                {
                    item.wheelB.brakeTorque = 0;
                    item.wheelR.brakeTorque = 0;

                    item.wheelB.motorTorque = maxMotorTorquo;
                    item.wheelR.motorTorque = maxMotorTorquo;
                }
                //          sterring wheel
                if (item.sterring)
                {
                    float angle = Quaternion.Angle(transform.rotation , Quaternion.Euler(0,0,0));
                    item.wheelB.steerAngle = sterring;
                    item.wheelR.steerAngle = sterring;
                    item.wheelB.transform.rotation = transform.rotation; 
                    item.wheelR.transform.rotation = transform.rotation; 
                    item.wheelB.transform.Rotate(0,sterring,0);
                    item.wheelR.transform.Rotate(0,sterring,0);
                }
                else
                {

                    item.wheelB.transform.rotation = transform.rotation; 
                    item.wheelR.transform.rotation = transform.rotation; 
                }
                ApplyLocalPositionToVisuals(item.wheelR);
                ApplyLocalPositionToVisuals(item.wheelB);
            }
        }
        else
        {
            foreach (var item in colliders)
            {
                item.wheelB.brakeTorque = maxBreakTorque;
                item.wheelR.brakeTorque = maxBreakTorque;
                if (item.wheelB.rpm <10f || item.wheelR.rpm > -10f)
                {
                    Destroy(gameObject , 3f);
                }
            }
        }
    }
    public Vector3 findPlayer(){
        Vector3 DisPlayer = transform.InverseTransformPoint(PlayerCar.position);
        Vector3 Dirction = DisPlayer/DisPlayer.magnitude;
        return Dirction;
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
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "ground" || other.gameObject.tag == "Enemy")
        {
            flagDestroy = true;
            gameManager.Instance.explorecar(transform.position);
        }
    }
}
