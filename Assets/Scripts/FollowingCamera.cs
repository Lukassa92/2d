using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    private GameEntity _character;
    private Camera _camera;
    public bool Follow = true;
    private float _forshadowingFactor = -120.0f;

	// Use this for initialization
	void Start ()
	{
	    _character = GetComponentInParent<GameEntity>();
	    _camera = GameObject.FindObjectOfType<Camera>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (Follow)
	    {
	        _camera.transform.position = new Vector3(_character.transform.position.x - _forshadowingFactor, _camera.transform.position.y,
	            _camera.transform.position.z);
        }
	        
	}
}
