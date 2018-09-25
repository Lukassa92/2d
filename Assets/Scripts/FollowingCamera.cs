using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    private GameObject _character;
    public bool Follow = true;
    private float _forshadowingFactor = -120.0f;

	// Use this for initialization
	void Start () {
        _character = GameObject.Find("MeeleUnit");
    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (Follow)
	    {
	        transform.position = new Vector3(_character.transform.position.x - _forshadowingFactor, transform.position.y,
	            transform.position.z);
        }
	        
	}
}
