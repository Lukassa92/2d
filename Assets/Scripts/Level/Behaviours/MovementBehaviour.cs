using System;
using Core;
using Level.Classes;
using UniRx;
using UnityEngine;

namespace Level.Behaviours
{
    public class MovementBehaviour : MonoBehaviour
    {
        [SerializeField]
        private MovementState _movementState = MovementState.Standing;
        private Transform _parentTransform;

        private Vector2? _movementTarget;

        private Rigidbody2D _rigidbody2D;
        private GameEntity _gameEntity;
        private IDisposable _subscription;

        void Start()
        {
            _parentTransform = GetComponentInParent<Transform>();
            _rigidbody2D = GetComponentInParent<Rigidbody2D>();
            _gameEntity = GetComponentInParent<GameEntity>();
            _subscription = _gameEntity.Store.Observable.OfActionTypes(GameEntityActionTypes.MoveToDirection, GameEntityActionTypes.MoveToEntity, GameEntityActionTypes.LookAt, GameEntityActionTypes.StopMovement).Subscribe(
                a =>
                {
                    switch (a.Type)
                    {
                        case GameEntityActionTypes.MoveToDirection:
                            MoveToLocation(a as MoveToLocationAction);
                            break;
                        case GameEntityActionTypes.MoveToEntity:
                            MoveToEntity(a as MoveToEntityAction);
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

        private void OnDestroy()
        {
            _subscription?.Dispose();
        }

        public void StopMovement()
        {
            _movementState = MovementState.Standing;
            _movementTarget = null;
        }

        private void Update()
        {
            if (_movementState == MovementState.Standing || _movementTarget == null)
                return;

            var step = _gameEntity.LevelEntity.BaseMovementSpeed * Time.deltaTime;
            var vector = Vector2.MoveTowards(_parentTransform.position, _movementTarget.Value, step);
            _rigidbody2D.MovePosition(vector);
        }

        private MoveDirection GetDirectionTo(Vector3 position)
        {
            return position.x > _parentTransform.position.x
                ? MoveDirection.Right
                : MoveDirection.Left;
        }

        public void LookAt(Vector3 position)
        {
            var direction = GetDirectionTo(position);
            _parentTransform.localScale =
                new Vector3(_parentTransform.localScale.x * (direction == MoveDirection.Left ? 1 : -1),
                    _parentTransform.localScale.y, _parentTransform.localScale.z);
        }

        public void LookAt(MoveDirection direction)
        {
            _parentTransform.localScale =
                new Vector3(_parentTransform.localScale.x * (direction == MoveDirection.Left ? 1 : -1),
                    _parentTransform.localScale.y, _parentTransform.localScale.z);
        }

        private void MoveToLocation(MoveToLocationAction action)
        {
            LookAt(action.TargetPosition);
            _movementState = MovementState.Moving;
            _movementTarget = action.TargetPosition;
        }

        private void MoveToEntity(MoveToEntityAction action)
        {
            LookAt(action.TargetGameObject.transform.position);
            _movementState = MovementState.Moving;
            _movementTarget = action.TargetGameObject.transform.position;
        }
    }
}
