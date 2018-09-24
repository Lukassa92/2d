public class BaseEnemyLevelEntity : LevelEntity
{
    public override EntityType Entity
    {
        get { return EntityType.Enemy; }
    }
}