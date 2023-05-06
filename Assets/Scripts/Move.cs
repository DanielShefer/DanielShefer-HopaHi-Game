using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    // Define parameters to use in the editor 
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotateSpeed = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;    
    [SerializeField] ParticleSystem rightThtusterParticles;
    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();  
    }
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }

        else if (Input.GetKey(KeyCode.Space))
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }

        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }

        else
            {
                StopRotating();
            }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);  //audio play when the player hit space key
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }
     void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    private void RotateRight()
    {
        ApplyRotation(-rotateSpeed);
        if (!leftThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Play();
        }
    }
    private void StopRotating()
    {
        rightThtusterParticles.Stop();
        leftThrusterParticles.Stop();
    }

    private void RotateLeft()
    {
        ApplyRotation(rotateSpeed);
        if (!rightThtusterParticles.isPlaying)
        {
            rightThtusterParticles.Play();
        }
    }

    public void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime); //active the rotation 
        rb.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }
}
