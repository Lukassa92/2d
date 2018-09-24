using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    private GameEntity _gameEntity;

    private bool _canAttack = false;
	// Use this for initialization
	void Start ()
	{
	    _gameEntity = GetComponent<GameEntity>();
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (_canAttack)
        {
            var _damageSource = new DamageSource() {Source = _gameEntity.LevelEntity, Target = GetTargetEntityFromCollider(coll).LevelEntity ,Damage = 5};
            _gameEntity.AI.OnDamagedOther(_damageSource);
        }
    }

    private GameEntity GetTargetEntityFromCollider(Collider2D coll)
    {
        return coll.GetComponentInParent<GameEntity>();
    }

    public void Attack()
    {
        Debug.Log("Attacke!");
        //Starte Animation
        _canAttack = true;
    }
    // Update is called once per frame
    void Update () {
		
	}
}
