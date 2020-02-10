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
    [SerializeField] float RPM;
    public List<wheel> wheelsCollider;
    public List<Transform> wheelMesh;
    public float maxMotorTorque;
    public float maxSteeringTorque;
    public float maxBreakTorque;
    public float inertiaFactor;
    private Rigidbody rigi;
    private Touch touch;
    Quaternion initRot;
    int screenWidth;
    float sterring;
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
        // pos.y = -1.7f;
        rigi.centerOfMass = pos;
    }
    private void FixedUpdate() {
        float motor = maxMotorTorque;
        screenWidth = Screen.width;
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);            
            sterring = maxSteeringTorque * sterrtAgnel(touch.position);
        }
        else
        {
            sterring = 0;
        }



        if (gameManager.Instance.State != gameState.lose)
        {
            foreach (var item in wheelsCollider )
            {
                
                item.wheelB.transform.rotation = initRot;
                item.wheelR.transform.rotation = initRot;
                
                //      motor torque
                if (item.motorTorque)
                {
                    item.wheelB.brakeTorque = 0;
                    item.wheelR.brakeTorque = 0;

                    item.wheelB.motorTorque = motor;
                    item.wheelR.motorTorque = motor;
                }
                //          sterring wheel
                if (item.sterring  )
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
                //      break wheel
                if (item.breakTorque && Input.GetKey(KeyCode.Space))
                {
                    item.wheelB.brakeTorque = maxBreakTorque;
                    item.wheelR.brakeTorque = maxBreakTorque;
                }
                ApplyLocalPositionToVisuals(item.wheelR);
                ApplyLocalPositionToVisuals(item.wheelB);
            }            
        }
        else
        {
            foreach (var item in wheelsCollider)
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

    public float sterrtAgnel(Vector2 position){
        float halfScreen=screenWidth/2;
        if(position.x > halfScreen)
            return 1;
        else
            return -1;
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
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "ground")
        {
            gameManager.Instance.loseGame();
        }
    }
}
