using System;

namespace Core
{
    public abstract class GameAction
    {
    }

    public class GameAction<T> : GameAction where T : struct, IConvertible
    {
        public readonly T Type;

        public GameAction(T type)
        {
            Type = type;
        }
    }
}
