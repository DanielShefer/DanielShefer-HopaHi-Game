using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
    
{
    [SerializeField] Vector3 spawnPos;
    Rigidbody Rigidbody;
    new MeshRenderer renderer;
    [SerializeField] float timeToWait = 0.3f;
    [SerializeField] float FallSpeed = 0.5f;

   void Start()
    {
        spawnPos = transform.position;
        renderer = GetComponent<MeshRenderer>();
        Rigidbody = GetComponent< Rigidbody>();  

        renderer.enabled = false;
        Rigidbody.useGravity = false;
    }


    void Update()
    {
        if(Time.time > timeToWait)
        {
            renderer.enabled = true;
            Rigidbody.useGravity = true;   
            Rigidbody.velocity = new Vector3(0, -20, 0); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Untagged"))
        {
            StartCoroutine(TogglePos());
        }
    }

    IEnumerator TogglePos()
    {
        yield return new WaitForSeconds(1.5f);
        transform.position = spawnPos;
        timeToWait = Time.time + timeToWait;
    }
    
}
