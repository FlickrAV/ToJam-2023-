using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllignmentTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPos = transform.position;

        transform.position= new Vector3(Mathf.Floor(currentPos.x) + 0.5f, Mathf.Floor(currentPos.y) + 0.5f, Mathf.Floor(currentPos.z));
    }
}
