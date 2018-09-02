using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    public delegate void Activate(string room);
    public static event Activate ActivateRoom;

    public delegate void DeActivate();
    public static event DeActivate DeActivateRoom;

    public int killCount;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GetComponent<Collider2D>().enabled = false;

            if (ActivateRoom != null)
            {
                ActivateRoom(gameObject.name);
            }
        }
    }

    private void Update()
    {
        if(killCount <= 0)
        {
            if(DeActivateRoom != null)
            {
                DeActivateRoom();
            }
        }
    }

}
