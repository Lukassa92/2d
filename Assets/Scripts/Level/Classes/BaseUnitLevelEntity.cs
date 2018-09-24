public class BaseUnitLevelEntity : LevelEntity
{
    public override EntityType Entity
    {
        get { return EntityType.Player; }
    }
}