using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class Bird : Unit
{
    [SerializeField] float maxSpeed;
    public float curSpeed { get; set; }
    [SerializeField] float maxRotateSpeed;
    public float curRotateSpeed { get; set; }
    [HideInInspector]
    public Vector2[] directions = new Vector2[2] { Vector2.left, Vector2.right };
    [HideInInspector]
    public Vector2[] normalDirections = new Vector2[2] { Vector2.left, Vector2.right };
    public Vector2[] contactRightDirections = new Vector2[2] { Vector2.down, Vector2.up };
    public Vector2[] contactLeftDirections = new Vector2[2] { Vector2.up, Vector2.down };

    public Vector2 direction { get; set; }
    public int directionIndex;
    public Vector2 primaryDirection { get; set; }
    public Rigidbody2D rb { get; set; }
    public Collider2D col { get; set; }
    public bool isTouchingGround;
    public bool isMoving { get; set; }
    public bool isMovingRight;

    public float rbGravityScale;

    public bool isClimbing;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        direction = Vector2.right;
        directionIndex = 1;
        curSpeed = 0;
        curRotateSpeed = 0;
        isClimbing = false;
    }

    public void MoveLeft()
    {
        curSpeed = maxSpeed;
        directionIndex = 0;
        isMoving = true;
        isMovingRight = false;

        Vector3 dir = transform.localScale;
        dir.x = -Mathf.Abs(dir.x);
        transform.localScale = dir;
    }

    public void MoveRight()
    {
        curSpeed = maxSpeed;
        directionIndex = 1;
        isMoving = true;
        isMovingRight = true;

        Vector3 dir = transform.localScale;
        dir.x = Mathf.Abs(dir.x);
        transform.localScale = dir;
    }

    public void StopMoving()
    {
        isMoving = false;
        curSpeed = 0;
        curRotateSpeed = 0;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            float hitDir = Vector2.SignedAngle(Vector2.up, (Vector2)(collision.gameObject.transform.position - (transform.position + (Vector3)col.offset)));
            //Debug.Log(hitDir);
            if (Mathf.Abs(hitDir) < 35f && isTouchingGround)
            {
                transform.DOScaleY(0, BoxBehaviour.FallTimePerCell);
            }

            else if (hitDir >= 35f && hitDir < 135f)
            {
                if (isTouchingGround)
                {
                    if (!isMovingRight)
                    {
                        isClimbing = true;
                        directions = contactLeftDirections;
                    }
                    else
                    {
                        isClimbing = false;
                        directions = normalDirections;
                    }
                }
                else
                {
                    isClimbing = true;
                    directions = contactLeftDirections;
                }


                if (isMoving)
                {
                    rb.gravityScale = 0;
                }
                else
                {
                    rb.gravityScale = rbGravityScale;
                }
            }
            else if (hitDir <= -35f && hitDir > -135f)
            {
                if (isTouchingGround)
                {
                    if (isMovingRight)
                    {
                        isClimbing = true;
                        directions = contactRightDirections;
                    }
                    else
                    {
                        isClimbing = false;
                        directions = normalDirections;
                    }
                }
                else
                {
                    isClimbing = true;
                    directions = contactRightDirections;
                }


                if (isMoving)
                {
                    rb.gravityScale = 0;
                }
                else
                {
                    rb.gravityScale = rbGravityScale;
                }
            }
            else if(Mathf.Abs(hitDir) >= 135f && Mathf.Abs(hitDir) < 155f)
            {
                isClimbing = true;
                directions = normalDirections;
            }

            else if (Mathf.Abs(hitDir) >= 155f)
            {
                isClimbing = false;
                isTouchingGround = true;
            }
        }
    }



    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box") && !isClimbing)
        {
            rb.gravityScale = rbGravityScale;
            directions = normalDirections;

            isTouchingGround = false;

            stateMachine.TransitionTo(stateStorage[State.Fall]);
        }
    }
}
