using MoreLinq;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementAIBehaviour : BaseAIBehaviour
{
    internal readonly CharacterMovement Movement;
    internal readonly GameEntity Owner;
    internal readonly List<GameEntity> EntitiesInView = new List<GameEntity>();
    internal TimeSpan ActionOffset = TimeSpan.FromMilliseconds(250);


    protected MovementAIBehaviour(GameEntity owner)
    {
        Owner = owner;
        Movement = Owner.GetComponent<CharacterMovement>();
    }

    public override void OnEntityEnteredViewRadius(GameEntity entity)
    {
        if (!EntitiesInView.Contains(entity))
        {
            EntitiesInView.Add(entity);
        }
    }

    public override void OnEntityLeftViewRadius(GameEntity entity)
    {
        Movement.DebugLog("Left View Radius");
        if (EntitiesInView.Contains(entity))
        {
            EntitiesInView.Remove(entity);
        }
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