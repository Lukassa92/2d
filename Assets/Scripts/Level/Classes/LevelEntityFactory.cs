using System;
using System.Linq;

public class LevelEntityFactory
{
    public static BaseLevelEntity CreateLevelEntity(string name, object[] args = null)
    {
        var type = typeof(BaseLevelEntity).Assembly
            .GetTypes().FirstOrDefault(t => t.IsSubclassOf(typeof(BaseLevelEntity)) && !t.IsAbstract && t.Name == name);
        if (type == null)
            throw new InvalidOperationException("No level entity class found with the name " + name);
        if (args != null)
            return (BaseLevelEntity) Activator.CreateInstance(type, args);

        return (BaseLevelEntity)Activator.CreateInstance(type);
    }
}