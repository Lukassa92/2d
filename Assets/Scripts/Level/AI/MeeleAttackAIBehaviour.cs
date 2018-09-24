using System;

public class MeeleAttackAI : MovementAIBehaviour
{
    private TargetEntity? _attackTarget;

    public MeeleAttackAI(CharacterMovement movement, TargetEntity owner) : base(movement, owner)
    {
    }

    public override void OnEntityEnteredAttackRadius(TargetEntity entity)
    {
        _attackTarget = entity;
        ActionPriority = 100;
        Movement.StopMovement();
    }

    public override void OnEntityLeftAttackRadius(TargetEntity entity)
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
