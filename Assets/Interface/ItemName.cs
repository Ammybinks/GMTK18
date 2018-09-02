﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemName : MonoBehaviour {

    Text text;

    ItemDialog dialog;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        dialog = GameObject.Find("Canvas").GetComponent<ItemDialog>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dialog.currentItem != null)
        {
            text.text = dialog.currentItem.itemName;
        }
    }
}
