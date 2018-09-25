using MoreLinq;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementAIBehaviour : BaseAIBehaviour
{

    internal readonly List<GameEntity> EntitiesInView = new List<GameEntity>();
    internal TimeSpan ActionOffset = TimeSpan.FromMilliseconds(250);


    protected MovementAIBehaviour(GameEntity owner) : base(owner)
    {
    }

    public override void OnEntityEnteredViewRadius(GameEntity entity)
    {
        base.OnEntityEnteredViewRadius(entity);

        if (!EntitiesInView.Contains(entity))
            EntitiesInView.Add(entity);
    }

    public override void OnEntityLeftViewRadius(GameEntity entity)
    {
        base.OnEntityLeftViewRadius(entity);

        if (EntitiesInView.Contains(entity))
            EntitiesInView.Remove(entity);
    }

    public override void OnEntityDied(GameEntity entity)
    {
        base.OnEntityDied(entity);

        if (EntitiesInView.Contains(entity))
            EntitiesInView.Remove(entity);
    }

    public override void OnEntityDestroyed(GameEntity entity)
    {
        base.OnEntityDestroyed(entity);

        if (EntitiesInView.Contains(entity))
            EntitiesInView.Remove(entity);
    }

    public GameEntity GetClosestTarget(IEnumerable<GameEntity> targets)
    {
        return targets.MinBy(GetDistanceToTarget);
    }

    private float GetDistanceToTarget(GameEntity target)
    {
        return Vector3.Distance(Owner.Position, target.Position);
    }
}