using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] Color interactableColor;
    [SerializeField] Color selectedColor;
    [SerializeField] private LimbDetatchMenu limbStateScript;

    [HideInInspector] public int thingsInRange = 0;
    [HideInInspector] public int armsInRange = 0;
    [HideInInspector] public int legsInRange = 0;

    [Header("Required limbs to activate")]
    public bool needsArms;
    public bool needsLegs;
    [Tooltip("Number of arms needed to activate")] public int arms = 0;
    [Tooltip("Number of legs needed to activate")]public int legs = 0;
    [HideInInspector] public bool limbCanInteract = false;
    [HideInInspector] public Limb limbScript;

    [HideInInspector] public bool playerCanInteract = true;

    //Can the player interact with their body?
    [HideInInspector] public bool playerInteract = false;

    public void Identify()
    {
        if (limbScript.isArm)
        {
            armsInRange += 1;
        }
        else if (limbScript.isLeg)
        {
            legsInRange += 1;
        }
    }
    public void Deselect()
    {
        if (limbScript.isArm)
        {
            armsInRange = 0;
            armsInRange = Mathf.Clamp(armsInRange, 0, 10);
        }
        else if (limbScript.isLeg)
        {
            legsInRange = 0;
            legsInRange = Mathf.Clamp(legsInRange, 0, 10);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            thingsInRange += 1;
            if(playerCanInteract)
                playerInteract = true;
            /*//Arm check
            InteractionCheck(other.transform.gameObject);
            if (GameObject.Find("Left Arm")|| limbStateScript.hasLimb[3])
            {
                armsInRange += 1;
            }
            if (limbStateScript.hasLimb[2] && limbStateScript.hasLimb[3])
            {
                armsInRange += 2;
            }

            //Leg check
            if (limbStateScript.hasLimb[4] || limbStateScript.hasLimb[5])
            {
                legsInRange += 1;
            }
            if (limbStateScript.hasLimb[4] && limbStateScript.hasLimb[5])
            {
                legsInRange += 2;
            }*/
        }

        if(other.tag == "Limb")
        {
            /*InteractionCheck(other.transform.gameObject);
            thingsInRange += 1;
            //Arm check
            //InteractionCheck(other.transform.gameObject);
            if (limbScript.gameObject.name.Contains("Arm"))
            {
                armsInRange += 1;
            }

            //Leg check
            else if (limbStateScript.hasLimb[4] || limbStateScript.hasLimb[5])
            {
                legsInRange += 1;
            }
            else if (limbStateScript.hasLimb[4] && limbStateScript.hasLimb[5])
            {
                legsInRange += 2;
            }*/
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.name == "Player")
        {
            thingsInRange = 0;
            if (playerCanInteract)
                playerInteract = false;
            //Arm check
            /*InteractionCheck(other.transform.gameObject);
            if (limbStateScript.hasLimb[2] || limbStateScript.hasLimb[3])
            {
                armsInRange -= 1;
            }
            else if (limbStateScript.hasLimb[2] && limbStateScript.hasLimb[3])
            {
                armsInRange -= 2;
            }

            //Leg check
            else if (limbStateScript.hasLimb[4] || limbStateScript.hasLimb[5])
            {
                legsInRange -= 1;
            }
            else if (limbStateScript.hasLimb[4] && limbStateScript.hasLimb[5])
            {
                legsInRange -= 2;
            }*/
        }

        if(other.tag == "Limb")
        {
            /*InteractionCheck(other.transform.gameObject);
            thingsInRange -= 1;
            //Arm check
            InteractionCheck(other.transform.gameObject);
            if (limbStateScript.hasLimb[2] || limbStateScript.hasLimb[3])
            {
                armsInRange -= 1;
            }
            if (limbStateScript.hasLimb[2] && limbStateScript.hasLimb[3])
            {
                armsInRange -= 2;
            }

            //Leg check
            if (limbStateScript.hasLimb[4] || limbStateScript.hasLimb[5])
            {
                legsInRange -= 1;
            }
            if (limbStateScript.hasLimb[4] && limbStateScript.hasLimb[5])
            {
                legsInRange -= 2;
            }*/
        }
    }

    public bool InRange()
    {
        if (!playerInteract)
        {
            if (needsArms)
            {
                if (armsInRange > 0)
                    return true;
                else
                    return false;
            }

            else if (needsLegs)
            {
                if (legsInRange > 0)
                    return true;
                else
                    return false;
            }
        }

        if (thingsInRange > 0)
            return true;        
        else
            return false;
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
        if(needsArms && limbToCheck.name.Contains("Arm"))
        {
            return true;
        }
        else if(needsLegs && limbToCheck.name.Contains("Leg"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
