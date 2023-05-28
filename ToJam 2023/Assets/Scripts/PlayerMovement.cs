using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField][Tooltip("Amount of time it takes for player to move from one tile to the next")] private float moveSpeed;
    [Tooltip("What layer to check for collisions")] public LayerMask colliderLayer;
    private Transform movePoint;
    private Transform directionIndicator;

    private LimbDetatchMenu limbMenuScript;

    public bool canMove = true;
    private bool isIdle = true;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        movePoint = gameObject.transform.Find("Move Point");
        directionIndicator = gameObject.transform.Find("Direction Indicator");
        movePoint.parent = null;
        directionIndicator.parent = null;

        limbMenuScript = GameObject.Find("Limb Manager").gameObject.GetComponent<LimbDetatchMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
            animator.SetFloat("Vertical", Input.GetAxis("Vertical"));
            /*
        if (transform.position == movePoint.position)
        {
            
        }
        else
        {
            animator.SetFloat("Horizontal", movePoint.position.x - transform.position.x);
            animator.SetFloat("Vertical", movePoint.position.y - transform.position.y);
        }*/
        animator.SetBool("isIdle", isIdle);

        //movement
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (transform.position != movePoint.position || Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            isIdle = false;
        }
        else
        {
            isIdle = true;
        }


        if (transform.position == movePoint.position)
        {
            //calculates the angle between where the move point is and where the direction idicator is
            Vector2 targetPosition = directionIndicator.position;
            Vector2 startPosition = movePoint.position;
            float angle = Mathf.Atan2(targetPosition.y - startPosition.y, targetPosition.x - startPosition.x);

            canMove = true;
            if (canMove)
            {
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1)
                {
                    //teleports the direction indicator to the position of the player and then puts it one tile infront of it in the direction that the player is facing
                    directionIndicator.position = transform.position;
                    directionIndicator.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);

                    //if there is nothing in the way, teleports the movePoint and the direction indicator one tile in the direction the player is moving
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0), 0.2f, colliderLayer))
                    {
                        movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
                        directionIndicator.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
                        canMove = false;
                    }
                }
                else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1)
                {
                    //teleports the direction indicator to the position of the player and then puts it one tile infront of it in the direction that the player is facing
                    directionIndicator.position = transform.position;
                    directionIndicator.position += new Vector3(0, Input.GetAxisRaw("Vertical"), 0);

                    //if there is nothing in the way, teleports the movePoint and the direction indicator one tile in the direction the player is moving
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0, Input.GetAxisRaw("Vertical"), 0), 0.2f, colliderLayer))
                    {
                        movePoint.position += new Vector3(0, Input.GetAxisRaw("Vertical"), 0);
                        directionIndicator.position += new Vector3(0, Input.GetAxisRaw("Vertical"), 0);
                        canMove = false;
                    }
                }

                if (Input.GetButtonDown("Jump"))
                {
                    if (limbMenuScript.hasLegs)
                    {
                        if (!Physics2D.OverlapCircle(new Vector3(Mathf.Floor(movePoint.position.x + Mathf.Sin(-angle + Mathf.Deg2Rad * 90) * 2) + 0.5f, Mathf.Floor(movePoint.position.y + Mathf.Cos(-angle + Mathf.Deg2Rad * 90) * 2) + 0.5f, 0), 0.2f, colliderLayer))
                        {
                            //moves the move point and the direction indicator two tiles in the direction that the player is facing
                            movePoint.position = new Vector2(Mathf.Floor(movePoint.position.x + Mathf.Sin(-angle + Mathf.Deg2Rad * 90) * 2) + 0.5f, Mathf.Floor(movePoint.position.y + Mathf.Cos(-angle + Mathf.Deg2Rad * 90) * 2) + 0.5f);
                            directionIndicator.position = new Vector2(Mathf.Floor(directionIndicator.position.x + Mathf.Sin(-angle + Mathf.Deg2Rad * 90) * 2) + 0.5f, Mathf.Floor(directionIndicator.position.y + Mathf.Cos(-angle + Mathf.Deg2Rad * 90) * 2) + 0.5f);
                        }
                    }
                }
            }

        }
        else
        {
            canMove = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Regenerator"))
        {
            limbMenuScript.Regenerate();
        }
    }
}
