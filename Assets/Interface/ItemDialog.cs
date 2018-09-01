using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDialog : MonoBehaviour {

    public Item currentItem;

    GameObject dialog;

	// Use this for initialization
	void Start () {
        dialog = GameObject.Find("Item Dialog");
	}
	
	// Update is called once per frame
	void Update () {
		if(currentItem != null)
        {
            if(dialog.activeSelf == false)
            {
                dialog.SetActive(true);
            }
        }
        else
        {
            if(dialog.activeSelf == true)
            {
                dialog.SetActive(false);
            }
        }
	}
}
