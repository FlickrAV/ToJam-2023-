using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField][Tooltip("Amount of time it takes for player to move from one tile to the next")] private float moveSpeed;
    [Tooltip("What layer to check for collisions")] public LayerMask colliderLayer;
    private Transform movePoint;
    private Transform limbSpawnPoint;

    public bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        movePoint = gameObject.transform.Find("Move Point");
        limbSpawnPoint = gameObject.transform.Find("Limb Spawn Point");
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        //movement
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        if (transform.position == movePoint.position)
        {
            canMove = true;
            if (canMove)
            {
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1)
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0), 0.2f, colliderLayer))
                    {
                        movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
                        limbSpawnPoint.position = movePoint.position;
                        canMove = false;
                    }
                }
            }

            if (canMove)
            {
                if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1)
                {
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0, Input.GetAxisRaw("Vertical"), 0), 0.2f, colliderLayer))
                    {
                        movePoint.position += new Vector3(0, Input.GetAxisRaw("Vertical"), 0);
                        limbSpawnPoint.position = movePoint.position;
                        canMove = false;
                    }
                }
            }
        }
        else
        {
            canMove = false;
        }
    }
}
