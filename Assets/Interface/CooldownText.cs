using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownText : AttackText {
    
	// Update is called once per frame
	void Update ()
    {
        text.text = "Cooldown:" + attack.fireRate;
    }
}
