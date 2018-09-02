using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    Rigidbody2D projectile;

    public int damage = 5;
    public int baseDamage;
    public float fireRate = 1;
    public int lines = 1;
    public int spread = 5;
    public int projectileSpeed = 5;
    public float range = 2;

    public bool haste;
    public bool puncture;
    public bool split;
    public bool blast;
    public bool restraint;
    public bool tear;
    public bool spike;
    public bool shock;
    public bool whisper;
    public bool burst;
    public bool snap;
    public bool mark;

    public string effects;

    public bool firing;

    float defaultFireRate;

    float variance = 15;
    float speedVariance = 1;

    bool charging;

    int spreadTier;

    public float timer;
    float expireTimer;

    Vector3 mousePosition;
    float mousePositionY;
    float mousePositionX;
    float radians;

    Rigidbody2D rb2d;
    List<Rigidbody2D> rb2dInstances;

    ItemDialog item;

    SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        rb2dInstances = new List<Rigidbody2D>();

        projectile = Resources.Load<Rigidbody2D>("PlayerBall");
        item = GameObject.Find("Canvas").GetComponent<ItemDialog>();

        baseDamage = damage;
        defaultFireRate = fireRate;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Save the current position of the mouse in relation to the main camera.
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mousePositionY = mousePosition.y - transform.position.y;
        mousePositionX = mousePosition.x - transform.position.x;

        //Get the angle between the mouse and camera in radians.
        radians = (Mathf.Atan2(mousePositionY, mousePositionX));

        rb2d.rotation = (radians * Mathf.Rad2Deg) + 45;

        if (charging)
        {
            timer += Time.deltaTime;

            if (timer > fireRate)
            {
                timer = fireRate;

                sprite.color = Color.yellow;
            }
            else
            {
                float timePercent = (timer / fireRate);
                sprite.color = new Color(Color.white.r * timePercent, Color.white.g * timePercent, Color.white.b * timePercent, 100);
            }
        }
        else
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;

                if (timer < 0)
                {
                    timer = 0;
                }
            }
            if (expireTimer > 0)
            {
                expireTimer -= Time.deltaTime;

                if (expireTimer < 0)
                {
                    expireTimer = 0;
                }
            }
        }

        if(blast)
        {
            if(timer != 0)
            {
                firing = true;
            }
            else
            {
                firing = false;
            }
        }

        if (shock)
        {
            foreach(Rigidbody2D projectile in rb2dInstances)
            {
                if(lines > 1)
                {
                    Vector3 temp = projectile.gameObject.transform.rotation.eulerAngles;

                    temp.z = (projectile.GetComponent<Projectile>().angle * Mathf.Rad2Deg);

                    projectile.transform.position = transform.position;
                    projectile.transform.rotation = Quaternion.Euler(temp);
                }
                else
                {
                    Vector3 temp = projectile.gameObject.transform.rotation.eulerAngles;

                    temp.z = (radians * Mathf.Rad2Deg) + projectile.GetComponent<Projectile>().angle;

                    projectile.transform.position = transform.position;
                    projectile.transform.rotation = Quaternion.Euler(temp);
                }

            }
        }

        if (Input.GetMouseButton(0))
        {
            if (puncture)
            {
                if(!charging)
                {
                    if(timer == 0)
                    {
                        charging = true;
                    }
                }
            }
            else
            {
                if (timer == 0)
                {
                    Fire();
                }
            }
        }
        else
        {
            if (puncture)
            {
                if (timer == fireRate)
                {
                    Fire();

                    sprite.color = Color.black;
                }

                timer = 0;

                charging = false;
            }

            if(shock)
            {
                if(expireTimer == 0)
                {
                    foreach (Rigidbody2D projectile in rb2dInstances)
                    {
                        Destroy(projectile.gameObject);
                    }
                    rb2dInstances.Clear();

                    expireTimer = -1;
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            if (item.currentItem != null)
            {
                float oldSpread = spread;

                if(item.currentItem.itemName == "Haste")
                {
                    haste = true;

                    fireRate -= (float)0.75;
                    range -= (float)7.5;
                }
                if (item.currentItem.itemName == "Puncture")
                {
                    puncture = true;

                    range = 0;
                    baseDamage += 5;
                    projectileSpeed += 15;
                    fireRate += 2;

                    timer = -1;

                    effects += "* +Charge Attack\n";
                }
                if (item.currentItem.itemName == "Split")
                {
                    split = true;

                    baseDamage -= 2;
                    lines += 2;
                    if(spreadTier < 1)
                    {
                        spread = 5;
                        spreadTier = 1;
                    }
                    CheckSpread();
                    effects += "* +2 Projectiles\n";
                }
                if (item.currentItem.itemName == "Blast")
                {
                    blast = true;

                    baseDamage += 10;

                    effects += "* -Movement Slow While Firing";
                }
                if (item.currentItem.itemName == "Restraint")
                {
                    restraint = true;

                    fireRate += 2;

                    spread -= 10;
                    if (spread < 1)
                    {
                        spread = 1;
                    }

                    effects += "* +Reduced Spread\n";
                }
                if (item.currentItem.itemName == "Tear")
                {
                    tear = true;

                    baseDamage = 0;

                    effects += "* +Stacking Damage";
                }
                if (item.currentItem.itemName == "Spike")
                {
                    spike = true;

                    baseDamage -= 5;
                    range -= 5;
                    if(spreadTier < 3)
                    {
                        spread = 30;
                        spreadTier = 3;
                    }
                    CheckSpread();

                    effects += "* +360° Spread";
                }
                if (item.currentItem.itemName == "Shock")
                {
                    shock = true;

                    fireRate -= 3;
                    Health health = GetComponent<Health>();
                    health.health -= 50;
                    if(health.health < 1)
                    {
                        health.health = 1;
                    }

                    projectile = Resources.Load<Rigidbody2D>("PlayerLaser");

                    effects += "* +Line Attack";
                }
                if (item.currentItem.itemName == "Whisper")
                {
                    whisper = true;

                    effects += "* +Converting Attack Speed to Damage";
                }
                if (item.currentItem.itemName == "Burst")
                {
                    burst = true;

                    lines += 6;
                    fireRate += 1;
                    if(spreadTier < 2)
                    {
                        spread = 15;
                        spreadTier = 2;
                    }
                    CheckSpread();

                    effects += "* +Shot Variance";
                }
                if (item.currentItem.itemName == "Snap")
                {
                    snap = true;
                    
                    effects += "* +Damage Variance";
                }
                if (item.currentItem.itemName == "Mark")
                {
                    mark = true;

                    range += 200;
                    baseDamage += 5000;
                    fireRate -= 5;
                    GetComponent<Health>().health -= 99;
                }

                if (baseDamage < 1)
                {
                    baseDamage = 1;
                }

                if(fireRate < 0.05)
                {
                    fireRate = 0.05F;
                }


                if(whisper)
                {
                    float lowestValue = 10;
                    float extraDamage = (lowestValue - fireRate) * 5;

                    fireRate = lowestValue;
                    damage = baseDamage + (int)Mathf.Round(extraDamage);
                }
                else
                {
                    damage = baseDamage;
                }

                Destroy(item.currentItem.gameObject);
            }
        }
    }
    

    void Fire()
    {
        if (lines > 1 || spike)
        {
            float degrees;
            float increment;

            if(spike)
            {
                degrees = 0;

                increment = spread;
                lines = 360 / (int)increment;
            }
            else
            {
                degrees = radians * Mathf.Rad2Deg;

                degrees -= spread;

                increment = (spread * 2) / (lines - 1);
            }

            if (shock)
            {
                foreach (Rigidbody2D projectile in rb2dInstances)
                {
                    Destroy(projectile.gameObject);
                }
                rb2dInstances.Clear();
            }

            for (int i = 0; i < lines; i++)
            {
                Rigidbody2D rb2dInstance = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));

                //Rigidbody2D rb2dInstance = instance.GetComponent<Rigidbody2D>();
                Projectile projectileInstance = rb2dInstance.GetComponent<Projectile>();

                if(tear)
                {
                    projectileInstance.tear = true;
                }


                if (burst)
                {
                    radians = (degrees + Random.Range(-variance, variance) + (increment * i)) * Mathf.Deg2Rad;

                    rb2dInstance.velocity = (new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * (projectileSpeed + Random.Range(-speedVariance, speedVariance)));
                }
                else
                {
                    radians = (degrees + (increment * i)) * Mathf.Deg2Rad;

                    rb2dInstance.velocity = (new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * projectileSpeed);
                }

                if (shock)
                {
                    Vector3 temp = rb2dInstance.gameObject.transform.rotation.eulerAngles;

                    temp.z = (radians * Mathf.Rad2Deg);

                    rb2dInstance.transform.rotation = Quaternion.Euler(temp);

                    rb2dInstance.transform.localScale = new Vector2(range * 100, rb2dInstance.transform.localScale.y);

                    //projectileInstance.expireTimer = 0.5F;
                    projectileInstance.angle = radians;

                    rb2dInstances.Add(rb2dInstance);


                    //Vector3 temp = rb2dInstance.gameObject.transform.rotation.eulerAngles;

                    //temp.z = temp.z - 45;

                    //rb2dInstance.gameObject.transform.rotation = Quaternion.Euler(temp);
                    //rb2dInstance.gameObject.transform.position = transform.position;
                }
                else
                {
                    if (burst)
                    {
                        rb2dInstance.velocity = (new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * (projectileSpeed + Random.Range(-speedVariance, speedVariance)));
                    }
                    else
                    {
                        rb2dInstance.velocity = (new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * projectileSpeed);
                    }
                }

                projectileInstance.startPos = rb2dInstance.transform.position;
                projectileInstance.damage = damage;
                projectileInstance.range = range;
            }
        }
        else
        {
            if(shock)
            {
                foreach (Rigidbody2D projectile in rb2dInstances)
                {
                    Destroy(projectile.gameObject);
                }
                rb2dInstances.Clear();
            }

            Rigidbody2D rb2dInstance = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));

            //Rigidbody2D rb2dInstance = instance.GetComponent<Rigidbody2D>();
            Projectile projectileInstance = rb2dInstance.GetComponent<Projectile>();

            if (tear)
            {
                projectileInstance.tear = true;
            }

            if(shock)
            {
                Vector3 temp = rb2dInstance.gameObject.transform.rotation.eulerAngles;

                temp.z = (radians * Mathf.Rad2Deg);
                
                rb2dInstance.transform.rotation = Quaternion.Euler(temp);

                rb2dInstance.transform.localScale = new Vector2(range * 100, rb2dInstance.transform.localScale.y);

                rb2dInstances.Add(rb2dInstance);
                //Vector3 temp = rb2dInstance.gameObject.transform.rotation.eulerAngles;

                //temp.z = temp.z - 45;

                //rb2dInstance.gameObject.transform.rotation = Quaternion.Euler(temp);
                //rb2dInstance.gameObject.transform.position = transform.position;
            }
            else
            {
                rb2dInstance.velocity = (new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * projectileSpeed);
            }

            projectileInstance.startPos = rb2dInstance.transform.position;
            if(snap)
            {
                projectileInstance.damage = Random.Range(0, damage * 2);
            }
            else
            {
                projectileInstance.damage = damage;
            }
            projectileInstance.range = 0;
        }

        timer = fireRate;
    }

    void CheckSpread()
    {
        if(restraint)
        {
            spread -= 10;
            if (spread < 1)
            {
                spread = 1;
            }
        }
    }
}

