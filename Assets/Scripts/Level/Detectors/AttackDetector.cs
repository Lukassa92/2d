using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDetector : MonoBehaviour
{

    private string _entityType;
    private GameEntity _gameEntity;

    // Use this for initialization
	void Start ()
	{
	    _gameEntity = GetComponentInParent<GameEntity>();
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        var entity = coll.GetComponent<GameEntity>();
        if (coll is BoxCollider2D && entity != null)
        {
            _gameEntity.AiManagerModule.OnEntityEnteredAttackRadius(entity);
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        var entity = coll.GetComponent<GameEntity>();
        if (coll is BoxCollider2D && entity != null)
        {
            _gameEntity.AiManagerModule.OnEntityLeftAttackRadius(entity);
        }
    }

    public void SetHitRange(float hitRange)
    {
        GetComponent<CircleCollider2D>().radius = hitRange;
    }
}
