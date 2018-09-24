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
        Movement.StopMovement();
    }

    public override void OnEntityLeftAttackRadius(GameEntity entity)
    {
        ActionPriority = 0;
        _attackTarget = null;
    }

    internal override TimeSpan Execute()
    {
        Movement.Attack();
        return TimeSpan.FromSeconds(1);
    }
}
