using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Runner : Enemy
{
    public float speed;

    public int damage;

    public LayerMask mask;

    void FixedUpdate()
    {
        if(active)
        {
            targetPos.y = player.transform.position.y - transform.position.y;
            targetPos.x = player.transform.position.x - transform.position.x;


            if (targetPos != null)
            {
                radians = (Mathf.Atan2(targetPos.y, targetPos.x));

                rb2d.AddForce(targetPos.normalized * speed);

                //rb2d.AddForce(new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * speed);

                //            // calc a target vel proportional to targetPosance (clamped to maxVel)
                //Vector2 tgtVel = Vector3.ClampMagnitude(toVel * targetPos, maxVel);
                //// calculate the velocity error
                //Vector2 error = (tgtVel - rb2d.velocity);
                //// calc a force proportional to the error (clamped to maxForce)
                //Vector2 force = Vector3.ClampMagnitude(gain * error, maxForce);
                //rb2d.AddForce(force);


                Debug.DrawRay(transform.position, targetPos, Color.red);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((mask.value & 1 << collision.gameObject.layer) != 0))
        {
            Destroy(gameObject);

            room.killCount--;

            Health health = collision.gameObject.GetComponent<Health>();
            if (health)
            {
                health.health -= damage;
            }
            //Collider2D collider2D = GetComponent<Collider2D>();
            //Physics2D.IgnoreCollision(collider2D, collision.collider, true);
        }
    }

    void OnCollisionEnter2D(Collider2D collision)
    {
    }

}
