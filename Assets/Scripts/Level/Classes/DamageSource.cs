namespace Level.Classes
{
    public class DamageSource
    {
        public LevelEntity Source { get; set; }
        public LevelEntity Target { get; set; }
        public int Damage { get; set; }
        public bool TargetKilled { get; set; } = false;
        public bool DamageDealt { get; set; } = true;
    }
}