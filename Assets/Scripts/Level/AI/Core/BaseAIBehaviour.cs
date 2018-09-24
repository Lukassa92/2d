using System;

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

    internal abstract TimeSpan ActionOffset { get; }
    internal DateTime LastAction = DateTime.MinValue;
    private int _actionPriority;

    internal bool NextActionPossible()
    {
        return DateTime.Now >= LastAction + ActionOffset;
    }

    internal void DoAction()
    {
        LastAction = DateTime.Now;
    }
}