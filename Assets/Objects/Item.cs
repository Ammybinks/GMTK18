using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Item : MonoBehaviour {

    //public delegate void Storage(float i);
    //public static event Storage Latch;

    public string itemName;
    public string itemDesc;

    public int distance;

    public bool latched;

    int threshold = 2;

    bool currentlyActive;

    Vector2 difference;

    GameObject player;
    ItemDialog dialog;

	void Start () {
        player = GameObject.Find("Player");
        dialog = GameObject.Find("Canvas").GetComponent<ItemDialog>();

        //GameObject item = GameObject.FindGameObjectWithTag(gameObject.tag);
        //if(item.name != gameObject.name)
        //{
        //    gameObject.SetActive(false);
        //}
        SceneManager.sceneLoaded += OnSceneLoaded;
        //Item.Latch += CheckLatch;
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

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(!latched)
        {
            Destroy(gameObject);
        }
    }

    //private void CheckLatch(float i)
    //{
    //    if((int)i == (int)itemIndex && i != itemIndex)
    //    {
    //        gameObject.SetActive(false);
    //    }
    //}
}
