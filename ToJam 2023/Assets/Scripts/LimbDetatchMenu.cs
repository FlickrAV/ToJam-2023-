using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LimbDetatchMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] limbs;
    [SerializeField] private GameObject[] menuButtons;
    [SerializeField] private Transform limbSpawnPosition;
    [SerializeField] private PlayerMovement playerScript;
    [SerializeField] private GameObject limbDetatchMenu;
    [SerializeField] private GameObject throwDestinationIcon;
    [SerializeField] private GameObject playerVisionSquare;

    [HideInInspector] public bool inThrowMode, inDropMode;

    private bool[] hasLimb = new bool[6];

    private bool hasRightEye, hasLeftEye, hasRightArm, hasLeftArm, hasRightLeg, hasLeftLeg = true;

    private bool hasArms = true;
    private bool hasLegs = true;
    private bool hasEyes = true;

    private int currentLimb = 0;

    private void Start()
    {
        //sets both modes to false
        inDropMode= false;
        inThrowMode = false;

        //sets all the booleans in the hasLimb array to true
        for (int i = 0; i < limbs.Length; i++)
        {
            hasLimb[i] = true;
        }
    }

    private void Update()
    {
        //sets every has[Insert Limb] boolean equal to their numbered counterpart
        hasRightEye= hasLimb[0];
        hasLeftEye= hasLimb[1];
        hasRightArm= hasLimb[2];
        hasLeftArm= hasLimb[3];
        hasRightLeg= hasLimb[4];
        hasLeftLeg= hasLimb[5];

        //opens the limb menu when the E key is pressed if the menu is not already open
        if (Input.GetKeyDown(KeyCode.E) && !limbDetatchMenu.activeSelf)
        {
            limbDetatchMenu.SetActive(true);
        }
        //closes the limb meny when the E key is pressed if the menu is already open
        else if (Input.GetKeyDown(KeyCode.E) && limbDetatchMenu.activeSelf)
        {
            limbDetatchMenu.SetActive(false);
        }


        if (inThrowMode)
        {
            //closes the limb menu and turns on the limb destination indicator
            limbDetatchMenu.SetActive(false);
            throwDestinationIcon.SetActive(true);

            if (Input.GetMouseButtonDown(0))
            {
                /*
                if the left mouse button is pressed, 
                spawns the limb at the location 
                of the limb destination indicator,
                turns off the corrisponding button
                in the menu, turns off the hasLimb
                boolean and exits throw mode
                */
                    Instantiate(limbs[currentLimb], throwDestinationIcon.transform.position, limbSpawnPosition.rotation);
                    menuButtons[currentLimb].SetActive(false);
                    hasLimb[currentLimb] = false;
                    inThrowMode = false;
            }

            //exits throw mode if right click, E or Escape are pressed
            if (Input.GetMouseButtonDown(1))
            {
                inThrowMode = false;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                inThrowMode = false;
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                inThrowMode = false;
            }


        }
        else if (inDropMode)
        {
            //closes the limb menu and turns on the limb destination indicator
            limbDetatchMenu.SetActive(false);
            throwDestinationIcon.SetActive(true);

            if (Input.GetMouseButtonDown(0))
            {
                /*
               if the left mouse button is pressed, 
               spawns the limb at the location 
               of the limb destination indicator,
               turns off the corrisponding button
               in the menu, turns off the hasLimb
               boolean and exits drop mode
               */
                    Instantiate(limbs[currentLimb], throwDestinationIcon.transform.position, limbSpawnPosition.rotation);
                    menuButtons[currentLimb].SetActive(false);
                    hasLimb[currentLimb] = false;
                    inDropMode = false;
            }

            //exits throw mode if right click, E or Escape are pressed
            if (Input.GetMouseButtonDown(1))
            {
                inDropMode = false;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                inDropMode = false;
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                inDropMode = false;
            }
        }
        //turns off the limb destination indicator when you are no longer in throw or drop mode
        else
        {
            throwDestinationIcon.SetActive(false);
        }



        //controls the booleans that say whether or not you have both of a certain type of limb
        if (!hasLeftArm && !hasRightArm)
        {
            hasArms= false;
        }
        if (!hasRightLeg && !hasLeftLeg)
        {
            hasLegs= false;
        }
        if (!hasLeftEye && !hasRightEye)
        {
            hasEyes= false;
        }

        //turns off the vision square if the player doesn't have any eyes
        if (!hasEyes)
        {
            playerVisionSquare.SetActive(false);
        }
    }


    public void ButtonPressed(GameObject limbToDetatch)
    {
        //sets the currentLimb variable equal to limbToDetatch for the rest of the script to use
        currentLimb = int.Parse(limbToDetatch.name);

        if (hasArms)
        {
            //enable throw mode if the player has any arms left when a button is selected in the limb menu
            inThrowMode = true;

            //turns off the limb menu
            limbDetatchMenu.SetActive(false);
        }
        else if (!hasArms)
        {
            //enable drop mode if the player has no arms left when a button is selected in the limb menu
            inDropMode= true;

            //turns off the limb menu
            limbDetatchMenu.SetActive(false);
        }
    }
}
