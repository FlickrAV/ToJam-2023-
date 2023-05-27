using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    public List<GameObject> nearbyInteractables;

    private void OnMouseDown() 
    {
        foreach(GameObject interactable in nearbyInteractables)
        {
            if(interactable.GetComponent<Interactable>() != null)
            {
                interactable.GetComponent<Interactable>().canClick = true;
            }
        }
    }
}
