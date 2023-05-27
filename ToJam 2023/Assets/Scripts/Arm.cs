using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    private List<Interactable> interactables = new List<Interactable>();
    private bool isInteracting = false;
    [HideInInspector] public bool isUsed = false;


    private void Update()
    {
        if (Input.GetButtonDown("Cancel") && isInteracting)
        {
            foreach (Interactable interactable in interactables)
            {
                interactable.limbCanInteract = false;
                interactable.gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
            }
        }
    }

    private void OnMouseDown()
    {
         if (interactables.Capacity > 0)
        {
            isInteracting = true;
            foreach (Interactable interactable in interactables)
            {
                interactable.limbCanInteract = interactable.InteractionCheck(this.gameObject);
                interactable.limbScript = this;
                interactable.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
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
