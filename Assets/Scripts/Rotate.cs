using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    // Start is called before the first frame update
    void FixedUpdate()
    {
        transform.Rotate(0, 0.5f, 0);
    }

    
}