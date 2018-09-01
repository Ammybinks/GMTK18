using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeText : AttackText {
    
	// Update is called once per frame
	void Update () {
        text.text = "Range:" + attack.range;
	}
}
