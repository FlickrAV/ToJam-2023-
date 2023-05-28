using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowLocationAllignment : MonoBehaviour
{
    public GameObject player;
    public Sprite canThrowIcon, cantThrowIcon;
    [HideInInspector]public LimbDetatchMenu limbManagerScript;
    [HideInInspector]public bool canThrowLimb = false;
    private bool onACollider = false;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        limbManagerScript = GameObject.Find("Limb Manager").gameObject.GetComponent<LimbDetatchMenu>();

        spriteRenderer= gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (canThrowLimb)
        {
            spriteRenderer.sprite = canThrowIcon;
        }
        else if (!canThrowLimb)
        {
            spriteRenderer.sprite = cantThrowIcon;
        }


        //converts the location of the mouse to a location in the world
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        if (limbManagerScript.inThrowMode)
        {
            //moves it's position to the position of the mouse, snapping to the middle of the tile
            transform.position = new Vector3(Mathf.Floor(mousePosition.x) + 0.5f, Mathf.Floor(mousePosition.y) + 0.5f, transform.position.z);
        }
        else if (limbManagerScript.inDropMode)
        {
            //does some math to find the angle between the player and the mouse in order to find where to place it
            Vector2 playerPosition = player.transform.position;
            float angle = Mathf.Atan2(mousePosition.y - playerPosition.y, mousePosition.x - playerPosition.x);

            //moves it's position to one tile away from the player in the direction of the mouse, snapping to the middle of the tile
            transform.position = new Vector2(Mathf.Floor(playerPosition.x + Mathf.Sin(-angle + Mathf.Deg2Rad * 90)) + 0.5f, Mathf.Floor(playerPosition.y + Mathf.Cos(-angle + Mathf.Deg2Rad * 90)) + 0.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Vision Square"))
        {
            if (!onACollider)
            {
                canThrowLimb = true;
            }
        }
        if (collision.gameObject.CompareTag("Collisions"))
        {
            onACollider = true;
            canThrowLimb = false;
        }            

    }    
    
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Vision Square"))
        {
            if (!onACollider)
            {
                canThrowLimb = true;
            }
        }
        if (collision.gameObject.CompareTag("Collisions"))
        {
            onACollider = true;
            canThrowLimb = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Vision Square"))
        {
            canThrowLimb = false;
        }
        if (collision.gameObject.CompareTag("Collisions"))
        {
            onACollider = false;
        }
    }
}
