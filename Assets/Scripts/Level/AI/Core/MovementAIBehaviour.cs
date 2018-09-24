using System;
using System.Collections.Generic;
using MoreLinq;
using UnityEngine;

public abstract class MovementAIBehaviour : BaseAIBehaviour
{
    internal readonly CharacterMovement Movement;
    internal readonly TargetEntity Owner;
    internal readonly List<TargetEntity> EntitiesInView = new List<TargetEntity>();
    internal TimeSpan ActionOffset = TimeSpan.FromMilliseconds(250);


    protected MovementAIBehaviour(CharacterMovement movement, TargetEntity owner)
    {
        Movement = movement;
        Owner = owner;
        // TODO: Nachher wieder entfernen
        ActionPriority = 100;
    }

    public override void OnEntityEnteredViewRadius(TargetEntity entity)
    {
        if (!EntitiesInView.Contains(entity))
            EntitiesInView.Add(entity);
    }

    public override void OnEntityLeftViewRadius(TargetEntity entity)
    {
        Movement.DebugLog("Left View Radius");
        if (EntitiesInView.Contains(entity))
            EntitiesInView.Remove(entity);
    }

    public TargetEntity GetClosestTarget(IEnumerable<TargetEntity> targets)
    {
        return targets.MinBy(GetDistanceToTarget);
    }

    private float GetDistanceToTarget(TargetEntity target)
    {
        return Vector3.Distance(Owner.GameEntity.Position, target.GameEntity.Position);
    }
}