using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [Range(0,1)] [SerializeField] float movementFactor; // 0 for no slide, 1 for full slide
    Vector3 startingPos; // used to store the initial position
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset;
        offset = startingPos + movementVector * movementFactor;
        transform.position = offset;
    }
}
