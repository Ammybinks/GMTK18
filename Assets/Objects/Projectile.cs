using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public int damage;
    public float range;

    public Vector2 startPos;

    Vector2 difference;
	// Update is called once per frame
	void Update () {
        difference = startPos - new Vector2(transform.position.x, transform.position.y);

        if(Mathf.Sqrt((Mathf.Pow(difference.x, 2) + Mathf.Pow(difference.y, 2))) >= range)
        {
            Destroy(gameObject);
        }
    }
}
