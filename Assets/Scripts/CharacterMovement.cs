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

    public void Run(States.State charState, Rigidbody2D charRigidbody2D, float charSpeed = 150.0f, GameTarget charTarget = null)
    {
        _state = charState;
        _rigidbody2D = charRigidbody2D;
        if (charTarget != null)
        {
//            if(charTarget.Position.x > )
        }
        if (_moveDirection == States.MoveDirection.Left)
        {
            GoToLeft();
        }
        else
        {
            GoToRight();
        }
    }

    private void GoToLeft()
    {
        _speed = -150.0f;
    }
    private void GoToRight()
    {
        _speed = 150.0f;
    }
    private void SetMovementDirectionByTag()
    {
        Debug.Log("entiotyType is: " + _entityType);
        if (_entityType == "Enemy")
        {
            _moveDirection = States.MoveDirection.Left;
            FlipChar();
            GoToLeft();
            Debug.Log("Left entiotyType is: " + _entityType + " speed is: "+_speed);
        }
        else if (_entityType == "Unit")
        {
            _moveDirection = States.MoveDirection.Right;
            FlipChar();
            GoToRight();
            Debug.Log("Right entiotyType is: " + _entityType + " speed is: " + _speed);
        }
//        else
//        {
//            _moveDirection = States.MoveDirection.Right;
//            FlipChar();
//            _speed = 150.0f;
//        }
    }
    // Update is called once per frame
    private void Update () {

        if (_state == States.State.Run)
        {
            _rigidbody2D.velocity = new Vector2(_speed, _rigidbody2D.velocity.y);
        }   
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
