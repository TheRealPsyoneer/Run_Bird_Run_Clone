using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRightCol : MonoBehaviour
{
    Vector2[] normalDirections = new Vector2[2] { Vector2.left, Vector2.right };
    Vector2[] contactDirections = new Vector2[2] { Vector2.up, Vector2.down };
    private void OnTriggerStay2D(Collider2D collision)
    {
        Bird bird = collision.gameObject.GetComponent<Bird>();
        if (bird != null)
        {
            if (bird.isTouchingGround)
            {
                if (!bird.isMovingRight)
                {
                    bird.directions = contactDirections;
                }
                else
                {
                    bird.directions = normalDirections;
                }
            }
            else
            {
                bird.directions = contactDirections;
            }

            if (bird.isMoving)
            {
                bird.rb.gravityScale = 0;
            }
            else
            {
                bird.rb.gravityScale = 5;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Bird bird = collision.gameObject.GetComponent<Bird>();
        if (bird != null)
        {
            bird.directions = normalDirections;
            bird.rb.gravityScale = 5;
        }
    }
}
