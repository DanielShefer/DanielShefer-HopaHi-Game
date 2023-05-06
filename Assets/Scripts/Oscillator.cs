using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] float period = 3f;  // how fast the object move from a to b 

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    // move object from a to b in loop 
    {
        if (period == 0) { return; }  // protect from 0 bug 
        float cycles = Time.time / period;  // continually growing over time

        const float tau = Mathf.PI * 2;  // constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau);  // going from -1 to 1 

        movementFactor = (rawSinWave + 1f) / 2f;  // recalculated to go from 0 to 1

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
