using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    
    public float speed;
    float currentSpeed;
    public float maxSpeed;

    public float dashSpeed;
    public float dashLength;
    public float dashInvLength;

    Rigidbody2D rb2d;
    SpriteRenderer sprite;

    bool dashing;
    float dashTimer;
    float dashInvTimer;
    float timePercent;

    Color colour;

    bool upKeyDown;
    bool downKeyDown;
    bool leftKeyDown;
    bool rightKeyDown;
    bool dashKeyDown;
    bool dashKeyUp;

    Vector2 up;
    Vector2 down;
    Vector2 left;
    Vector2 right;

    Attack attack;
    // Use this for initialization
    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        attack = GetComponent<Attack>();

        up = Vector2.up;
        down = Vector2.down;
        left = Vector2.left;
        right = Vector2.right;
    }

    // Update is called once per frame
    void Update() {
        if(dashTimer > 0)
        {
            dashTimer -= Time.deltaTime;

            float timePercent = (dashTimer / dashLength);
            sprite.color = new Color(colour.r * timePercent, colour.g * timePercent, colour.b * timePercent, 100);

            if (dashTimer <= 0)
            {
                dashTimer = 0;

                dashing = false;
            }
        }
        if(dashInvTimer > 0)
        {
            dashInvTimer -= Time.deltaTime;

            if(dashInvTimer <= 0)
            {
                dashInvTimer = 0;

                gameObject.layer = 10;
            }
        }

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
        if (Input.GetKey(KeyCode.Space))
        {
            dashKeyDown = true;
        }
    }

    private void FixedUpdate()
    {
        if (dashKeyDown && dashKeyUp)
        {

            if (!dashing)
            {
                dashKeyUp = false;

                Vector2 tempVelocity = new Vector2();

                if (upKeyDown)
                {
                    tempVelocity += (up * dashSpeed);
                }
                if (downKeyDown)
                {
                    tempVelocity += (down * dashSpeed);
                }
                if (leftKeyDown)
                {
                    tempVelocity += (left * dashSpeed);
                }
                if (rightKeyDown)
                {
                    tempVelocity += (right * dashSpeed);
                }

                rb2d.velocity = tempVelocity;

                if (tempVelocity != Vector2.zero)
                {
                    dashing = true;
                    dashTimer = dashLength;

                    gameObject.layer = 12;
                    dashInvTimer = dashInvLength;

                    colour = new Color(0.88F, 1, 1, 1);
                    sprite.color = colour;
                }
            }
            else
            {
                sprite.color = new Color(1, 0, 0, 1);
            }
        }
        if (!dashing)
        {
            if(attack.firing)
            {
                currentSpeed = speed / 2;
            }
            else
            {
                currentSpeed = speed;
            }

            if (upKeyDown)
            {
                rb2d.AddForce(up * currentSpeed);
                if (rb2d.velocity.y > maxSpeed)
                {
                    rb2d.velocity = new Vector2(rb2d.velocity.x, maxSpeed);
                }
            }
            if (downKeyDown)
            {
                rb2d.AddForce(down * currentSpeed);
                if (rb2d.velocity.y < -maxSpeed)
                {
                    rb2d.velocity = new Vector2(rb2d.velocity.x, -maxSpeed);
                }
            }
            if (leftKeyDown)
            {
                rb2d.AddForce(left * currentSpeed);
                if (rb2d.velocity.x < -maxSpeed)
                {
                    rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
                }
            }
            if (rightKeyDown)
            {
                rb2d.AddForce(right * currentSpeed);
                if (rb2d.velocity.x > maxSpeed)
                {
                    rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
                }
            }
        }
    }

    void ResetKeys()
    {
        upKeyDown = false;
        downKeyDown = false;
        leftKeyDown = false;
        rightKeyDown = false;

        if(!Input.GetKey(KeyCode.Space))
        {
            dashKeyDown = false;
            dashKeyUp = true;
        }
    }
}
