using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement: MonoBehaviour
{
    public enum State
    {
        Run,
        Fight,
        Stand
    }
    public enum Direction
    {
        Left,
        Right
    }
    private float _speed = 150.0f;
    [SerializeField]
    protected internal State CharacterState = State.Run;
    
    private Direction _direction = Direction.Right;
    private Rigidbody2D _rigidbody2D;

    private Vector3 _rigidbody2DVelocity;
	// Use this for initialization
	void Start () {
		
	}

    private void Awake()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    public void StopMovement()
    {
        CharacterState = State.Stand;
    }

    public void Attack()
    {
        Debug.Log("Attacke!");
        StopMovement();
        CharacterState = State.Fight;
        //Play attack animation
        //start calculate damage at enemy and by you
        StartCoroutine(DeleteEnemyForDev());
    }

    private IEnumerator DeleteEnemyForDev()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(GameObject.Find("Enemy"));
        Run();
    }

    public void Run()
    {
        CharacterState = State.Run;
    }

    // Update is called once per frame
    private void Update () {
        //Move Forward
        if (CharacterState == State.Run)
        {
            if (_direction == Direction.Right)
            {
                Debug.Log("Turn right");
                _rigidbody2D.velocity = new Vector2(_speed, _rigidbody2D.velocity.y);
            }
            else
            {
                _rigidbody2D.velocity = new Vector2(-_speed, _rigidbody2D.velocity.y);
            }
            
        }        
	}

    public void RunToTarget(float targetNearstEnemy)
    {
        if (targetNearstEnemy < transform.position.y)
        {
//            Hier muss sich der Character umdrehen
            FlipChar();
        }
        else
        {
            FlipChar();
        }
        Run();
    }

    private void FlipChar()
    {
        if (_direction == Direction.Right)
        {
            _direction = Direction.Left;
            transform.localScale = new Vector3(- transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            _direction = Direction.Right;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
}
