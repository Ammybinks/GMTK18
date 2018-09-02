using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public LayerMask mask;

    public bool dieOnCollide;

    public int damage;
    public float range;

    public float angle;

    public Vector2 startPos;

    bool collided;
    public bool tear;

    Vector2 difference;
	// Update is called once per frame
	void Update () {
        difference = startPos - new Vector2(transform.position.x, transform.position.y);

        if(Mathf.Sqrt((Mathf.Pow(difference.x, 2) + Mathf.Pow(difference.y, 2))) >= range && range > 0)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((mask.value & 1 << collision.gameObject.layer) != 0) && !collided)
        {
            if(dieOnCollide)
            {
                Destroy(gameObject);
            }
            else
            {
                collided = true;
            }

            Health health = collision.GetComponent<Health>();
            if (health)
            {
                health.health -= damage;
            }

            Enemy enemy = collision.GetComponent<Enemy>();
            if(enemy)
            {
                //enemy.Bump();
                if(tear)
                {
                    enemy.tearStacks += 1;

                    enemy.health -= enemy.tearStacks;
                }
                else
                {
                    enemy.health -= damage;
                }
            }
            //Collider2D collider2D = GetComponent<Collider2D>();
            //Physics2D.IgnoreCollision(collider2D, collision.collider, true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!collided)
        {
            if (((mask.value & 1 << collision.gameObject.layer) != 0))
            {
                if (dieOnCollide)
                {
                    Destroy(gameObject);
                }
                else
                {
                    collided = true;
                }

                Health health = collision.GetComponent<Health>();
                if (health)
                {
                    health.health -= damage;
                }

                Enemy enemy = collision.GetComponent<Enemy>();
                if (enemy)
                {
                    //enemy.Bump();

                    enemy.health -= damage;
                }
                //Collider2D collider2D = GetComponent<Collider2D>();
                //Physics2D.IgnoreCollision(collider2D, collision.collider, true);
            }
        }
    }
}
