using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    private List<Interactable> interactables = new List<Interactable>();
    private bool isInteracting = false;
    [HideInInspector] public bool isUsed = false;
    private Interactable interactedObject;


    private void Update()
    {
        if (Input.GetButtonDown("Cancel") && isInteracting)
        {
            foreach (Interactable interactable in interactables)
            {
                interactable.limbCanInteract = false;
                interactable.gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
                isInteracting = false;
            }
        }
    }

    private void OnMouseDown()
    {
        if (interactables.Capacity > 0 && !isInteracting)
        {
            isInteracting = true;
            if (!isUsed)
            {
                foreach (Interactable interactable in interactables)
                {
                    interactable.limbCanInteract = interactable.InteractionCheck(this.gameObject);
                    interactable.limbScript = this;
                    interactable.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                }
            }
            else
            {
                interactedObject.limbsUsed -= 1;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Interactable")
        {
            interactables.Add(other.gameObject.GetComponent<Interactable>());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Interactable")
        {
            foreach (Interactable script in interactables)
            {
                if (script == other.GetComponent<Interactable>())
                {
                    interactables.Remove(script);
                    interactables.TrimExcess();
                }
            }
        }
    }
}
