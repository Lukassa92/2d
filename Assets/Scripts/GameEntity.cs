using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntity : MonoBehaviour {

    [SerializeField]
    private States.State _state = States.State.Run;
    [SerializeField]
    private States.MoveDirection _direction = States.MoveDirection.Right;
    [SerializeField]
    private string _entityType;
    private GameTarget _newTarget;
    private GameTarget _oldTarget;
    private bool _targetHasChanged = false;

    private CharacterMovement _characterMovement;
	// Use this for initialization
    void Start()
    {
        _entityType = transform.tag;
        _characterMovement = GetComponent<CharacterMovement>();
        _characterMovement.Run(_state, GetComponent<Rigidbody2D>());
    }

    public GameTarget GetNewTarget()
    {
        return _newTarget;
    }

    public string GetEntityType()
    {
        return _entityType;
    }
    public void SetNewTarget(GameTarget target)
    {
        if (_targetHasChanged)
        {
            SetOldTarget(_newTarget);
        }
        _newTarget = target;
    }

    public void SwitchTargetHasChanged()
    {
        _targetHasChanged = !_targetHasChanged;
    }
    private void SetOldTarget(GameTarget target)
    {
        _oldTarget = target;
    }
	// Update is called once per frame
	void FixedUpdate () {
	    
	}
}
