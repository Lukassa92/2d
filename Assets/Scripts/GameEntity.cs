using JetBrains.Annotations;
using System;
using UnityEngine;

public class GameEntity : MonoBehaviour
{
    public BaseAI AI;
    public LevelEntity LevelEntity;
    public string AIName = "MeleeUnitAI";
    public string LevelEntityName = "MeleeUnitLevelEntity";

    [SerializeField] public int Health = 100;

    [SerializeField] public bool IsAlive = true;

    [SerializeField] public EntityType EntityType;
    public bool StartRunningOnAwake = true;
    [Range(0.01f, 0.4f)] public float Visibility = 0.04f;
    [Range(0.01f, 0.4f)] public float HitRange = 0.02f;
    private GameEntityDetectionService _gameEntityDetector;
    private AttackDetector _attackDetector;
    private GameObject _scriptObject;
    public Vector3 Position = new Vector3(0,0,0);

    // Use this for initialization
    void Start()
    {
        Position = transform.position;
        EntityType = (EntityType)Enum.Parse(typeof(EntityType), transform.tag);
        _gameEntityDetector = GetComponentInChildren<GameEntityDetectionService>();
        _attackDetector = GetComponentInChildren<AttackDetector>();
        _scriptObject = GameObject.Find("ScriptObject");
        _gameEntityDetector.SetVisibility(Visibility);
        _attackDetector.SetHitRange(HitRange);
        
        LevelEntity = LevelEntityFactory.CreateLevelEntity(LevelEntityName);
        AI = AIFactory.CreateAI(AIName, this);

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
        Health = LevelEntity.Health;
        IsAlive = LevelEntity.IsAlive;
//        if (!LevelEntity.IsAlive)
//        {
//            _scriptObject.GetComponentInParent<DestroyService>().DestroyGameObjectByName(transform.name);
////            GameObject.Find(transform.name);
//        }
    }
}
