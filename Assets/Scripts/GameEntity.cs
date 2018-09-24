using Assets.Scripts.Level.Classes;
using JetBrains.Annotations;
using System;
using UnityEngine;

public class GameEntity : MonoBehaviour
{
    public BaseAI AI;
    public LevelEntity LevelEntity;

    [SerializeField]
    private States.State _state = States.State.Run;
    [SerializeField]
    private States.MoveDirection _direction = States.MoveDirection.Right;
    [SerializeField] public EntityType EntityType;
    private TargetEntity _newTargetEntity;
    private TargetEntity _oldTargetEntity;
    private bool _targetHasChanged = false;
    private bool _stateHasChanged = false;
    public bool StartRunningOnAwake = true;
    [Range(0.01f, 0.4f)] public float Visibility = 0.04f;
    [Range(0.01f, 0.4f)] public float HitRange = 0.02f;
    private CharacterMovement _characterMovement;
    private GameEntityDetectionService _gameEntityDetector;
    private AttackDetector _attackDetector;
    public Vector3 Position
    {
        get { return transform.position; }
    }

    // Use this for initialization
    void Start()
    {
        EntityType = (EntityType)Enum.Parse(typeof(EntityType), transform.tag);
        _characterMovement = GetComponent<CharacterMovement>();
        _gameEntityDetector = GetComponentInChildren<GameEntityDetectionService>();
        _attackDetector = GetComponentInChildren<AttackDetector>();

        _gameEntityDetector.SetVisibility(Visibility);
        _attackDetector.SetHitRange(HitRange);

        if (StartRunningOnAwake)
        {
//            _characterMovement.Run(_state, GetComponent<Rigidbody2D>());
        }
        //        GetComponentInChildren<CircleCollider2D>().radius = Visibility;
        //        GetComponentInChildren<CircleCollider2D>().radius = HitRange;

    }

    public TargetEntity GetNewTarget()
    {
        return _newTargetEntity;
    }

    public void SetNewTarget(TargetEntity targetEntity)
    {
        if (_targetHasChanged)
        {
            SetOldTarget(_newTargetEntity);
        }
        _newTargetEntity = targetEntity;
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
    private void SetOldTarget(TargetEntity targetEntity)
    {
        _oldTargetEntity = targetEntity;
    }
    // Update is called once per frame
    [UsedImplicitly]
    void FixedUpdate()
    {
        if (_targetHasChanged)
        {
            _targetHasChanged = false;
//            if (_newTargetEntity != null)
//            {
//                _characterMovement.Run(States.State.Run, GetComponent<Rigidbody2D>(), 150.0f, _newTargetEntity);
//            }
//            else
//            {
//                _characterMovement.Run(States.State.Run, GetComponent<Rigidbody2D>());
//            }
        }

        if (_stateHasChanged)
        {
            _stateHasChanged = false;
            _characterMovement.Attack();
        }
    }
}
