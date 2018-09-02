using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Turret : Enemy
{
    new private int offset;
    
    // Update is called once per frame
    void Update()
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

                    fired = false;
                }
            }

            if (moveTimer == 0)
            {
                Fire();
            }
        }
    }
    void FixedUpdate()
    {
    }

    new void Fire()
    {
        if (!fired)
        {
            int shots = 9;
            float degrees = 0 + offset;
            
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
            offset += 2;

            if (moveTimer == 0)
            {
                moveTimer = 0.5F;
            }
        }
    }
}
