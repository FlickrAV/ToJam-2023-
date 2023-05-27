using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LimbDetatchMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] limbs;    
    [SerializeField] private Transform limbSpawnPosition;
    [SerializeField] private PlayerMovement playerScript;



    public void Detatch(GameObject limbToDetatch)
    {
        if(playerScript.canMove && !Physics2D.OverlapCircle(limbSpawnPosition.position, 0.2f, playerScript.colliderLayer))
        Instantiate(limbs[int.Parse(limbToDetatch.name)], limbSpawnPosition.position, limbSpawnPosition.rotation);

    }
}
