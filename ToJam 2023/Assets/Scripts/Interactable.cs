using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [HideInInspector] public bool canClick = false;
    [SerializeField] Color interactableColor;
    [SerializeField] Color selectedColor;
    public bool canInteractWithTwoHands;
    public bool canInteractWithOneHand;
    public bool canInteractWithOneLeg;
    public bool canInteractWithTwoLegs;

    private void Update() 
    {
        if(canClick)   
        {
            GetComponent<SpriteRenderer>().color = interactableColor;
        }
    }

    private void OnMouseOver() 
    {
        if(canClick)
        {
            GetComponent<SpriteRenderer>().color = interactableColor;
        }
    }

    private void OnMouseDown()
    {
        //if(canClick)
    }
}
