using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDetector : MonoBehaviour
{

    private string _attachedTo;
	// Use this for initialization
	void Start ()
	{
	    _attachedTo = GetComponentInParent<Transform>().tag;
	}

    void OnEnterTrigger2D(Collision coll)
    {
        if (coll.transform.tag == _attachedTo)
        {
            Debug.Log("in reichweite für gegner");
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
