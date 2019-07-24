namespace Level.AI
{
    public interface IAIEventReceiver
    {
        void OnCollisionWith(GameEntity entity);
        void OnDamagedOther(DamageSource source);
        void OnEntityDestroyed(GameEntity entity);
        void OnEntityDied(GameEntity entity);
        void OnEntityEnteredAttackRadius(GameEntity entity);
        void OnEntityEnteredViewRadius(GameEntity entity);
        void OnEntityLeftAttackRadius(GameEntity entity);
        void OnEntityLeftViewRadius(GameEntity entity);
        void OnOwnerDamaged(DamageSource source);
        void OnOwnerHealed(HealSource source);
        void OnOwnerSpawned();
        void OnTick();
    }
}