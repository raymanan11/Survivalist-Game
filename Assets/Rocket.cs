using UnityEngine.SceneManagement;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rotThrust  = 85f; // for the amount of rotation
    [SerializeField] float mainThrust = 85f; // for the upward thrust

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
        if (state == State.Alive) {
            Thrust();
            Rotate();
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (state != State.Alive) {
            return;
        }

        switch (collision.gameObject.tag) { // determines the collisions based on the game tags
            case "Friendly":
                break;
            case "Finish":
                state = State.Transcending;
                Invoke("LoadNextLevel", 1f); // load the next scene after one second
                break;

            default:
                state = State.Dying;
                Invoke("LoadFirstLevel", 1f);
                break;
        }
    }

    private void LoadNextLevel() {
        SceneManager.LoadScene(1);
    }

    private void LoadFirstLevel() {
        SceneManager.LoadScene(0);
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
