using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToCall;
    [SerializeField] private string[] functionsToCall;

    private void ButtonPressed()
    {
        for(int i = 0; i < objectsToCall.Length;i++)
        {
            objectsToCall[i].SendMessage(functionsToCall[i]);
        }
    }

    private void OnTriggerStay2D(Collider2D other) 
    {   
        if(other.name == "Interact Range")
        {
            Debug.Log("wah");
            if (Input.GetButtonDown("Interact"))
            {
                Debug.Log("wah");
                ButtonPressed();
            }
        }
    }
}
