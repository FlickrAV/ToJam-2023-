using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    [SerializeField] private GameObject exitLock;

    public bool isOpen = false;

    private SpriteRenderer exitSprite;
    public Sprite exitOpen;
    public Sprite exitClose;

    public SpriteRenderer exitSpriteDark;

    private void Start()
    {
        exitSprite = gameObject.GetComponent<SpriteRenderer>();
        exitSprite.sprite = exitClose;
        if (isOpen)
        {
            exitSprite.sprite = exitOpen;
            exitLock.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {   
        if(other.name == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void DestroyLock()
    {
        exitSprite.sprite = exitOpen;
        exitSpriteDark.sprite = exitOpen;
        exitLock.SetActive(false);
    }

    public void RespawnLock()
    {
        exitSprite.sprite = exitClose;
        exitSpriteDark.sprite = exitClose;
        exitLock.SetActive(true);
    }
}
