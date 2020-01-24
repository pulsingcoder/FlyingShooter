﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
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
        if (Input.GetKey(KeyCode.Space)) // Can Rotate with thrust
        {
            rb.AddRelativeForce(Vector3.up);
            if (!audio.isPlaying)
            {
                audio.Play();
            }
        }
        else
        {
            audio.Stop();
        }
      
        if (Input.GetKey(KeyCode.A)) // can rotate in left
        {
            transform.Rotate(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D)) // can rotate in right
        {
            transform.Rotate(-Vector3.forward);
        }
    }
}
