using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class WallMover : MonoBehaviour {

    [SerializeField] Vector3 movementVector = new Vector3(0f, 10f, 0f);
    [SerializeField] float period = 2f;

    float movementRange;

    Vector3 startingPos;

    // Start is called before the first frame update
    void Start() {
        startingPos = transform.position; // this is the walls starting position
    }

    // Update is called once per frame
    void Update() {
        print(Time.time);
        if (period <= Mathf.Epsilon) { // if period == 0 the wall movement will not move
            return; 
        }
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2; // about 6.38 or 2 PI
        float SinWave = Mathf.Sin(cycles * tau); // goes from -1 to 1
        

        movementRange = SinWave / 2f + .5f; // SinWave goes from -1 to 1 --> dividing by two goes from -0.5 to 0.5 --> adding 0.5 goes from 0 to 1
        Vector3 displacement = movementVector * movementRange; // when multiplying the two, it determines how much the wall moves
        transform.position = startingPos + displacement; // the new position is based on starting position and how much we move it
    }

}
