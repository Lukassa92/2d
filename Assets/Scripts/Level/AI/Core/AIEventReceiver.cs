public class AIEventReceiver
{
    public virtual void OnEntityEnteredViewRadius(TargetEntity entity) { }
    public virtual void OnEntityLeftViewRadius(TargetEntity entity) { }
    public virtual void OnEntityEnteredAttackRadius(TargetEntity entity) { }
    public virtual void OnEntityLeftAttackRadius(TargetEntity entity) { }
    public virtual void OnOwnerDamaged(DamageSource source) { }
    public virtual void OnOwnerSpawned() { }
    public virtual void OnOwnerHealed(HealSource source) { }
    public virtual void OnDamagedOther(DamageSource source) { }
    public virtual void OnCollisionWith(TargetEntity entity) { }
    public virtual void OnTick() { }
}