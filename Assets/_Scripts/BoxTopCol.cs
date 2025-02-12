using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTopCol : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        Bird bird = collision.gameObject.GetComponent<Bird>();
        if (bird != null)
        {
            bird.isTouchingGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Bird bird = collision.gameObject.GetComponent<Bird>();
        if (bird != null)
        {
            bird.isTouchingGround = false;
        }
    }
}
