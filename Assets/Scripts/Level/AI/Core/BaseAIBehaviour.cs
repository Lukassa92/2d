﻿using System;

public abstract class BaseAIBehaviour : AIEventReceiver
{
    internal readonly MovementBehaviour MovementBehaviour;
    internal readonly AttackBehaviour AttackBehaviour;
    internal readonly GameEntity Owner;

    protected BaseAIBehaviour(GameEntity owner)
    {
        Owner = owner;
        MovementBehaviour = Owner.GetComponent<MovementBehaviour>();
        AttackBehaviour = Owner.GetComponent<AttackBehaviour>();
    }

    public int ActionPriority
    {
        get { return _actionPriority; }
        set
        {
            var val = value;
            if (val < 0)
            {
                val = 0;
            }
            else if (val > 100)
            {
                val = 100;
            }
            _actionPriority = val;
        }
    }
    private int _actionPriority = 0;

    public virtual void Unselect(BaseAIBehaviour behaviour)
    {
    }

    internal abstract TimeSpan Execute();
}