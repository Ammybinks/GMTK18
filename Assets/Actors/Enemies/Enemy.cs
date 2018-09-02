using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Enemy : MonoBehaviour {
    
    protected GameObject player;

    protected Rigidbody2D projectile;

    [SerializeField]
    protected Room room;
    
    public int health;
    public int tearStacks;

    [SerializeField]
    protected float toVel = 2.5f;
    [SerializeField]
    protected float maxVel = 15.0f;
    [SerializeField]
    protected float maxForce = 40.0f;
    [SerializeField]
    protected float gain = 5f;

    protected bool active;

    protected bool fired;
    protected bool offset;

    protected Rigidbody2D rb2d;
    protected Rigidbody2D rb2dInstance;

    protected Vector2 relativePosition;
    protected Vector2 targetPos;
    protected float radians;
    
    protected float moveTimer;

    private void Awake()
    {
        player = GameObject.Find("Player");

        Room.ActivateRoom += Activate;

        GetComponent<Collider2D>().enabled = false;
    }

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        projectile = Resources.Load<Rigidbody2D>("Ball");
    }
    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
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
    void FixedUpdate()
        {
            if (targetPos != null)
            {
                Move();
                // calc a target vel proportional to targetPosance (clamped to maxVel)
                Vector2 tgtVel = Vector3.ClampMagnitude(toVel * targetPos, maxVel);
                // calculate the velocity error
                Vector2 error = (tgtVel - rb2d.velocity);
                // calc a force proportional to the error (clamped to maxForce)
                Vector2 force = Vector3.ClampMagnitude(gain * error, maxForce);
                rb2d.AddForce(force);

                //radians = (Mathf.Atan2(targetPos.y, targetPos.x));

                ////rb2d.AddForce(new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * 14);

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
    
    protected virtual void Move()
    {

    }

    protected virtual void Fire()
    {

    }

    protected virtual void PostFire()
    {

    }

    //public virtual void Bump()
    //{
    //    if(moveTimer == 0)
    //    {
    //        rb2d.velocity = Random.insideUnitCircle.normalized * 2;
    //    }
    //}

    protected void Activate(string name)
    {
        if(room.gameObject.name == name)
        {
            active = true;

            GetComponent<Collider2D>().enabled = true;
        }
    }
}
