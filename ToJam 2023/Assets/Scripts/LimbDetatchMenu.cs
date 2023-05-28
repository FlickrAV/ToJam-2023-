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
    [SerializeField] public PlayerMovement playerScript;
    [SerializeField] private GameObject limbDetatchMenu;
    [SerializeField] public GameObject playerVisionSquare;
    
    private GameObject throwDestinationIcon;
    private ThrowLocationAllignment throwLocationAllignmentScript;

    [HideInInspector] public bool inThrowMode, inDropMode;

    [HideInInspector] public bool[] hasLimb = new bool[7];

    [HideInInspector]public bool hasRightEye, hasLeftEye, hasRightArm, hasLeftArm, hasRightLeg, hasLeftLeg, hasBody = true;

    [HideInInspector]public bool hasArms = true;
    [HideInInspector]public bool hasLegs = true;
    [HideInInspector]public bool hasEyes = true;

    //For throwing body
    [HideInInspector] public ThrowBody body;
    [HideInInspector] public bool hasThrown = false;

    private GameObject player;
    private GameObject movePoint;

    public int currentLimb = 0;

    //For resetting all buttons upon press
    [HideInInspector] public Button interactables;

    private void Start()
    {
        throwDestinationIcon = GameObject.Find("Throw Destination");
        throwLocationAllignmentScript = throwDestinationIcon.GetComponent<ThrowLocationAllignment>();

        //sets both modes to false
        inDropMode= false;
        inThrowMode = false;

        //sets all the booleans in the hasLimb array to true
        for (int i = 0; i < hasLimb.Length; i++)
        {
            hasLimb[i] = true;
        }

        //For body throwing
        player = GameObject.FindWithTag("Player");
        body = GameObject.FindWithTag("Player").GetComponent<ThrowBody>();
        movePoint = GameObject.Find("Move Point");

        interactables = GameObject.FindWithTag("Interactable").GetComponent<Button>();

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
        //hasBody = hasLimb[6];

        //checks if each limb is active and turns the corrisponding button on or off
        for (int i = 0; i < limbs.Length;i++)
        {
            menuButtons[i].SetActive(hasLimb[i]);
        }

        //turns the vision square on or off depending on whether or not the player has eyes
        playerVisionSquare.SetActive(hasEyes);

        //opens the limb menu when the Limb Menu Key is pressed if the menu is not already open
        if (Input.GetButtonDown("Limb Menu") && !limbDetatchMenu.activeSelf)
        {
            limbDetatchMenu.SetActive(true);
        }
        //closes the limb meny when the Limb Menu Key is pressed if the menu is already open
        else if (Input.GetButtonDown("Limb Menu") && limbDetatchMenu.activeSelf)
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
                if (throwLocationAllignmentScript.canThrowLimb)
                {
                    /*
                    if the left mouse button is pressed, 
                    spawns the limb at the location 
                    of the limb destination indicator,
                    turns off the hasLimb
                    boolean and exits throw mode
                    */
                    if (currentLimb < 6)
                        Instantiate(limbs[currentLimb], throwDestinationIcon.transform.position, limbSpawnPosition.rotation);
                    else
                    {
                        movePoint.transform.position = throwDestinationIcon.transform.position;
                        player.transform.position = throwDestinationIcon.transform.position;
                    }
                    menuButtons[currentLimb].SetActive(false);
                    hasLimb[currentLimb] = false;
                    inThrowMode = false;
                    if(currentLimb == 6)
                    {
                        hasThrown = true;
                    }
                }
            }

            //exits throw mode if right click, E or Escape are pressed
            if (Input.GetMouseButtonDown(1))
            {
                inThrowMode = false;
            }
            if (Input.GetButtonDown("Limb Menu"))
            {
                inThrowMode = false;
            }
            if (Input.GetButtonDown("Cancel"))
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
                if (throwLocationAllignmentScript.canThrowLimb)
                {
                    /*
                   if the left mouse button is pressed, 
                   spawns the limb at the location 
                   of the limb destination indicator,
                   turns off the hasLimb
                   boolean and exits drop mode
                   */
                        Instantiate(limbs[currentLimb], throwDestinationIcon.transform.position, limbSpawnPosition.rotation);
                        hasLimb[currentLimb] = false;
                        inDropMode = false;
                }
            }

            //exits throw mode if right click, E or Escape are pressed
            if (Input.GetMouseButtonDown(1))
            {
                inDropMode = false;
            }
            if (Input.GetButtonDown("Limb Menu"))
            {
                inDropMode = false;
            }
            if (Input.GetButtonDown("Cancel"))
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
        if (!hasLeftEye && !hasRightEye)
        {
            hasEyes= false;
        }
        else
        {
            hasEyes = true;
        }

        if (!hasLeftArm && !hasRightArm)
        {
            hasArms= false;
        }
        else
        {
            hasArms = true;
        }

        if (!hasRightLeg && !hasLeftLeg)
        {
            hasLegs= false;
        }
        else
        {
            hasLegs = true;
        }


        if (Input.GetButtonDown("Reset"))
        {
            Regenerate();
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


    public void Regenerate()
    {
        //sets all the hasLimb booleans back to true
        for (int i = 0; i < hasLimb.Length; i++)
        {
            hasLimb[i] = true;
        }

        /*
        if (GameObject.Find("Right Eye(Clone)") != null)
        {
            GameObject rightEye= GameObject.Find("Right Eye(Clone)");
            Limb limb = rightEye.GetComponent<Limb>();
            if (limb.interactables.Capacity > 0)
            {
                limb.interactables.Clear();
            }
        }
        */
        //GameObject.Find("Left Eye(Clone)").GetComponent<Limb>().interactables.Clear();

        if (GameObject.Find("Right Arm(Clone)") != null)
        {
            GameObject.Find("Right Arm(Clone)").GetComponent<Limb>().interactables.Clear();
        }

        if (GameObject.Find("Left Arm(Clone)") != null)
        {
            GameObject.Find("Left Arm(Clone)").GetComponent<Limb>().interactables.Clear();
        }

        if (GameObject.Find("Right Leg(Clone)") != null)
        {
            GameObject.Find("Right Leg(Clone)").GetComponent<Limb>().interactables.Clear();
        }

        if (GameObject.Find("Left Leg(Clone)") != null)
        {
            GameObject.Find("Left Leg(Clone)").GetComponent<Limb>().interactables.Clear();
        }

        //interactables.ButtonDepressed();


        //destroys all detatched limbs
        Destroy(GameObject.Find("Right Eye(Clone)"));
        Destroy(GameObject.Find("Left Eye(Clone)"));        
        Destroy(GameObject.Find("Right Arm(Clone)"));
        Destroy(GameObject.Find("Left Arm(Clone)"));
        Destroy(GameObject.Find("Right Leg(Clone)"));
        Destroy(GameObject.Find("Left Leg(Clone)"));
    }
}
