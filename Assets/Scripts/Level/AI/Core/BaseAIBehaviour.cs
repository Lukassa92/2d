﻿using System;

public abstract class BaseAIBehaviour : AIEventReceiver
{
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
    private int _actionPriority;

    public virtual void Unselect(BaseAIBehaviour behaviour)
    {
    }

    internal abstract TimeSpan Execute();
}