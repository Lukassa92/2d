using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitVideo : MonoBehaviour
{

    private CharacterMovement _characterMovement;
	// Use this for initialization
	void Start ()
	{
	    _characterMovement = GameObject.Find("Fred").GetComponent<CharacterMovement>();
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.name == "Enemy")
        {
            Debug.Log("Hit Enemy");
            _characterMovement.speed = 0.0f;
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
