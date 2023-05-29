using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBody : MonoBehaviour
{
    public LimbDetatchMenu limbMenuScript;
    public ThrowLocationAllignment allignment;
    [HideInInspector] public Limb limbScript;
    public GameObject player;
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
        if (inRange && limbScript.isArm)
        {
            Debug.Log(limbScript.playerIsThrowable);
            if (limbScript.playerIsThrowable)
            {
                Debug.Log("Body is picked up");
                limbMenuScript.currentLimb = 6;
                limbMenuScript.inThrowMode = true;

            }
            /*if (!limbMenuScript.inThrowMode)
            {
                Debug.Log("Run the code to delete current player");
                limbMenuScript.playerVisionSquare = GameObject.FindWithTag("Vision Square");
                limbMenuScript.playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
                Destroy(player);
            }*/
        }
    }

    /*private void Update()
    {
        if (limbMenuScript.hasThrown)
        {
            /*Debug.Log(limbMenuScript.hasThrown);
            Debug.Log("Run the code to delete current player");
            limbMenuScript.playerVisionSquare = GameObject.FindWithTag("Vision Square");
            limbMenuScript.playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
            allignment.player = GameObject.FindWithTag("Player");
            limbMenuScript.hasThrown = false;
            Debug.Log("Goodybye! (Deletes old player");
            Destroy(player);
        }
    }*/
}
