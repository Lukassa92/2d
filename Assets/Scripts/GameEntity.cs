using Assets.Scripts.Level.Classes;
using JetBrains.Annotations;
using System;
using UnityEngine;

public class GameEntity : MonoBehaviour
{
    public BaseAI AI;
    public LevelEntity LevelEntity;
    public string AIName = "MeleeUnitAI";
    
    [SerializeField] public EntityType EntityType;
    private TargetEntity _newTargetEntity;
    private TargetEntity _oldTargetEntity;
    public bool StartRunningOnAwake = true;
    [Range(0.01f, 0.4f)] public float Visibility = 0.04f;
    [Range(0.01f, 0.4f)] public float HitRange = 0.02f;
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
        _gameEntityDetector = GetComponentInChildren<GameEntityDetectionService>();
        _attackDetector = GetComponentInChildren<AttackDetector>();

        _gameEntityDetector.SetVisibility(Visibility);
        _attackDetector.SetHitRange(HitRange);

        AI = AIFactory.CreateAI(AIName, new TargetEntity { GameEntity = this, LevelEntity = LevelEntity });

        if (StartRunningOnAwake)
        {
            //            _characterMovement.Run(_state, GetComponent<Rigidbody2D>());
        }

    }

    [UsedImplicitly]
    void FixedUpdate()
    {
        AI.OnTick();
    }
}
