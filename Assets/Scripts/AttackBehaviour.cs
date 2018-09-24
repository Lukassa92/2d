using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public TimeSpan Attack(GameEntity attackTarget)
    {
        Debug.Log("Attacke!");
        return TimeSpan.FromSeconds(1);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
