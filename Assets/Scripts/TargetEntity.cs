public struct TargetEntity
{
    public static TargetEntity Empty { get { return new TargetEntity(); } }
    public LevelEntity LevelEntity { get; set; }
    public GameEntity GameEntity { get; set; }
}
