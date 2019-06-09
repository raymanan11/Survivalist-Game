using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rotThrust  = 85f; // for the amount of rotation
    [SerializeField] float mainThrust = 85f; // for the upward thrust

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

    void OnCollisionEnter(Collision collision) {
        switch (collision.gameObject.tag) { // determines the collisions based on the game tags
            case "Friendly":
                print("OK");
                break;
            default:
                print("DEAD");
                break;
        }
    }

    private void Thrust() {
        if (Input.GetKey(KeyCode.Space)) {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust); // up is relative to the y arow because it's facing up
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
        float rotationFrame = rotThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.forward * rotationFrame);
        }

        else if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(-Vector3.forward * rotationFrame);
        }

        rigidBody.freezeRotation = false; // physics is back
    }
}
