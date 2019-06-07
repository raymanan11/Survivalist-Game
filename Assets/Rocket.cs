using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody; // declare a variable rigidBody in order to access the RigidBody script
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start() {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

    }


    // Update is called once per frame
    void Update() {
        Thrust();
        Rotate();
    }


    private void Thrust() {
        if (Input.GetKey(KeyCode.Space)) {
            rigidBody.AddRelativeForce(Vector3.up); // up is relative to the y arow because it's facing up
            if (!audioSource.isPlaying) {
                audioSource.Play();
            }
        }

        else {
            audioSource.Stop();
        }
    }

    private void Rotate() {
        rigidBody.freezeRotation = true; // this is before we control the rotation of rocket

        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.forward);
        }

        else if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(-Vector3.forward);
        }

        rigidBody.freezeRotation = false; // physics is back
    }
}
