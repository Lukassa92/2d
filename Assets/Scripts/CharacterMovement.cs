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
//        Debug.Log("Stop!");
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
            //            _rigidbody2D.velocity = new Vector2(_speed, _rigidbody2D.velocity.y);
            //            _rigidbody2D.AddForce(new Vector2(_speed, _rigidbody2D.velocity.y),ForceMode2D.Force);
            Vector3 temp = _parenTransform.transform.position;
            temp.x = (_speed /100 )* Time.deltaTime;
            _parenTransform.transform.position = temp;
        }
    }

    public void DebugLog(string msg)
    {
        Debug.Log("Msg: " +msg);
    }

    public void RunTo(Vector3 gameEntityPosition)
    {
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
        _speed = GetSpeedForDirection(direction);
        _state = States.State.Run;
    }

    public void LookAt(Vector3 position)
    {
        Debug.Log("in look at via position");
        var direction = GetDirectionTo(position);
        _parenTransform.localScale =
            new Vector3(_parenTransform.localScale.x * (direction == States.MoveDirection.Left ? 1 : -1),
                _parenTransform.localScale.y, _parenTransform.localScale.z);
    }

    public void LookAt(States.MoveDirection direction)
    {
        Debug.Log("in look at via direction");
        _parenTransform.localScale =
            new Vector3(_parenTransform.localScale.x * (direction == States.MoveDirection.Left ? 1 : -1),
                _parenTransform.localScale.y, _parenTransform.localScale.z);
    }
    
}
