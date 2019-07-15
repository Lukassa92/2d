using System;

namespace Core
{
    public abstract class GameAction
    {
    }

    public class GameAction<T> : GameAction where T : struct, IConvertible
    {
        public T Type { get; private set; }

        public GameAction(T type)
        {
            Type = type;
        }
    }
}
