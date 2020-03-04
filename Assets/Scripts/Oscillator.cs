using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    float period = 2f; 
    [SerializeField] Vector3 movementVector = new Vector3(10f,10f,10f);
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
        // We can't compare float value like == therefore we
        // want to compare with exceptable difference and that is the small value is Eplison 
        if (period <= Mathf.Epsilon) return ;
        print(Mathf.Epsilon);
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2;
        float rawSine = Mathf.Sin(cycles * tau);
        movementFactor = rawSine / 2f;
        
        Vector3 offset = startingPos + movementVector * movementFactor;
        transform.position = offset;

    }
}
