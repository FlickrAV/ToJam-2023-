using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] Color interactableColor;
    [SerializeField] Color selectedColor;
    [SerializeField] private LimbDetatchMenu limbStateScript;
    public int thingsInRange = 0;
    [Header("Required limbs to activate")]
    [Tooltip("Number of arms needed to activate")] public int arms = 0;
    [Tooltip("Number of legs needed to activate")]public int legs = 0;
    [HideInInspector] public bool limbCanInteract = false;
    [HideInInspector] public Arm limbScript;
    [HideInInspector] public int limbsUsed = 0;


    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            thingsInRange += 1;
        }

        if(other.tag == "Limb")
        {
            InteractionCheck(other.transform.gameObject);
            thingsInRange += 1;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.name == "Player")
        {
            thingsInRange -= 1;
        }

        if(other.tag == "Limb")
        {
            thingsInRange -= 1;
        }
    }

    public bool InRange()
    {
        if(thingsInRange > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool PlayerInteractionCheck()
    {
        if(arms == 1)
        {
            if(limbStateScript.hasLimb[2] || limbStateScript.hasLimb[3])
            {
                return true;
            }
            else
            return false;
        }
        else if(arms == 2)
        {
            if(limbStateScript.hasLimb[2] && limbStateScript.hasLimb[3])
            {
                return true;
            }
            else
            return false;
        }
        else if(legs == 1)
        {
            if(limbStateScript.hasLimb[4] || limbStateScript.hasLimb[5])
            {
                return true;
            }
            else
            return false;
        }
        else if(legs ==2)
        {
            if(limbStateScript.hasLimb[4] && limbStateScript.hasLimb[5])
            {
                return true;
            }
            else 
            return false;
        }
        else 
        {
            return false;
        }
    }

    public bool InteractionCheck(GameObject limbToCheck)
    {
        if(arms > 0 && limbToCheck.name.Contains("Arm"))
        {
            return true;
        }
        else if(legs > 0 && limbToCheck.name.Contains("Leg"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
