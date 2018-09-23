using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class HitEnemy : MonoBehaviour
{
    private CharacterMovement _characterMovement;
    
	// Use this for initialization
	void Start ()
	{
	    _characterMovement = GameObject.Find("Fred").GetComponent<CharacterMovement>();
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Enemy")
        {
            Debug.Log("Hit Enemy");
//            _characterMovement.CharacterState = CharacterMovement.State.Fight;
//            _characterMovement.Attack();
            
        } else if (col.transform.name == "Wall")
        {
            Debug.Log("Hit obstacle");
//            _characterMovement.CharacterState = CharacterMovement.State.Stand;
//            _characterMovement.StopMovement();
        }
    }
   
}
