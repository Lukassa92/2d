using Core;
using Level.AI;
using Level.Classes;
using Level.Behaviours;
using UnityEngine;

public class GameEntity : MonoBehaviour
{
    public AiManagerModule AiManagerModule;
    public LevelEntity LevelEntity { get; private set; }

    private ViewRangeDetector _gameEntityDetector;
    private AttackDetector _attackDetector;
    private EntitySettings _settings;

    private GameActionStore _store;
    public GameActionStore Store
    {
        get { return _store = _store ?? new GameActionStore(); }
    }

    public bool IsAlive => LevelEntity.IsAlive;
    public EntityType EntityType => LevelEntity.EntityType;

    public void Start()
    {
        _gameEntityDetector = GetComponentInChildren<ViewRangeDetector>();
        _attackDetector = GetComponentInChildren<AttackDetector>();
        _settings = GetComponentInChildren<EntitySettings>();
        _gameEntityDetector.SetVisibility(_settings.ViewRange);
        _attackDetector.SetHitRange(_settings.AttackRange);
        LevelEntity = new LevelEntity(_settings, this);
    }
}
