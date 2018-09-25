using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
        InvokeRepeating("ClearDamageDisplay", 5.0f, 5.0f);
	}

    public void ClearDamageDisplay()
    {
        GetComponent<Text>().text = "";
    }
	// Update is called once per frame
	void Update () {
		
	}
}
