using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Rocket : MonoBehaviour
{
    
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float levelLoadDelay = 2f;
    Rigidbody rb;
    AudioSource audioSource;
    [SerializeField] AudioClip dealthClip;
    [SerializeField] AudioClip thrustClip;
    [SerializeField] AudioClip successClip;
    [SerializeField] ParticleSystem dealthParticle;
    [SerializeField] ParticleSystem thrustParticle;
    [SerializeField] ParticleSystem successParticle;
    enum State { Alive, Dying, Trancending}
     State state = State.Alive;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        
    }

    private void ProcessInput()
    {
        if (state == State.Alive)
        {


            RespondToThrust();
            RespondToRotate();
        }
       
       
    }
    private void RespondToThrust()
    {
        ApplyThrust();
    }

    private void ApplyThrust()
    {
        if (Input.GetKey(KeyCode.Space) || (SimpleInput.GetAxis("Vertical") != 0)) // Can Rotate with thrust
        {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

            if (!audioSource.isPlaying )
            {
                
                
                    audioSource.PlayOneShot(thrustClip);
                
                
            }
            thrustParticle.Play();
        }
        else
        {
            thrustParticle.Stop();
            audioSource.Stop();
        }
    }

    private void RespondToRotate()
    {
        rb.freezeRotation = true; // take manual control of rotation 
        
        
        if (Input.GetKey(KeyCode.A) || (SimpleInput.GetAxis("Horizontal")<0)) // can rotate in left
        {
            float rotateThisFrame = rcsThrust * Time.deltaTime;
            transform.Rotate(Vector3.forward * rotateThisFrame);
        }
        else if (Input.GetKey(KeyCode.D) || (SimpleInput.GetAxis("Horizontal") > 0)) // can rotate in right
        {
            float rotateThisFrame = rcsThrust * Time.deltaTime;
            transform.Rotate(-Vector3.forward * rotateThisFrame);
            rb.freezeRotation = false; // resume physisc control of rotation
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive)
        {
            return ;
        }
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                // do nothing now
                
                break;
            case "Finish":
                // do nothing now
                StartSuccessSequence(); break;
            default:
                StartDeathSequence();

                break;
        }

    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        audioSource.Stop();
        dealthParticle.Play();
        audioSource.PlayOneShot(dealthClip);
        Invoke("PlayerDeath", levelLoadDelay);
    }

    private void StartSuccessSequence()
    {
        state = State.Trancending;
        audioSource.Stop();
        audioSource.PlayOneShot(successClip);
        successParticle.Play();
        Invoke("LoadNextLevel", levelLoadDelay);

       
    }

    private  void PlayerDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
      
    }

    private void LoadNextLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextSceneIndex);
        
        
    }
}
