using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectText : AttackText {
    
	// Update is called once per frame
	void Update ()
    {
        text.text = attack.effects.Replace("\\n", "\n");
    }
}
