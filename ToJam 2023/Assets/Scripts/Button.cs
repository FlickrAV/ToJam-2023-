using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToCall;
    [SerializeField] private string[] functionsToCall;
    private Interactable interactableScript;

    private void Start() 
    {
        interactableScript = GetComponent<Interactable>();
    }


    public void ButtonPressed()
    {
        for(int i = 0; i < objectsToCall.Length;i++)
        {
            objectsToCall[i].SendMessage(functionsToCall[i]);
        }
    }   

    private void OnMouseDown() 
    {
        if(interactableScript.InRange())    
        {
            if(interactableScript.limbCanInteract)
            {
                interactableScript.limbsUsed += 1;
                if(interactableScript.limbsUsed == interactableScript.arms || interactableScript.limbsUsed == interactableScript.legs)
                {
                    ButtonPressed();
                    interactableScript.limbScript.isUsed = true;
                    interactableScript.limbScript.gameObject.transform.position = Vector3.MoveTowards(interactableScript.limbScript.gameObject.transform.position, transform.position, 5);
                }
            }
            else if(interactableScript.PlayerInteractionCheck())
            {
                ButtonPressed();
            }
        }
    }
}
