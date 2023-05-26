using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] [Tooltip("Amount of time it takes for player to move from one tile to the next")] private float moveSpeed;
<<<<<<< HEAD
<<<<<<< HEAD
=======
    [SerializeField] private LayerMask colliderLayer;
>>>>>>> origin/main
=======
    [SerializeField] private LayerMask colliderLayer;
>>>>>>> origin/main
    private Transform movePoint;


    // Start is called before the first frame update
    void Start()
    {
        movePoint = gameObject.transform.Find("Move Point");
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        if(transform.position == movePoint.position)
        {
            if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1)
            {
<<<<<<< HEAD
<<<<<<< HEAD
                movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
=======
=======
>>>>>>> origin/main
                if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0), 0.2f, colliderLayer))
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
                }
<<<<<<< HEAD
>>>>>>> origin/main
=======
>>>>>>> origin/main
            }

            if(Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1)
            {
<<<<<<< HEAD
<<<<<<< HEAD
                movePoint.position += new Vector3(0, Input.GetAxisRaw("Vertical"), 0);
=======
=======
>>>>>>> origin/main
                if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(0, Input.GetAxisRaw("Vertical"), 0), 0.2f, colliderLayer))
                {
                    movePoint.position += new Vector3(0, Input.GetAxisRaw("Vertical"), 0);   
                }
<<<<<<< HEAD
>>>>>>> origin/main
=======
>>>>>>> origin/main
            }
        }
    }
}
