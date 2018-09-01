using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStatus : MonoBehaviour {

    GameObject dialog;
    GameObject prompt;

    public Attack attack;
	// Use this for initialization
	void Awake ()
    {
        dialog = GameObject.Find("Attack Status");
        prompt = GameObject.Find("Attack Prompt");
        attack = GameObject.Find("Player").GetComponent<Attack>();

        dialog.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.F))
        {
            dialog.SetActive(!dialog.active);
            prompt.SetActive(!prompt.active);
        }
	}
}
