using JetBrains.Annotations;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private States.State _state = States.State.Stand;
    [SerializeField]
    private States.MoveDirection _moveDirection;
    private States.MoveDirection _standardMoveDirection;
    [SerializeField]
    private string _entityType;
    private Transform _parenTransform;
    private Rigidbody2D _rigidbody2D;
    [SerializeField]
    private float _speed;

    private GameEntity _gameEntity;

    void Start()
    {
        _parenTransform = GetComponentInParent<Transform>();
        _rigidbody2D = GetComponentInParent<Rigidbody2D>();
        _gameEntity = GetComponentInParent<GameEntity>();
    }
    public void StopMovement()
    {
        Debug.Log("Stop!");
        _state = States.State.Stand;
    }

    public void Attack()
    {
        Debug.Log("Attacke!");
        _state = States.State.Attack;
    }

    [UsedImplicitly]
    private void Update()
    {
        if (_state == States.State.Run)
        {
            _rigidbody2D.velocity = new Vector2(_speed, _rigidbody2D.velocity.y);
        }
    }

    public void RunTo(Vector3 gameEntityPosition)
    {
        Debug.Log("Run is called");
        var direction = GetDirectionTo(gameEntityPosition);
        _speed = GetSpeedForDirection(direction);
        _state = States.State.Run;
    }

    private float GetSpeedForDirection(States.MoveDirection direction)
    {
        return _gameEntity.LevelEntity.Speed * (direction == States.MoveDirection.Left ? -1 : 1);
    }

    private States.MoveDirection GetDirectionTo(Vector3 position)
    {
        return position.x > _parenTransform.position.x
            ? States.MoveDirection.Right
            : States.MoveDirection.Left;
    }

    public void RunTo(States.MoveDirection direction)
    {
        Debug.Log("Run to direction is called");
        _speed = GetSpeedForDirection(direction);
        _state = States.State.Run;
    }

    //    private void FlipChar()
    //    {
    //        if (_moveDirection == States.MoveDirection.Right)
    //        {
    //            _moveDirection = States.MoveDirection.Left;
    //            _parenTransform.localScale = new Vector3(-_parenTransform.localScale.x, _parenTransform.localScale.y, _parenTransform.localScale.z);
    //        }
    //        else
    //        {
    //            _moveDirection = States.MoveDirection.Right;
    //            _parenTransform.localScale = new Vector3(_parenTransform.localScale.x, _parenTransform.localScale.y, _parenTransform.localScale.z);
    //        }
    //    }
}
