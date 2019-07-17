using Core;
using JetBrains.Annotations;
using Level.Classes;
using System;
using UniRx;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{
    private MovementState _movementState = MovementState.Stand;
    [SerializeField]
    private MoveDirection _moveDirection;
    private MoveDirection _standardMoveDirection;
    [SerializeField]
    private string _entityType;
    private Transform _parenTransform;
    private Rigidbody2D _rigidbody2D;
    [SerializeField]
    private float _speed;

    private GameEntity _gameEntity;
    private IDisposable _subscription;

    void Start()
    {
        _parenTransform = GetComponentInParent<Transform>();
        _rigidbody2D = GetComponentInParent<Rigidbody2D>();
        _gameEntity = GetComponentInParent<GameEntity>();
        _subscription = _gameEntity.Store.Observable.OfActionTypes(GameEntityActionTypes.MoveToDirection, GameEntityActionTypes.LookAt, GameEntityActionTypes.StopMovement).Subscribe(
            a =>
            {
                switch (a.Type)
                {
                    case GameEntityActionTypes.MoveToDirection:
                        MoveToLocation(a as MoveToLocationAction);
                        break;
                    case GameEntityActionTypes.LookAt:
                        LookAt(((LookAtAction)a).TargetPosition);
                        break;
                    case GameEntityActionTypes.StopMovement:
                        StopMovement();
                        break;
                }
            });
    }

    public void StopMovement()
    {
        //        Debug.Log("Stop!");
        _movementState = MovementState.Stand;
    }

    private void Update()
    {
        if (_movementState == MovementState.Run)
        {
            _rigidbody2D.velocity = new Vector2(_speed, _rigidbody2D.velocity.y);
            //            _rigidbody2D.AddForce(new Vector2(_speed, _rigidbody2D.velocity.y),ForceMode2D.Force);

        }
        else
        {
            _rigidbody2D.velocity = Vector2.zero;
        }
    }

    public void RunTo(Vector3 gameEntityPosition)
    {
        var direction = GetDirectionTo(gameEntityPosition);
        _speed = GetSpeedForDirection(direction);
        _movementState = MovementState.Run;
    }

    private float GetSpeedForDirection(MoveDirection direction)
    {
        return _gameEntity.BaseLevelEntity.BaseMovementSpeed * (direction == MoveDirection.Left ? -1 : 1);
    }

    private MoveDirection GetDirectionTo(Vector3 position)
    {
        return position.x > _parenTransform.position.x
            ? MoveDirection.Right
            : MoveDirection.Left;
    }

    public void RunTo(MoveDirection direction)
    {
        _speed = GetSpeedForDirection(direction);
        _movementState = MovementState.Run;
    }

    public void LookAt(Vector3 position)
    {
        var direction = GetDirectionTo(position);
        _parenTransform.localScale =
            new Vector3(_parenTransform.localScale.x * (direction == MoveDirection.Left ? 1 : -1),
                _parenTransform.localScale.y, _parenTransform.localScale.z);
    }

    public void LookAt(MoveDirection direction)
    {
        _parenTransform.localScale =
            new Vector3(_parenTransform.localScale.x * (direction == MoveDirection.Left ? 1 : -1),
                _parenTransform.localScale.y, _parenTransform.localScale.z);
    }

    private void MoveToLocation(MoveToLocationAction action)
    {
        LookAt(action.TargetPosition);
        RunTo(action.TargetPosition);
    }
}
