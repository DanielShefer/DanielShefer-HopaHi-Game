using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;

    AudioSource audioSource;
    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;  //toggle collision 
        }
    }

    void OnCollisionEnter(Collision other) 
    {
        if (isTransitioning || collisionDisabled) { return; }
        
        switch (other.gameObject.tag)
         {
            case "Start":
                Debug.Log("This thing is friendly");
                break;

            case "End": //End is the tag attached to object
                StartSuccessSequence(); //when the player success 
                break;

            default: //when the player hit an obstcle
                StartCrashSequence(); 
                break; 
          }   
    }

    void StartSuccessSequence() //what happend when the player success on the level
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success); // SFX when the player success
        successParticles.Play();
        GetComponent<Move>().enabled = false; // קריאה לכפתור תנועה כלא פועל
        Invoke("LoadNextLevel", levelLoadDelay); // תתחיל את השלב הבא בדילאי
    }

    // order to remove the controll of the player when faile
    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash); // SFX when the player crash
        crashParticles.Play();  
        GetComponent<Move>().enabled = false;
        Invoke("ReloadLevel" , levelLoadDelay); // reload the level with a delay of 1 sec
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // level 1
        GetComponent<Move>().enabled = false;
        Invoke("ReloadLevel", 3f);
        int nextSceneIndex = currentSceneIndex +1; //  level 2

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) // אם אין יותר שלבים תחזור לשלב הראשון 
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
   
    }
