using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Limb: MonoBehaviour
{
    [HideInInspector]public List<Interactable> interactables = new List<Interactable>(1);
    private bool isInteracting = false;
    [HideInInspector] public bool isUsed = false;
    private Interactable interactedObject;

    public bool isArm;
    public bool isLeg;

    public GameObject locationSelectorIcon;

    [HideInInspector]public bool playerNearby = false;
    [HideInInspector] public bool playerIsThrowable = false;
    [HideInInspector] public ThrowBody body;

    private void Start()
    {
        body = GameObject.FindWithTag("Body").GetComponent<ThrowBody>(); 
        body.limbScript = this;
        interactables.Clear();
    }


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
            Instantiate(locationSelectorIcon);
            
            isInteracting = true;
            if (!isUsed)
            {
                foreach (Interactable interactable in interactables)
                {
                    interactable.limbCanInteract = interactable.InteractionCheck(this.gameObject);
                    interactable.limbScript = this;
                    interactable.Identify();
                    isUsed= true;
                    //interactable.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                }
            }
            else if (playerNearby)
            {
                Debug.Log("Select body to pick up");
                playerIsThrowable = true;
            }
            else if (!playerNearby)
            {
                playerIsThrowable = false;
            }
            else
            {
                foreach (Interactable interactable in interactables)
                {
                    interactable.limbScript = null;
                    interactable.Deselect();
                }
                
                interactedObject.limbsUsed -= 1;


            }
        }
        else if (playerNearby && !isInteracting)
        {
            Debug.Log("Select body to pick up");
            playerIsThrowable = true;
        }
        else if (!playerNearby)
        {
            playerIsThrowable = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Interactable")
        {
            interactables.Add(other.gameObject.GetComponent<Interactable>());

        }
        if (other.tag == "Body")
        {
            Debug.Log("Body is ready to be picked up");
            playerNearby = true;
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
        if (other.tag == "Body")
        {
            Debug.Log("Body can no longer be picked up");
            playerNearby = false;
        }
    }
}
