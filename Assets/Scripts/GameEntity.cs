using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
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
    private bool _stateHasChanged = false;
    public bool StartRunningOnAwake = true;
    [Range(0.01f, 0.4f)] public float Visibility = 0.04f;
    [Range(0.01f, 0.4f)] public float HitRange = 0.04f;
    private CharacterMovement _characterMovement;
	// Use this for initialization
    void Start()
    {
        _entityType = transform.tag;
        _characterMovement = GetComponent<CharacterMovement>();
        if (StartRunningOnAwake)
        {
            _characterMovement.Run(_state, GetComponent<Rigidbody2D>());
        }
        //        GetComponentInChildren<CircleCollider2D>().radius = Visibility;
        //        GetComponentInChildren<CircleCollider2D>().radius = HitRange;
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

    public void SetState(States.State state)
    {
        _state = state;
    }

    public States.State GetState()
    {
        return _state;
    }
    public void SwitchTargetHasChanged()
    {
        _targetHasChanged = !_targetHasChanged;
    }
    public void SwitchStateHasChanged()
    {
        _stateHasChanged = !_stateHasChanged;
    }
    private void SetOldTarget(GameTarget target)
    {
        _oldTarget = target;
    }
	// Update is called once per frame
	[UsedImplicitly]
	void FixedUpdate () {
	    if (_targetHasChanged)
	    {
	        _targetHasChanged = false;
	        if (_newTarget != null)
	        {
	            _characterMovement.Run(States.State.Run, GetComponent<Rigidbody2D>(), 150.0f, _newTarget);
            }
	        else
	        {
	            _characterMovement.Run(States.State.Run, GetComponent<Rigidbody2D>());
            }
	    }

	    if (_stateHasChanged)
	    {
	        _stateHasChanged = false;
            _characterMovement.Attack();
	    }
	}
}
