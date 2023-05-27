using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToCall;
    [SerializeField] private string[] functionsToCall;
    private bool inRange = false;

    private void ButtonPressed()
    {
        for(int i = 0; i < objectsToCall.Length;i++)
        {
            objectsToCall[i].SendMessage(functionsToCall[i]);
        }
    }

    private void Update() 
    {
        if(Input.GetButtonDown("Interact") && inRange)
            {
                ButtonPressed();
            }    
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.name == "Player")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.name == "Player")
        {
            inRange = false;
        }
    }
}
