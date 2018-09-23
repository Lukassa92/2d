using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement: MonoBehaviour
{
    private States.State _state = States.State.Stand;
    [SerializeField]
    private States.MoveDirection _moveDirection;
    private string _entityType;
    private Transform _parenTransform;
    private Rigidbody2D _rigidbody2D;
    private float _speed;
    
    void Start()
    {
        //Hier durch muss der Detector immer ein DIREKTES Kindelement sein
        _entityType = GetComponentInParent<Transform>().tag;
        _parenTransform = GetComponentInParent<Transform>();
        SetMovementDirectionByTag();
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

    public void Run(States.State charState, Rigidbody2D charRigidbody2D, float charSpeed = 150.0f)
    {
        _state = charState;
        _rigidbody2D = charRigidbody2D;
        if (_moveDirection == States.MoveDirection.Left)
        {
            _speed = -charSpeed;
        }
        else
        {
            _speed = charSpeed;
        }
        
    }
    private void SetMovementDirectionByTag()
    {
        Debug.Log("entiotyType is: " + _entityType);
        if (_entityType == "Enemy")
        {
            _moveDirection = States.MoveDirection.Left;
            Debug.Log("Left entiotyType is: " + _entityType);
            FlipChar();
        }
        else if (_entityType == "Unit")
        {
            _moveDirection = States.MoveDirection.Right;
            Debug.Log("Right entiotyType is: " + _entityType);
            FlipChar();
        }
        else
        {
            _moveDirection = States.MoveDirection.Right;
            FlipChar();
        }
    }
    // Update is called once per frame
    private void Update () {

        if (_state == States.State.Run)
        {
            _rigidbody2D.velocity = new Vector2(_speed, _rigidbody2D.velocity.y);
        }
        //Move Forward
        //if (CharacterState == State.Run)
        //{
        //    if (_direction == Direction.Right)
        //    {
        //        Debug.Log("Turn right");
        //        _rigidbody2D.velocity = new Vector2(_speed, _rigidbody2D.velocity.y);
        //    }
        //    else
        //    {
        //        _rigidbody2D.velocity = new Vector2(-_speed, _rigidbody2D.velocity.y);
        //    }
            
        //}        
	}

    private void FlipChar()
    {
        if (_moveDirection == States.MoveDirection.Right)
        {
            _moveDirection = States.MoveDirection.Left;
            _parenTransform.localScale = new Vector3(-_parenTransform.localScale.x, _parenTransform.localScale.y, _parenTransform.localScale.z);
        }
        else
        {
            _moveDirection = States.MoveDirection.Right;
            _parenTransform.localScale = new Vector3(_parenTransform.localScale.x, _parenTransform.localScale.y, _parenTransform.localScale.z);
        }
    }
}
