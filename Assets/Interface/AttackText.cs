using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackText : MonoBehaviour {

    protected Text text;

    protected Attack attack;
	// Use this for initialization
	void Start ()
    {
        text = GetComponent<Text>();

        attack = GameObject.Find("Canvas").GetComponent<AttackStatus>().attack;
	}
}
