using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStatus : MonoBehaviour {

    public GameObject dialog;
    public GameObject prompt;

    public Attack attack;
	// Use this for initialization
	void Awake ()
    {
        attack = GameObject.Find("Player").GetComponent<Attack>();

        dialog.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.F))
        {
            dialog.SetActive(!dialog.activeSelf);
            prompt.SetActive(!prompt.activeSelf);
        }
	}
}
