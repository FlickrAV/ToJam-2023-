using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBody : MonoBehaviour
{
    public LimbDetatchMenu limbMenuScript;
    [HideInInspector] public Limb limbScript;
    private bool inRange = false;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Limb")
        {
            inRange = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Limb")
        {
            inRange = false;
        }
    }
    private void OnMouseDown()
    {
        Debug.Log("You clicked the body!");
        if (inRange)
        {
            Debug.Log(limbScript.playerIsThrowable);
            if (limbScript.playerIsThrowable)
            {
                Debug.Log("Body is picked up");
                limbMenuScript.currentLimb = 6;
                limbMenuScript.inThrowMode = true;
            }
            if (limbMenuScript.hasThrown)
            {
                Debug.Log("Body is thrown");
                Destroy(this);
            }
        }
    }
}
