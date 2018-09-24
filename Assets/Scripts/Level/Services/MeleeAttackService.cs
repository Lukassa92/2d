using System;

namespace Assets.Scripts.Level.Services
{
    public class MeleeAttackService
    {
        private static MeleeAttackService _meleeAttackService;
        public static MeleeAttackService GetService()
        {
            return _meleeAttackService ?? (_meleeAttackService = new MeleeAttackService());
        }

        public TimeSpan StartAttack(GameEntity attacker, GameEntity defender, Action<DamageSource> onAttackDone)
        {
            var attackTime = TimeSpan.FromSeconds(1);
            var damageService = DamageService.GetService();
            var damageSource = damageService.DoDamage(attacker.LevelEntity, defender.LevelEntity, 4, 6);
            onAttackDone(damageSource);
            return attackTime;
        }
    }
}
