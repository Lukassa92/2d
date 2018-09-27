using JetBrains.Annotations;
using System;
using UnityEngine;

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
    [SerializeField] public float MovementSpeed = global::MovementSpeed.Normal;
    [SerializeField] public double AttackSpeed = 1.0;
    [SerializeField] public int BaseDamage = 5;

    public bool StartRunningOnAwake = true;
    [Range(0.0f, 50.0f)] public float Visibility = 0.04f;
    [Range(0.0f, 50.0f)] public float HitRange = 0.02f;
    private ViewRangeDetector _gameEntityDetector;
    private AttackDetector _attackDetector;
    public Healthbar HealthBar;
    private GameObject _scriptObject;
    public Vector3 Position = new Vector3(0, 0, 0);

    // Use this for initialization
    void Start()
    {
        Position = transform.position;
        EntityType = (EntityType)Enum.Parse(typeof(EntityType), transform.tag);
        _gameEntityDetector = GetComponentInChildren<ViewRangeDetector>();
        _attackDetector = GetComponentInChildren<AttackDetector>();
        _scriptObject = GameObject.Find("ScriptObject");
        _gameEntityDetector.SetVisibility(Visibility);
        _attackDetector.SetHitRange(HitRange);
        HealthBar = GetComponentInChildren<Healthbar>();
        Health = MaxHealth;
        BaseLevelEntity = LevelEntityFactory.CreateLevelEntity(LevelEntityName, new object[] { MaxHealth, MovementSpeed, TimeSpan.FromSeconds(AttackSpeed), BaseDamage, this });
        AI = AIFactory.CreateAI(AIName, this);
        GetComponentInChildren<Healthbar>().enabled = true;
        if (StartRunningOnAwake)
        {
            //            _movementBehaviour.Run(_state, GetComponent<Rigidbody2D>());
        }

    }

    [UsedImplicitly]
    void FixedUpdate()
    {
        AI.OnTick();
    }

    public void Update()
    {
        Health = BaseLevelEntity.Health;
        IsAlive = BaseLevelEntity.IsAlive;
        //        if (!BaseLevelEntity.IsAlive)
        //        {
        //            _scriptObject.GetComponentInParent<DestroyService>().DestroyGameObjectByName(transform.name);
        ////            GameObject.Find(transform.name);
        //        }
    }
}
