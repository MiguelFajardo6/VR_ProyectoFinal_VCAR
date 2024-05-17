using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour
{
    public int checkNumber;
    public bool collisionOccurred = false;
    public bool collisionExit = false;
    public bool VueltaFin = false;
    void OnTriggerExit(Collider other)
    {
        collisionExit = true;
        Debug.Log("Colision" + checkNumber +"pasado");
    }
    void OnTriggerEnter(Collider other)
    {
        collisionOccurred = true;
    }

    public bool GetCollisionState()
    {
        return collisionOccurred;
    }

    public bool GetCollisionExit()
    {
        return collisionExit;
    }

    public void ResetCollisionState()
    {
        collisionOccurred = false;
        collisionExit = false;
    }
    public void ResetCollisionOcurred()
    {
        collisionOccurred = false;
        
    }
}
