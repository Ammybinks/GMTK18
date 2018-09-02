using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthStatus : MonoBehaviour {

    RectTransform rectTransform;
    Vector2 size;
    Health health;
	// Use this for initialization
	void Start () {
        rectTransform = GetComponent<RectTransform>();

        health = GameObject.Find("Player").GetComponent<Health>();
	}
	
	// Update is called once per frame
	void Update () {
        size = rectTransform.localScale;
        size.x = health.health;
        size.y = health.health;
        rectTransform.localScale = size / 100;
	}
}
