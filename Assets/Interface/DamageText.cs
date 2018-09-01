using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : AttackText {
    
	// Update is called once per frame
	void Update () {
        text.text = "Damage:" + attack.damage;
	}
}
