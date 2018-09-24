﻿using MoreLinq;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementAIBehaviour : BaseAIBehaviour
{
    internal readonly MovementBehaviour MovementBehaviour;
    internal readonly GameEntity Owner;
    internal readonly List<GameEntity> EntitiesInView = new List<GameEntity>();
    internal TimeSpan ActionOffset = TimeSpan.FromMilliseconds(250);


    protected MovementAIBehaviour(MovementBehaviour movementBehaviour, GameEntity owner)
    {
        MovementBehaviour = movementBehaviour;
        Owner = owner;
        // TODO: Nachher wieder entfernen
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
        MovementBehaviour.DebugLog("Left View Radius");
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