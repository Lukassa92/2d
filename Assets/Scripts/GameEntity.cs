using System;
using Core;
using Level.AI;
using Level.Classes;
using Level.Behaviours;
using Level.Detectors;
using UniRx;
using UnityEngine;

public class GameEntity : MonoBehaviour
{
    public AiManagerModule AiManagerModule;
    public LevelEntity LevelEntity { get; private set; }

    private ViewRangeDetector _gameEntityDetector;
    private AttackDetector _attackDetector;
    private EntitySettings _settings;

    private GameActionStore _store;
    private IDisposable _subscription;

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
        AiManagerModule = GetComponentInChildren<AiManagerModule>();

        _subscription = Store.Observable.OfActionType<EntityDeathAction>().Subscribe(a =>
        {
            Destroy(gameObject);
        });
    }

    private void OnDestroy()
    {
        _subscription?.Dispose();
    }
}
