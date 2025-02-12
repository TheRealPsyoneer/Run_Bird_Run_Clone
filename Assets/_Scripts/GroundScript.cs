using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        Bird bird = collision.gameObject.GetComponent<Bird>();
        if (bird != null)
        {
            bird.isTouchingGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Bird bird = collision.gameObject.GetComponent<Bird>();
        if (bird != null)
        {
            bird.isTouchingGround = false;
        }
    }
}
