using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class HitEnemy : MonoBehaviour
{
    private MovementBehaviour _movementBehaviour;
    
	// Use this for initialization
	void Start ()
	{
//	    _movementBehaviour = GameObject.Find("Fred").GetComponent<MovementBehaviour>();
    }
    
//    void OnCollisionEnter2D(Collision2D col)
//    {
//        if (col.transform.tag == "Enemy")
//        {
//            Debug.Log("Hit Enemy");
//            _movementBehaviour.Attack();
//            
//        } else if (col.transform.name == "Wall")
//        {
//            Debug.Log("Hit obstacle");
//            _movementBehaviour.StopMovement();
//        }
//    }
   
}
