using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Rocket : MonoBehaviour
{
    static int currentScene = 0;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rcsThrust = 100f;
   
    Rigidbody rb;
    AudioSource audio; 
    enum State { Alive, Dying, Trancending}
    State state = State.Alive;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
        
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
            Thrust();
            Rotate();
        }
    }
    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space) || (SimpleInput.GetAxis("Vertical")!=0)) // Can Rotate with thrust
        {
            rb.AddRelativeForce(Vector3.up * mainThrust);
            if (!audio.isPlaying)
            {
                audio.Play();
            }
        }
        else
        {
            audio.Stop();
        }
    }

    private void Rotate()
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
                print("Friendly");
                break;
            case "Finish":
                // do nothing now
                state = State.Trancending;
                Invoke("LoadNextLevel", 1f);
                
                LoadNextLevel(); break;
            default:
                state = State.Dying;
                Invoke("PlayerDeath", 1f);
                
                break;
        }

    }

    private  void PlayerDeath()
    {
        SceneManager.LoadScene(currentScene);
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
        state = State.Alive;
        currentScene = 1;
    }
}
