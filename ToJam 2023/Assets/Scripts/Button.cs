using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToCall;
    [SerializeField] private string[] functionsToCall;

    [SerializeField] private GameObject[] objectsToCallOnDepress;
    [SerializeField] private string[] functionsToCallOnDepress;

    private bool isPressed = false;

    private Interactable interactableScript;

    public SpriteRenderer spriteRender;
    public Sprite spriteOn;
    public Sprite spriteOff;

    public SpriteRenderer spriteRenderDark;

    private void Start() 
    {
        interactableScript = GetComponent<Interactable>();
    }

    private void Update()
    {
        if (interactableScript.playerInteract)
        {
            if (interactableScript.thingsInRange == 0 && isPressed)
            {
                ButtonDepressed();
            }
        }
        else if (interactableScript.needsArms)
        {
            if (interactableScript.armsInRange == 0 && isPressed)
            {
                ButtonDepressed();
            }
        }
        else if (interactableScript.needsLegs)
        {
            if (interactableScript.legsInRange == 0 && isPressed)
            {
                ButtonDepressed();
            }
        }
    }

    public void ButtonPressed()
    {
        for (int i = 0; i < objectsToCall.Length; i++)
        {
            spriteRender.sprite = spriteOn;
            spriteRenderDark.sprite = spriteOn;
            objectsToCall[i].SendMessage(functionsToCall[i]);
            isPressed = true;

            Destroy(GameObject.Find("Location Selector(Clone)"));
        }
    }

    public void ButtonDepressed()
    {
        for (int i = 0; i < objectsToCallOnDepress.Length; i++)
        {
            spriteRender.sprite = spriteOff;
            spriteRenderDark.sprite = spriteOff;
            objectsToCallOnDepress[i].SendMessage(functionsToCallOnDepress[i]);
            isPressed = false;
        }
    }

    private void OnMouseDown() 
    {
        if(interactableScript.InRange() && !isPressed)    
        {
            if(interactableScript.limbCanInteract)
            {
                //interactableScript.limbsUsed += 1;
                if (interactableScript.needsArms)
                {
                    if(interactableScript.armsInRange == interactableScript.arms)
                    {
                        interactableScript.playerCanInteract = false;
                        interactableScript.playerInteract = false;  
                        ButtonPressed();      
                        interactableScript.limbScript.isUsed = true;
                        interactableScript.limbScript.gameObject.transform.position = Vector3.MoveTowards(interactableScript.limbScript.gameObject.transform.position, transform.position, 5);
                    }
                }
                else if (interactableScript.needsLegs)
                {
                    if (interactableScript.legsInRange == interactableScript.legs)
                    {
                        interactableScript.playerCanInteract = false;
                        interactableScript.playerInteract = false;    
                        ButtonPressed();
                        interactableScript.limbScript.isUsed = true;
                        interactableScript.limbScript.gameObject.transform.position = Vector3.MoveTowards(interactableScript.limbScript.gameObject.transform.position, transform.position, 5);
                    }
                }
                /*if(interactableScript.limbsUsed == interactableScript.arms || interactableScript.limbsUsed == interactableScript.legs)
                {
                    ButtonPressed();
                    interactableScript.limbScript.isUsed = true;
                    interactableScript.limbScript.gameObject.transform.position = Vector3.MoveTowards(interactableScript.limbScript.gameObject.transform.position, transform.position, 5);
                }*/
            }
            if (interactableScript.PlayerInteractionCheck() && interactableScript.playerInteract)
            {
                ButtonPressed();
            }
        }
    }
}
