using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    SpriteRenderer sprite;
    Collider2D collider;

	// Use this for initialization
	void Start ()
    {
        Room.ActivateRoom += Activate;
        Room.DeActivateRoom += DeActivate;

        sprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();

        DeActivate();
    }
	
    private void Activate(string name)
    {
        if(gameObject.transform.parent.name == name)
        {
            sprite = GetComponent<SpriteRenderer>();

            Color color = sprite.color;
            color.a = 1;
            sprite.color = color;

            collider.enabled = true;
        }
    }

    private void DeActivate()
    {
        sprite = GetComponent<SpriteRenderer>();

        Color color = sprite.color;
        color.a = 0;
        sprite.color = color;

        collider.enabled = false;
    }
}
