using System.Collections.Generic;
using Level.AI;

public class MeleeUnitAI : BaseAI
{
    public MeleeUnitAI(GameEntity owner)
        : base(owner)
    {
    }

    protected override List<BaseAIBehaviour> GetBehaviours()
    {
        return new List<BaseAIBehaviour>(new MovementAIBehaviour[]
        {
            new MeleeMovementAIBehaviour(Owner),
            new MeeleAttackAI(Owner)
        });
    }
}