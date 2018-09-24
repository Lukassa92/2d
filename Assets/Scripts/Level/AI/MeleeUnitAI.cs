using System.Collections.Generic;
using Assets.Scripts.Level.Classes;

public class MeleeUnitAI : BaseAI
{
    public MeleeUnitAI(TargetEntity owner, CharacterMovement movement)
        : base(owner, movement)
    {
    }

    protected override List<BaseAIBehaviour> Behaviours
    {
        get { return new List<BaseAIBehaviour>(new MovementAIBehaviour[] { new MeleeMovementAIBehaviour(Movement, Owner), new MeeleAttackAI(Movement, Owner) }); }
    }
}