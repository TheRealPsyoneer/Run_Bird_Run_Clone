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
    public Vector2 direction;
    public Rigidbody2D rb;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        direction = Vector2.right;
        curSpeed = 0;
        curRotateSpeed = 0;
    }

    public void MoveLeft()
    {
        curSpeed = maxSpeed;
        direction = Vector2.left;
        Vector3 dir = transform.localScale;
        dir.x = -Mathf.Abs(dir.x);
        transform.localScale = dir;
    }
    public void MoveRight()
    {
        curSpeed = maxSpeed;
        direction = Vector2.right;
        Vector3 dir = transform.localScale;
        dir.x = Mathf.Abs(dir.x);
        transform.localScale = dir;
    }

    public void StopMoving()
    {
        curSpeed = 0;
        curRotateSpeed = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            float hitDir = Vector2.SignedAngle(Vector2.up, (Vector2)(collision.transform.position - transform.position));
            if (Mathf.Abs(hitDir) <= 22.5f)
            {
                transform.DOScaleY(0, BoxBehaviour.FallTimePerCell);
            }
            else
            {
                curSpeed = maxSpeed * 10;
                curRotateSpeed = maxRotateSpeed;
            }
        }
    }
}
