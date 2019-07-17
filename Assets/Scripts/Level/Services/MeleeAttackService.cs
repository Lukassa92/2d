using System;

namespace Level.Services
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
            var damageService = DamageService.GetService();
            var damageSource = damageService.DoDamage(attacker.BaseLevelEntity, defender.BaseLevelEntity, attacker.BaseLevelEntity.BaseDamage);
            onAttackDone(damageSource);
            return attacker.BaseLevelEntity.AttackSpeed;
        }
    }
}
