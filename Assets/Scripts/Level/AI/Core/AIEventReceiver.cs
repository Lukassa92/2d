public class AIEventReceiver
{
    public virtual void OnEntityEnteredViewRadius(GameEntity entity) { }
    public virtual void OnEntityLeftViewRadius(GameEntity entity) { }
    public virtual void OnEntityEnteredAttackRadius(GameEntity entity) { }
    public virtual void OnEntityLeftAttackRadius(GameEntity entity) { }
    public virtual void OnOwnerDamaged(DamageSource source) { }
    public virtual void OnOwnerSpawned() { }
    public virtual void OnOwnerHealed(HealSource source) { }
    public virtual void OnDamagedOther(DamageSource source) { }
    public virtual void OnCollisionWith(GameEntity entity) { }
    public virtual void OnEntityDied(GameEntity entity) { }
    public virtual void OnEntityDestroyed(GameEntity entity) { }
    public virtual void OnTick() { }
}