using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDetector : MonoBehaviour
{

    private string _attachedTo;

    private string _entityType;
    private GameEntity _gameEntity;

    // Use this for initialization
	void Start ()
	{
	    _gameEntity = GetComponentInParent<GameEntity>();
	    _attachedTo = _gameEntity.GetComponentInParent<Transform>().tag;
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.transform.tag == "Enemy" || coll.transform.tag == "Player" && coll.transform.tag != _attachedTo)
        {
//            Debug.Log("in reichweite für gegner: "+coll.transform.tag + " _ "+_attachedTo);
            _gameEntity.AI.OnEntityEnteredAttackRadius(GetTargetEntityFromCollider(coll));
        }
    }
    private GameEntity GetTargetEntityFromCollider(Collider2D coll)
    {
        return coll.GetComponentInParent<GameEntity>();
    }
    public void SetHitRange(float hitRange)
    {
        GetComponent<CircleCollider2D>().radius = hitRange;
    }
    // Update is called once per frame
    void Update () {
		
	}
}
