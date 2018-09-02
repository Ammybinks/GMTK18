using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Boss1 : Boss {
    
    protected Vector2 oldPlayerPos;
    protected float oldRadians;

    protected float diff;

    protected float turnSpeed;
    protected float turnSpeedRamp;
    protected float maxTurnSpeed = 25;

    protected Vector3 furthestCorner;
    protected Vector3[] corners;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        corners = new Vector3[4];

        corners[0] = new Vector3(room.transform.position.x + 12, room.transform.position.y + -12, 80);
        corners[1] = new Vector3(room.transform.position.x + -12, room.transform.position.y + 12, 260);
        corners[2] = new Vector3(room.transform.position.x + -12, room.transform.position.y + -12, 350);
        corners[3] = new Vector3(room.transform.position.x + 12, room.transform.position.y + 12, 170);
    }

    protected override void SetStage()
    {
        if (stage == 1)
        {
            //stageTimer = (Random.Range(1, 10) * 2) + 1;

            targetPos.y = player.transform.position.y - transform.position.y;
            targetPos.x = player.transform.position.x - transform.position.x;

            targetPos -= targetPos.normalized * 5;

            targetPos.y += room.transform.position.y - transform.position.y;
            targetPos.x += room.transform.position.x - transform.position.x;

            oldPlayerPos = player.transform.position;

            if (cycles == 0)
            {
                cycles = 3;
            }

            moveTimer = 2;
        }
        if (stage == 2)
        {
            //stageTimer = 5;

            targetPos = room.transform.position;

            if (cycles == 0)
            {
                cycles = 10;
            }

            moveTimer = 0.5F;
        }
        if (stage == 3)
        {
            furthestCorner = corners[0];
            //stageTimer = 8;
            float dist = Vector2.Distance(player.transform.position, corners[0]);
            for (var i = 0; i < 4; i++)
            {
                var tempDist = Vector2.Distance(player.transform.position, corners[i]);
                if (tempDist > dist)
                {
                    furthestCorner = corners[i];
                    dist = tempDist;
                }
            }

            targetPos.y = furthestCorner.y - transform.localPosition.y;
            targetPos.x = furthestCorner.x - transform.localPosition.x;

            //Get the angle between the mouse and camera in radians.
            //radians = (Mathf.Atan2(targetPos.y, targetPos.x));

            //rb2d.AddForce(new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * 14);

            //cornerIndex++;

            //if (cornerIndex > 3)
            //{
            //    cornerIndex = 0;
            //}

            if (cycles == 0)
            {
                cycles = 1;
            }

            moveTimer = 2;
        }

        cycles--;
    }

    protected override void Move()
    {

        if (stage == 1)
        {
            targetPos.y = oldPlayerPos.y - transform.position.y;
            targetPos.x = oldPlayerPos.x - transform.position.x;

            targetPos -= targetPos.normalized * 5;

            targetPos.y += room.transform.position.y - transform.position.y;
            targetPos.x += room.transform.position.x - transform.position.x;
        }

        if (stage == 2)
        {
            targetPos = room.transform.position - transform.position;
        }

        if (stage == 3)
        {
            targetPos.y = furthestCorner.y - transform.localPosition.y;
            targetPos.x = furthestCorner.x - transform.localPosition.x;
        }

    }

    protected override void Fire()
    {
        ////IF PATTERN 1
        if (stage == 1)
        {
            if (!fired)
            {
                projectile = Resources.Load<Rigidbody2D>("Ball");

                int variance = 30;
                float speedVariance = 1;
                int projectileNumber = 20;

                float degrees = radians * Mathf.Rad2Deg;
                float angle;

                for (int i = 0; i < projectileNumber; i++)
                {
                    rb2dInstance = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));

                    //Rigidbody2D rb2dInstance = instance.GetComponent<Rigidbody2D>();
                    Projectile projectileInstance = rb2dInstance.GetComponent<Projectile>();

                    projectileInstance.damage = 5;

                    angle = (degrees + Random.Range(-variance, variance)) * Mathf.Deg2Rad;

                    rb2dInstance.velocity = (new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * (10 + Random.Range(-speedVariance, speedVariance)));
                }

                fired = true;

                if (stageTimer == 0)
                {
                    stageTimer = 0.1F;
                }
            }
        }
        if (stage == 2)
        {
            if (!fired)
            {
                projectile = Resources.Load<Rigidbody2D>("Ball");

                int shots = 36;
                float degrees = (radians * Mathf.Rad2Deg) + 30;

                if (offset)
                {
                    degrees += 5;
                }

                float increment = 360 / shots;

                for (int i = 1; i <= shots; i++)
                {
                    rb2dInstance = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));

                    //Rigidbody2D rb2dInstance = instance.GetComponent<Rigidbody2D>();
                    Projectile projectileInstance = rb2dInstance.GetComponent<Projectile>();

                    radians = (degrees + (increment * i)) * Mathf.Deg2Rad;

                    rb2dInstance.velocity = (new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * 2);

                    projectileInstance.damage = 5;
                }

                fired = true;
                offset = !offset;

                if (stageTimer == 0)
                {
                    stageTimer = 0.01F;
                }
            }
        }
        ////If PATTERN 3
        if (stage == 3)
        {
            projectile = Resources.Load<Rigidbody2D>("Laser");

            if (!fired)
            {
                turnSpeed = 0;
                turnSpeedRamp = 0.02F;

                //Create an instance of the projectile.
                rb2dInstance = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;

                Projectile projectileInstance = rb2dInstance.GetComponent<Projectile>();

                projectileInstance.damage = 50;

                //Set the rotation of the projectile
                Vector3 temp = rb2dInstance.gameObject.transform.rotation.eulerAngles;

                temp.z = furthestCorner.z;

                rb2dInstance.gameObject.transform.rotation = Quaternion.Euler(temp);
                rb2dInstance.gameObject.transform.position = transform.position;

                oldRadians = furthestCorner.z * Mathf.Deg2Rad;

                diff = ((radians * Mathf.Rad2Deg)) - ((oldRadians * Mathf.Rad2Deg));

                //if (diff > -140)
                //{
                //    turnSpeed *= -1;
                //    turnSpeedRamp *= -1;
                //}

                fired = true;

                if (stageTimer == 0)
                {
                    stageTimer = 1.8F;
                }
            }
        }
    }

    protected override void PostFire()
    {
        if (fired)
        {
            if (stage == 3 && rb2dInstance)
            {
                oldRadians += (turnSpeed * Mathf.Deg2Rad);

                //Set the rotation of the projectile
                Vector3 temp = rb2dInstance.gameObject.transform.rotation.eulerAngles;

                temp.z = oldRadians * Mathf.Rad2Deg;

                rb2dInstance.gameObject.transform.rotation = Quaternion.Euler(temp);
                rb2dInstance.gameObject.transform.position = transform.position;

                turnSpeed += turnSpeedRamp;
                if (turnSpeed > maxTurnSpeed)
                {
                    turnSpeed = maxTurnSpeed;
                }
            }
        }
    }
}
