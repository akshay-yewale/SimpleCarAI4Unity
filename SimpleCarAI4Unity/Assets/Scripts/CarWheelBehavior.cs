using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWheelBehavior : MonoBehaviour {

    public WheelCollider wheelCollider;

    private Vector3 wheelPosition = new Vector3();
    private Quaternion wheelRotataion = new Quaternion();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        wheelCollider.GetWorldPose(out wheelPosition, out wheelRotataion);
        transform.position = wheelPosition;
        transform.rotation = wheelRotataion;
	}
}
