using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject objectToCall;
    [SerializeField] private string functionToCall;

    private void ButtonPressed()
    {
        objectToCall.SendMessage(functionToCall);
    }

    private void OnTriggerStay2D(Collider2D other) 
    {   
        if(other.name == "Player")
        {
            if(Input.GetButtonDown("Interact"))
            {
                ButtonPressed();
            }
        }
    }
}
