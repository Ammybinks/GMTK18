using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : Projectile
{
    //public LayerMask mask;

    RaycastHit2D hit;

    private void FixedUpdate()
    {
        //hit = Physics2D.Raycast(gameObject.transform.position, gameObject.transform.right, Mathf.Infinity, mask);
        Debug.DrawRay(gameObject.transform.position, gameObject.transform.right, Color.red);

        Vector3 rayPosition = gameObject.transform.right * 80;
        //rayPosition.y += 1;

        Debug.DrawRay(gameObject.transform.position, rayPosition, Color.red);
        if (hit)
        {
            Health health = hit.collider.gameObject.GetComponent<Health>();
            
            if (health)
            {
                health.health -= damage;
            }
        }
    }
}
