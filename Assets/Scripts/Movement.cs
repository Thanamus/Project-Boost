using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Parameters

    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 500f;
    [SerializeField] AudioClip mainEngine; // Audio clip
    // Vector3 thrust = Vector3.up;

    // CACHE
    Rigidbody myRigidbody; // physics
    AudioSource myAudio; // audio

    // States

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myAudio = GetComponent<AudioSource>();
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
            if (!myAudio.isPlaying)
            {
                myAudio.PlayOneShot(mainEngine);
            }
        } else {
            myAudio.Stop();
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
