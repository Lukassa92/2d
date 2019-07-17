using MoreLinq;
using System;
using System.Collections.Generic;
using UniRx;

public abstract class BaseAI : AIEventReceiver
{
    public GameEntity Owner { get; private set; }
    public MovementBehaviour MovementBehaviour { get; private set; }
    internal readonly List<BaseAIBehaviour> Behaviours;

    private BaseAIBehaviour _lastExecutedBehaviour;
    private DateTime _nextExecutionDate = DateTime.Now;
    private IDisposable _tickIntervalTimer;

    protected BaseAI(GameEntity owner)
    {
        Owner = owner;
        MovementBehaviour = owner.GetComponent<MovementBehaviour>();
        // ReSharper disable once VirtualMemberCallInConstructor
        Behaviours = GetBehaviours();
        _tickIntervalTimer = Observable.Interval(TimeSpan.FromSeconds(0.1)).Subscribe(x => OnTick());
    }
    protected abstract List<BaseAIBehaviour> GetBehaviours();

    private BaseAIBehaviour GetBehaviourWithHighestPriority()
    {
        return Behaviours.MaxBy(b => b.ActionPriority);
    }

    private void CheckNextExecution()
    {
        if (DateTime.Now >= _nextExecutionDate)
        {
            ExecuteNextBehaviour();
        }
    }

    private void ExecuteNextBehaviour()
    {
        var behaviour = GetBehaviourWithHighestPriority();
        if (behaviour != _lastExecutedBehaviour)
        {
            if (_lastExecutedBehaviour != null)
                _lastExecutedBehaviour.Unselect(behaviour);
            _lastExecutedBehaviour = behaviour;
        }
        var delay = _lastExecutedBehaviour.Execute();
        _nextExecutionDate = DateTime.Now + delay;
    }

    public override void OnEntityEnteredViewRadius(GameEntity entity)
    {
        Behaviours.ForEach(b => b.OnEntityEnteredViewRadius(entity));
    }

    public override void OnEntityLeftViewRadius(GameEntity entity)
    {
        Behaviours.ForEach(b => b.OnEntityLeftViewRadius(entity));
    }

    public override void OnEntityEnteredAttackRadius(GameEntity entity)
    {
        Behaviours.ForEach(b => b.OnEntityEnteredAttackRadius(entity));
    }

    public override void OnEntityLeftAttackRadius(GameEntity entity)
    {
        Behaviours.ForEach(b => b.OnEntityLeftAttackRadius(entity));
    }

    public override void OnOwnerDamaged(DamageSource source)
    {
        Behaviours.ForEach(b => b.OnOwnerDamaged(source));
    }

    public override void OnOwnerSpawned()
    {
        Behaviours.ForEach(b => b.OnOwnerSpawned());
    }

    public override void OnOwnerHealed(HealSource source)
    {
        Behaviours.ForEach(b => b.OnOwnerHealed(source));
    }

    public override void OnDamagedOther(DamageSource source)
    {
        Behaviours.ForEach(b => b.OnDamagedOther(source));
    }

    public override void OnCollisionWith(GameEntity entity)
    {
        Behaviours.ForEach(b => b.OnCollisionWith(entity));
    }

    public override void OnEntityDied(GameEntity entity)
    {
        Behaviours.ForEach(b => b.OnEntityDied(entity));
    }

    public override void OnEntityDestroyed(GameEntity entity)
    {
        Behaviours.ForEach(b => b.OnEntityDestroyed(entity));
    }

    public override void OnTick()
    {
        Behaviours.ForEach(b => b.OnTick());
        CheckNextExecution();
    }
}
