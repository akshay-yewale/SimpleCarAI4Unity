using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour {

    public Transform pathToFollow;
    public float MaxSteeringAngle = 40.0f;
    public WheelCollider wheelFrontLeft;
    public WheelCollider wheelFrontRight;

    public float maxSpeed;
    public float minSpeed;

    public Vector3 centerOfMass;

    private List<Transform> nodes;
    private int currentNodeIndex = 0;


    [Header("Sensors")]
    float sensorLength = 5.0f;
    public Vector3 frontSensorPosition = new Vector3(0.0f, 0.2f, 0.3f);


    private bool isAvoiding = false;
    private float avoidingSteeringMultiplier = 0;

    // Use this for initialization

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.localPosition + frontSensorPosition, 0.1f);
    }

    void Start () {

        

        GetComponent<Rigidbody>().centerOfMass = centerOfMass;

        Transform[] childTransform = pathToFollow.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();
        {
            for (int index = 0; index < childTransform.Length; index++)
            {
                if (childTransform[index] != pathToFollow.transform)
                    nodes.Add(childTransform[index]);
            }
        }


    }
	
	// Update is called once per frame
	void FixedUpdate () {

        CheckForSensors();
        ApplySteeringToWheels();
        //Drive();
        CheckForWaypoints();

	}

    private void ApplySteeringToWheels()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNodeIndex].position);
        float angleToSteer = (relativeVector.x / relativeVector.magnitude) * MaxSteeringAngle;
        wheelFrontLeft.steerAngle = angleToSteer;
        wheelFrontRight.steerAngle = angleToSteer;
    }

    private void Drive()
    {
        float currentSpeed = 2 * Mathf.PI * wheelFrontLeft.radius * wheelFrontLeft.rpm * 60 / 1000;

        wheelFrontLeft.motorTorque = 20.0f;
        wheelFrontRight.motorTorque = 20.0f;
    }

    private void CheckForWaypoints()
    {
        if(Vector3.Distance(transform.position, nodes[currentNodeIndex].position) < 1.0f)
        {
            currentNodeIndex = (currentNodeIndex + 1) % nodes.Count;      
        }
    }


    void CheckForSensors()
    {
        RaycastHit hit;
        Vector3 sensorStartPosition = transform.position;
        sensorStartPosition += transform.forward * frontSensorPosition.z;
        sensorStartPosition += transform.forward * frontSensorPosition.y;
        {


            sensorStartPosition += transform.right * 0.3f;
            if (Physics.Raycast(sensorStartPosition, transform.forward, out hit, sensorLength))
            {
                
                Debug.DrawLine(sensorStartPosition, hit.point);
            }
            if (Physics.Raycast(sensorStartPosition, Quaternion.AngleAxis(30.0f, transform.up) * transform.forward, out hit, sensorLength))
            {
                Debug.DrawLine(sensorStartPosition, hit.point);
            }

            sensorStartPosition -= transform.right * 0.6f;
            if (Physics.Raycast(sensorStartPosition, transform.forward, out hit, sensorLength))
            {
                Debug.DrawLine(sensorStartPosition, hit.point);
            }
            if (Physics.Raycast(sensorStartPosition, Quaternion.AngleAxis(-30.0f, transform.up) * transform.forward, out hit, sensorLength))
            {
                Debug.DrawLine(sensorStartPosition, hit.point);
            }


            if (Physics.Raycast(sensorStartPosition, transform.forward, out hit, sensorLength))
            {
                Debug.DrawLine(sensorStartPosition, hit.point);
            }



        }
    }


}


/*
 * 
 * For braking
 * backWheel.braking torque = 
 * */