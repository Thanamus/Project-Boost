using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody myRigidbody;

    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 500f;
    // Vector3 thrust = Vector3.up;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust(){
        if (Input.GetKey(KeyCode.Space))
        {
            // Debug.Log("Space pressed - Thrusting");
            // thrust = thrust*;
            myRigidbody.AddRelativeForce(Vector3.up*Time.deltaTime * mainThrust);
        }
    }
    void ProcessRotation(){
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // Debug.Log("Left pressed - Rotate left");
            ApplyRotation(rotationThrust);

        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            // Debug.Log("Right pressed - Rotate right");
            ApplyRotation(-rotationThrust);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        //freeze physics system rotation
        myRigidbody.freezeRotation = true;
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationThisFrame);

        //unfreeze physics system rotation
        myRigidbody.freezeRotation = false;
    }

    // end class
}
