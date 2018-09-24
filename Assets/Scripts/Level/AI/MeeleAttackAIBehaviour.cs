using System;

public class MeeleAttackAI : MovementAIBehaviour
{
    private GameEntity _attackTarget;

    public MeeleAttackAI(GameEntity owner) : base(owner)
    {
    }

    public override void OnEntityEnteredAttackRadius(GameEntity entity)
    {
        _attackTarget = entity;
        ActionPriority = 100;
    }

    public override void OnEntityLeftAttackRadius(GameEntity entity)
    {
        ActionPriority = 0;
        _attackTarget = null;
    }

    internal override TimeSpan Execute()
    {
        MovementBehaviour.StopMovement();
        return AttackBehaviour.Attack(_attackTarget);
    }
}
