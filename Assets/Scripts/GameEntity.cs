using Core;
using Level.Classes;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

public class GameEntity : MonoBehaviour
{
    public BaseAI AI;
    public BaseLevelEntity BaseLevelEntity;
    public string AIName = "MeleeUnitAI";
    public string LevelEntityName = "MeleeUnitLevelEntity";

    [SerializeField] public int Health;
    [SerializeField] public bool IsAlive = true;
    [SerializeField] public EntityType EntityType;

    [SerializeField] public int MaxHealth = 100;
    [FormerlySerializedAs("MovementSpeed")] [SerializeField] public float BaseMovementSpeed = MovementSpeed.Normal;
    [SerializeField] public double AttackSpeed = 1.0;
    [SerializeField] public int BaseDamage = 5;
    [Range(0.0f, 50.0f)] public float Visibility = 0.04f;
    [Range(0.0f, 50.0f)] public float HitRange = 0.02f;

    private ViewRangeDetector _gameEntityDetector;
    private AttackDetector _attackDetector;
    public Vector3 Position = new Vector3(0, 0, 0);

    public GameActionStore Store { get; set; }

    // Use this for initialization
    void Start()
    {
        Store = new GameActionStore();

        Position = transform.position;
        EntityType = (EntityType)Enum.Parse(typeof(EntityType), transform.tag);
        _gameEntityDetector = GetComponentInChildren<ViewRangeDetector>();
        _attackDetector = GetComponentInChildren<AttackDetector>();
        _gameEntityDetector.SetVisibility(Visibility);
        _attackDetector.SetHitRange(HitRange);
        Health = MaxHealth;
        BaseLevelEntity = LevelEntityFactory.CreateLevelEntity(LevelEntityName, new object[] { MaxHealth, BaseMovementSpeed, TimeSpan.FromSeconds(AttackSpeed), BaseDamage, this });
        AI = AIFactory.CreateAI(AIName, this);
    }

    public void Update()
    {
        Health = BaseLevelEntity.Health;
        IsAlive = BaseLevelEntity.IsAlive;
    }
}
