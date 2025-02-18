using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudLoop : MonoBehaviour
{
    [SerializeField] float speed;
    float resetPos;

    float initPosX;

    private void Start()
    {
        initPosX = transform.position.x;
        resetPos = GetComponent<SpriteRenderer>().bounds.size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < initPosX - resetPos)
        {
            transform.position = new Vector2(initPosX, transform.position.y);
        }

        transform.Translate(Vector2.left * Time.deltaTime * speed);
    }
}
