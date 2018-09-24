using JetBrains.Annotations;
using System;
using UnityEngine;

public class GameEntity : MonoBehaviour
{
    public BaseAI AI;
    public LevelEntity LevelEntity;
    public string AIName = "MeleeUnitAI";
    public string LevelEntityName = "MeleeUnitLevelEntity";
    
    [SerializeField] public EntityType EntityType;
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

        LevelEntity = LevelEntityFactory.CreateLevelEntity(LevelEntityName);
        AI = AIFactory.CreateAI(AIName, this);

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
