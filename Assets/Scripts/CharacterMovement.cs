using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement: MonoBehaviour
{
    public float speed = 150.0f;
    private bool _running = true;

    private Rigidbody2D _rigidbody2D;
	// Use this for initialization
	void Start () {
		
	}

    private void Awake()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update () {
        //Move Forward 
        _rigidbody2D.velocity = new Vector2(speed, _rigidbody2D.velocity.y);
		//_rigidbody2D.AddForce(transform.forward * speed);
	}
}
