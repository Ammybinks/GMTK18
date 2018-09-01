using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public string itemName;
    public string itemDesc;

    public int distance;

    int threshold = 2;

    bool currentlyActive;

    Vector2 difference;

    GameObject player;
    ItemDialog dialog;

	void Start () {
        player = GameObject.Find("Player");
        dialog = GameObject.Find("Canvas").GetComponent<ItemDialog>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        difference = new Vector2(player.transform.position.x, player.transform.position.y) - new Vector2(transform.position.x, transform.position.y);

        if (Mathf.Sqrt((Mathf.Pow(difference.x, 2) + Mathf.Pow(difference.y, 2))) <= threshold)
        {
            currentlyActive = true;

            dialog.currentItem = this;
        }
        else if (currentlyActive)
        {
            currentlyActive = false;

            dialog.currentItem = null;
        }
    }

    public void OnDestroy()
    {
        dialog.currentItem = null;
    }
}
