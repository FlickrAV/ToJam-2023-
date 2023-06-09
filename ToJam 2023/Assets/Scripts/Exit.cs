using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    [SerializeField] private GameObject exitLock;
    private void OnTriggerEnter2D(Collider2D other) 
    {   
        if(other.name == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void DestroyLock()
    {
        Destroy(exitLock);
    }
}
