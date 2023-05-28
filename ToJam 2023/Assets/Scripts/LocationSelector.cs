using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationSelector : MonoBehaviour
{
    public GameObject player;
    public Sprite canInteractIcon, cantInteractIcon;
    [HideInInspector]public LimbDetatchMenu limbManagerScript;
    [HideInInspector]public bool canInteract = false;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        limbManagerScript = GameObject.Find("Limb Manager").gameObject.GetComponent<LimbDetatchMenu>();

        spriteRenderer= gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (canInteract)
        {
            spriteRenderer.sprite = canInteractIcon;
        }
        else if (!canInteract)
        {
            spriteRenderer.sprite = cantInteractIcon;
        }


        //converts the location of the mouse to a location in the world
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //moves it's position to the position of the mouse, snapping to the middle of the tile
        transform.position = new Vector3(Mathf.Floor(mousePosition.x) + 0.5f, Mathf.Floor(mousePosition.y) + 0.5f, transform.position.z);

       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {     
        if (collision.gameObject.CompareTag("Interactable"))
        {
            canInteract = true;
        }
    }    
    
    
    private void OnTriggerStay2D(Collider2D collision)
    {
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
            canInteract = false;
    }
}
