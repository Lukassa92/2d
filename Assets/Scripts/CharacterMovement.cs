using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement: MonoBehaviour
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
        //Den scheiß noch in den MovementService auslagern
        if (charTarget != null)
        {
            if (charTarget.Position.x > _parenTransform.position.x)
            {
                GoToRight();
            }
            else
            {
                GoToLeft();
            }
        }
        else
        {
            if (_standardMoveDirection == States.MoveDirection.Left)
            {
                GoToLeft();
            }
            else
            {
                GoToRight();
            }
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
        SetSpeed(-150.0f);
        _moveDirection = States.MoveDirection.Left;
    }
    private void GoToRight()
    {
        SetSpeed(150.0f);
        _moveDirection = States.MoveDirection.Right;
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }
    private void SetMovementDirectionByTag()
    {
        if (_entityType == "Enemy")
        {
            _moveDirection = States.MoveDirection.Left;
            _standardMoveDirection = States.MoveDirection.Left;
            FlipChar();
            GoToLeft();
        }
        else if (_entityType == "Unit")
        {
            _moveDirection = States.MoveDirection.Right;
            _standardMoveDirection = States.MoveDirection.Right;
            FlipChar();
            GoToRight();
        }
        else
        {
            _moveDirection = States.MoveDirection.Right;
            _standardMoveDirection = States.MoveDirection.Right;
            FlipChar();
            _speed = 150.0f;
        }
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
