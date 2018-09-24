using System;
using System.Threading;

namespace Assets.Scripts.Level.Services
{
    public class MeleeAttackService
    {
        private static MeleeAttackService _meleeAttackService;
        public static MeleeAttackService GetService()
        {
            return _meleeAttackService ?? (_meleeAttackService = new MeleeAttackService());
        }

        public TimeSpan StartAttack(GameEntity attacker, GameEntity defender, Action onAttackDone)
        {
            var attackTime = TimeSpan.FromSeconds(1);
            var timer = new Timer(_ => OnTimerFinished(onAttackDone), null, attackTime, TimeSpan.FromMilliseconds(-1));
            return attackTime;
        }

        private void OnTimerFinished(Action attackDone)
        {
            attackDone();
        }
    }
}
