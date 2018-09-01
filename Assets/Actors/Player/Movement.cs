using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    
    public int speed;
    public int maxSpeed;

    Rigidbody2D rb2d;

    bool upKeyDown;
    bool downKeyDown;
    bool leftKeyDown;
    bool rightKeyDown;

    Vector2 up;
    Vector2 down;
    Vector2 left;
    Vector2 right;
    // Use this for initialization
    void Start() {
        rb2d = GetComponent<Rigidbody2D>();

        up = Vector2.up;
        down = Vector2.down;
        left = Vector2.left;
        right = Vector2.right;
    }

    // Update is called once per frame
    void Update() {
        ResetKeys();

        if (Input.GetKey(KeyCode.W))
        {
            upKeyDown = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            downKeyDown = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            leftKeyDown = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rightKeyDown = true;
        }
    }

    private void FixedUpdate()
    {
        if (upKeyDown)
        {
            rb2d.AddForce(up * speed);
            if(rb2d.velocity.y > maxSpeed)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, maxSpeed);
            }
        }
        if (downKeyDown)
        {
            rb2d.AddForce(down * speed);
            if (rb2d.velocity.y < -maxSpeed)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, -maxSpeed);
            }
        }
        if (leftKeyDown)
        {
            rb2d.AddForce(left * speed);
            if (rb2d.velocity.x < -maxSpeed)
            {
                rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
            }
        }
        if (rightKeyDown)
        {
            rb2d.AddForce(right * speed);
            if (rb2d.velocity.x > maxSpeed)
            {
                rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
            }
        }
    }

    void ResetKeys()
    {
        upKeyDown = false;
        downKeyDown = false;
        leftKeyDown = false;
        rightKeyDown = false;
    }
}
