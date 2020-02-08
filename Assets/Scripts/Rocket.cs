﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rcsThrust = 100f;
    Rigidbody rb;
    AudioSource audio; 
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
        Thrust();
        Rotate();
    }
    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space)) // Can Rotate with thrust
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
        

        if (Input.GetKey(KeyCode.A)) // can rotate in left
        {
            float rotateThisFrame = rcsThrust * Time.deltaTime;
            transform.Rotate(Vector3.forward * rotateThisFrame);
        }
        else if (Input.GetKey(KeyCode.D)) // can rotate in right
        {
            float rotateThisFrame = rcsThrust * Time.deltaTime;
            transform.Rotate(-Vector3.forward * rotateThisFrame);
            rb.freezeRotation = false; // resume physisc control of rotation
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                // do nothing now
                print("Friendly");
                break;
            case "Fuel":
                // do nothing now
                print("Fuel");
                break;
            default:
                print("Dead");
                // kill player
                break;
        }

    }
}
