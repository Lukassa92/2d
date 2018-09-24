using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleAttackAI : MovementAIBehaviour
{
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnEntityEnteredAttackRadius(TargetEntity entity)
    {
        Movement.StopMovement();
    }
    public MeeleAttackAI(CharacterMovement movement, TargetEntity owner) : base(movement, owner)
    {
    }
}
