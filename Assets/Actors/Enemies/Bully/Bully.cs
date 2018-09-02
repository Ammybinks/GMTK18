using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Bully : Enemy {

    bool movingForwards;
    int direction;

    Vector2 targetPoint;

    RaycastHit2D[] hits;

    float moveTimer = 0.1F;

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
            if (moveTimer > 0)
            {
                moveTimer -= Time.deltaTime;

                if (moveTimer < 0)
                {
                    moveTimer = 0;


                    //    if (movingForwards)
                    //    {
                    //        targetPos = relativePosition - (relativePosition.normalized * 2);
                    //    }
                    //    else
                    //    {
                    //        direction = Random.Range(0, 4);

                    //        if (direction == 0)
                    //        {
                    //            targetPoint = new Vector2(transform.position.x + transform.up.x * 5, transform.position.y + transform.up.y * 5);
                    //        }
                    //        if (direction == 1)
                    //        {
                    //            targetPoint = new Vector2(transform.position.x + transform.right.x * 5, transform.position.y + transform.right.y * 5);
                    //        }
                    //        if (direction == 2)
                    //        {
                    //            targetPoint = new Vector2(transform.position.x + -transform.right.x * 5, transform.position.y + -transform.right.y * 5);
                    //        }
                    //        if (direction == 3)
                    //        {
                    //            targetPoint = new Vector2(transform.position.x + -transform.up.x * 5, transform.position.y + -transform.up.y * 5);
                    //        }

                    //        hits = Physics2D.RaycastAll(transform.position, targetPoint);
                    //        if (hits != null)
                    //        {
                    //            for(int i = 0; i < hits.Length; i++)
                    //            {
                    //                if(hits[i].collider.tag == "Environment")
                    //                {
                    //                    Vector2 temp = new Vector2(transform.position.x - hits[i].transform.position.x, transform.position.y - hits[i].transform.position.y);

                    //                    targetPoint = new Vector2(hits[i].transform.position.x + temp.normalized.x * 1, hits[i].transform.position.y + temp.normalized.y * 1);

                    //                    SpriteRenderer.Instantiate(projectile, new Vector3(targetPoint.x, targetPoint.y), new Quaternion());
                    //                }
                    //            }
                    //        }

                    //        targetPos.y = targetPoint.y - transform.position.y;
                    //        targetPos.x = targetPoint.x - transform.position.x;
                    //    }

                    //    movingForwards = !movingForwards;

                    //    fired = false;
                }
            }

            relativePosition.y = player.transform.position.y - transform.position.y;
            relativePosition.x = player.transform.position.x - transform.position.x;

            targetPos = relativePosition - (relativePosition.normalized * 2);

            //Get the angle between the mouse and camera in radians.
            radians = (Mathf.Atan2(relativePosition.y, relativePosition.x));

            rb2d.rotation = (radians * Mathf.Rad2Deg) + 45;

            if (moveTimer == 0)
            {
                Fire();
            }

            PostFire();
        }
    }

    new private void Move()
    {
        targetPos = relativePosition - (relativePosition.normalized * 2);

        //if (movingForwards)
        //{
        //    targetPos = relativePosition - (relativePosition.normalized * 2);
        //}
        //else
        //{
        //    targetPos.y = targetPoint.y - transform.position.y;
        //    targetPos.x = targetPoint.x - transform.position.x;
        //}
    }

    new private void Fire()
    {
        int variance = 5;
        float speedVariance = 1;
        int projectileNumber = 10;

        float degrees = radians * Mathf.Rad2Deg;
        float angle;

        for (int i = 0; i < projectileNumber; i++)
        {
            rb2dInstance = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));

            //Rigidbody2D rb2dInstance = instance.GetComponent<Rigidbody2D>();
            Projectile projectileInstance = rb2dInstance.GetComponent<Projectile>();

            projectileInstance.damage = 1;

            angle = (degrees + Random.Range(-variance, variance)) * Mathf.Deg2Rad;

            rb2dInstance.velocity = (new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * (20 + Random.Range(-speedVariance, speedVariance)));
        }

        fired = true;

        if (moveTimer == 0)
        {
            moveTimer = 1F;
        }
    }
}
