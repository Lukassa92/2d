using System;

public class MeeleAttackAI : MovementAIBehaviour
{
    private GameEntity _attackTarget;

    public MeeleAttackAI(GameEntity owner) : base(owner)
    {
    }

    public override void OnEntityEnteredAttackRadius(GameEntity entity)
    {
        base.OnEntityEnteredViewRadius(entity);

        _attackTarget = entity;
        ActionPriority = 100;
    }

    public override void OnEntityLeftAttackRadius(GameEntity entity)
    {
        base.OnEntityLeftViewRadius(entity);

        ResetEnemy();
    }

    public override void OnEntityDied(GameEntity entity)
    {
        base.OnEntityDied(entity);

        ResetEnemy();
    }

    public override void OnEntityDestroyed(GameEntity entity)
    {
        base.OnEntityDestroyed(entity);

        ResetEnemy();
    }

    private void ResetEnemy()
    {
        ActionPriority = 0;
        _attackTarget = null;
    }

    internal override TimeSpan Execute()
    {
        MovementBehaviour.StopMovement();
        MovementBehaviour.LookAt(_attackTarget.Position);
        return AttackBehaviour.Attack(_attackTarget);
    }
}
