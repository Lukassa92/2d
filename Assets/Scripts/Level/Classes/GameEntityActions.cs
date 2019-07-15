using Core;

namespace Level.Classes
{
    public enum GameEntityActionTypes
    {
        HealthChanged
    }

    public class HealthChangedAction : GameAction<GameEntityActionTypes>
    {
        public int OldHealth { get; private set; }
        public int NewHealth { get; private set; }
        public int MaxHealth { get; private set; }

        public HealthChangedAction(int oldHealth, int newHealth, int maxHealth) : base(GameEntityActionTypes.HealthChanged)
        {
            OldHealth = oldHealth;
            NewHealth = newHealth;
            MaxHealth = maxHealth;
        }
    }
}
