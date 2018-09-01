using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    public Rigidbody2D projectile;

    public int damage = 5;
    public float fireRate = 1;
    public int lines = 1;
    public int spread = 5;
    public int projectileSpeed = 5;
    public float range = 2;

    public bool rayCasted;
    public bool homing;

    public string effects;

    float timer;

    Vector3 mousePosition;
    float mousePositionY;
    float mousePositionX;
    float radians;

    Rigidbody2D rb2d;

    ItemDialog item;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        
        item = GameObject.Find("Canvas").GetComponent<ItemDialog>();
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

        if(timer > 0)
        {
            timer -= Time.deltaTime;

            if(timer < 0)
            {
                timer = 0;
            }
        }

        if(Input.GetMouseButton(0) && timer == 0)
        {
            if(lines > 1)
            {
                float degrees = radians * Mathf.Rad2Deg;

                degrees -= spread;

                float increment = (spread * 2) / (lines - 1);

                for (int i = 0; i < lines; i++)
                {
                    Rigidbody2D rb2dInstance = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));

                    //Rigidbody2D rb2dInstance = instance.GetComponent<Rigidbody2D>();
                    Projectile projectileInstance = rb2dInstance.GetComponent<Projectile>();

                    radians = (degrees + (increment * i)) * Mathf.Deg2Rad;

                    rb2dInstance.velocity = (new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * projectileSpeed);

                    projectileInstance.startPos = rb2dInstance.transform.position;
                    projectileInstance.damage = damage;
                    projectileInstance.range = range;
                }
            }
            else
            {
                Rigidbody2D rb2dInstance = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));

                //Rigidbody2D rb2dInstance = instance.GetComponent<Rigidbody2D>();
                Projectile projectileInstance = rb2dInstance.GetComponent<Projectile>();

                rb2dInstance.velocity = (new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * projectileSpeed);

                projectileInstance.startPos = rb2dInstance.transform.position;
                projectileInstance.damage = damage;
                projectileInstance.range = range;
            }

            timer = fireRate;
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            if (item.currentItem != null)
            {
                if(item.currentItem.itemName == "Shotgunny")
                {
                    damage -= 2;
                    lines += 2;
                    effects += "* +2 Projectiles\n";
                }
                if (item.currentItem.itemName == "Yellow Fever")
                {
                    fireRate -= (float)0.75;
                    range -= (float)7.5;
                }

                Destroy(item.currentItem.gameObject);
            }
        }
    }
}
