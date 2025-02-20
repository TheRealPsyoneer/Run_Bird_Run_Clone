using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class Bird : Unit
{
    [SerializeField] float maxSpeed;
    public float curSpeed;
    [SerializeField] float rotateTime;
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
    public Animator animator { get; set; }
    public bool isTouchingGround;
    public bool isMoving { get; set; }
    public bool isMovingRight;

    public float rbGravityScale;

    

    public Transform[] children { get; set; }

    public BirdExpression expression { get; set; }
    public Transform sweat;
    public Transform sweat2;

    public EventSO dieEvent;
    public EventSO gameStartEvent;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        expression = GetComponent<BirdExpression>();
        children = GetComponentsInChildren<Transform>();
        animator = GetComponentInChildren<Animator>();
        direction = Vector2.right;
        directionIndex = 1;
        curSpeed = 0;
    }

    public void MoveLeft()
    {
        if (GameManager.Instance.gameState == GameState.MainMenu)
        {
            gameStartEvent.Broadcast();
        }

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
        if (GameManager.Instance.gameState == GameState.MainMenu)
        {
            gameStartEvent.Broadcast();
        }

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
        rb.gravityScale = rbGravityScale;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        BoxBehaviour theBox = collision.gameObject.GetComponent<BoxBehaviour>();
        float hitDir = Vector2.SignedAngle(Vector2.up, (Vector2)(collision.gameObject.transform.position - (transform.position + (Vector3)col.offset)));
        
        if (collision.gameObject.CompareTag("Box") && Mathf.Abs(hitDir) < 40f && isTouchingGround)
        {
            if (stateMachine.currentState.state != State.Die)
            {
                stateMachine.TransitionTo(stateStorage[State.Die]);
            }
        }
        if (collision.gameObject.CompareTag("Box") && theBox != null && theBox.isClimbable)
        {
            
            //Debug.Log(hitDir);
            

            if (hitDir >= 40f && hitDir < 147f && stateMachine.currentState.state == State.Move)
            {
                if (isTouchingGround)
                {
                    
                    if (!isMovingRight)
                    {
                        //isClimbing = true;
                        children[1].transform.DOKill();
                        children[1].transform.DOLocalRotate(new Vector3(0, 0, 90), rotateTime);
                        directions = contactLeftDirections;
                    }
                    else
                    {
                        //isClimbing = false;
                        directions = normalDirections;
                    }
                }
                else
                {
                    //isClimbing = true;
                    children[1].transform.DOKill();
                    children[1].transform.DOLocalRotate(new Vector3(0, 0, 90), rotateTime);
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
            else if (hitDir <= -40f && hitDir > -147f && stateMachine.currentState.state == State.Move)
            {
                if (isTouchingGround)
                {
                    if (isMovingRight)
                    {
                        //isClimbing = true;
                        children[1].transform.DOKill();
                        children[1].transform.DOLocalRotate(new Vector3(0, 0, 90), rotateTime);
                        directions = contactRightDirections;
                    }
                    else
                    {
                        //isClimbing = false;
                        directions = normalDirections;
                    }
                }
                else
                {
                    //isClimbing = true;
                    children[1].transform.DOKill();
                    children[1].transform.DOLocalRotate(new Vector3(0, 0, 90), rotateTime);
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
            //else if(Mathf.Abs(hitDir) >= 135f && Mathf.Abs(hitDir) < 155f)
            //{
            //    //isClimbing = true;
            //    directions = normalDirections;
            //}
            //else if (Mathf.Abs(hitDir) >= 145f && Mathf.Abs(hitDir) < 155f)
            //{
            //    rb.velocity = directions[directionIndex]*2;
            //}

            else if (Mathf.Abs(hitDir) >= 145f)
            {
                //isClimbing = false;
                //directions = normalDirections;
                isTouchingGround = true;
                rb.gravityScale = rbGravityScale;
            }
        }
    }



    private void OnCollisionExit2D(Collision2D collision)
    {
        BoxBehaviour theBox = collision.gameObject.GetComponent<BoxBehaviour>();
        if (collision.gameObject.CompareTag("Box") && theBox != null && theBox.isClimbable)
        {
            
            if (!col.IsTouchingLayers(LayerMask.GetMask("Box")))
            {
                rb.gravityScale = rbGravityScale;

                children[1].transform.DOLocalRotate(Vector3.zero, rotateTime * 0.5f).SetEase(Ease.Linear);

                directions = normalDirections;
                
                rb.AddForce(directions[directionIndex]*3, ForceMode2D.Impulse);

                isTouchingGround = false;

                stateMachine.TransitionTo(stateStorage[State.Idle]);
            }

            else
            {
                float hitDir = Vector2.SignedAngle(Vector2.up, (Vector2)(collision.gameObject.transform.position - (transform.position + (Vector3)col.offset)));
                if (Mathf.Abs(hitDir) >= 125f)
                {
                    //isClimbing = false;
                    //directions = normalDirections;
                    isTouchingGround = false;
                }
            }

            //if (!isTouchingGround && !col.IsTouchingLayers(LayerMask.GetMask("Box")))
            //{
            //    rb.gravityScale = rbGravityScale;


            //    directions = normalDirections;
                
            //    rb.AddForce(directions[directionIndex]*2, ForceMode2D.Impulse);

            //    //isTouchingGround = false;

            //    stateMachine.TransitionTo(stateStorage[State.Idle]);
            //}
            //else if (isTouchingGround && col.IsTouchingLayers(LayerMask.GetMask("Box")))
            //{
            //    isTouchingGround = false;
            //}
        }
    }
}
