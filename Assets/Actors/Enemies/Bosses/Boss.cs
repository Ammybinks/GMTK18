using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Boss : Enemy {

    protected float stageTimer = 2;
    protected int stage;
    protected int cycles;

    // Use this for initialization
    void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (health <= 0)
        {
            room.killCount--;

            Destroy(gameObject);
        }

        if (active)
        {
            if (stageTimer > 0)
            {
                stageTimer -= Time.deltaTime;

                if (stageTimer < 0)
                {
                    stageTimer = 0;

                    if (cycles == 0)
                    {
                        int previousStage = stage;
                        while (previousStage == stage)
                        {
                            stage = Random.Range(1, 4);
                        }
                    }

                    if (rb2dInstance != null)
                    {
                        Destroy(rb2dInstance.gameObject);
                    }

                    //rb2d.velocity = Random.insideUnitCircle.normalized * 2;

                    SetStage();
                }
            }
            if (moveTimer > 0)
            {
                moveTimer -= Time.deltaTime;

                if (moveTimer < 0)
                {
                    moveTimer = 0;

                    fired = false;
                }
            }

            relativePosition.y = player.transform.position.y - transform.position.y;
            relativePosition.x = player.transform.position.x - transform.position.x;

            //Get the angle between the mouse and camera in radians.
            radians = (Mathf.Atan2(relativePosition.y, relativePosition.x));

            rb2d.rotation = (radians * Mathf.Rad2Deg) + 45;

            if (rb2d.velocity.magnitude <= 0.1 && moveTimer == 0)
            {
                Fire();
            }

            PostFire();
        }
    }

    protected virtual void SetStage()
    {

    }
}
