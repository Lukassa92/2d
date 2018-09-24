using System.Collections.Generic;

public class MeleeUnitAI : BaseAI
{
    public MeleeUnitAI(GameEntity owner, CharacterMovement movement)
        : base(owner, movement)
    {
    }

    protected override List<BaseAIBehaviour> GetBehaviours()
    {
        return new List<BaseAIBehaviour>(new MovementAIBehaviour[]
        {
            new MeleeMovementAIBehaviour(Movement, Owner),
            new MeeleAttackAI(Movement, Owner)
        });
    }
}