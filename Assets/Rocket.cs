using UnityEngine.SceneManagement;
using UnityEngine;

public class Rocket : MonoBehaviour {
    [SerializeField] float rotThrust = 85f; // for the amount of rotation
    [SerializeField] float mainThrust = 85f; // for the upward thrust

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip levelPass;

    [SerializeField] ParticleSystem mainEngineParticles; // the built in Unity ParticleSystem allows put any particle in the lever and use the .Play to show effect
    [SerializeField] ParticleSystem explosionParticles;
    [SerializeField] ParticleSystem levelPassParticles;


    Rigidbody rigidBody; // declare a variable rigidBody in order to access the RigidBody script
    AudioSource audioSource;

    enum State {Alive, Dying, Transcending};
    State state = State.Alive;

    // Start is called before the first frame update
    void Start() {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update() {
        if (state == State.Alive) { // if rocket hits obstacle, stops any thrusting or user controls but if rocket hasn't hit anything then controls are normal
            ReactThrustInput();
            ReactRotatInput();
        }
    }

    private void ReactThrustInput() {
        if (Input.GetKey(KeyCode.Space)) {
            ApplyThrust();
        }
        else {
            audioSource.Stop();
            mainEngineParticles.Stop(); // once user stops pressing space, music stops and thrusting particles stop
        }

    }

    private void ApplyThrust() {

        rigidBody.AddRelativeForce(Vector3.up * mainThrust); // deals with the upward thrust of rocket and audio
        if (!audioSource.isPlaying) {
            audioSource.PlayOneShot(mainEngine);
        }
        mainEngineParticles.Play(); // plays thrusting particles

    }

    private void ReactRotatInput() {
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

    void OnCollisionEnter(Collision collision) { // deals with what happens when rocket collide with objects
        if (state != State.Alive) {
            return;
        }
        switch (collision.gameObject.tag) {
            case "Friendly":
                break;
            case "Finish": // if rocket hits the platform with the tag "Finish"
                SuccessSequence();
                break;

            default: // if the rocket hits any objects that have tags that are untagged
                DeathSequence();
                break;
        }
    }

    private void SuccessSequence() {
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(levelPass);
        levelPassParticles.Play();
        Invoke("LoadNextLevel", 1.5f); // load the next scene after one second
    }

    private void DeathSequence() {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(deathSound);
        explosionParticles.Play();
        Invoke("LoadFirstLevel", 1.5f);
    }

    private void LoadNextLevel() {
        SceneManager.LoadScene(1); // this is how to switch to next level
    }

    private void LoadFirstLevel() { // this is to load first level again if you die
        SceneManager.LoadScene(0);
    }

}
