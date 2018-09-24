﻿using System;
using System.Linq;

public class LevelEntityFactory
{
    public static LevelEntity CreateLevelEntity(string name)
    {
        var type = typeof(LevelEntity).Assembly
            .GetTypes().FirstOrDefault(t => t.IsSubclassOf(typeof(LevelEntity)) && !t.IsAbstract && t.Name == name);
        if (type == null)
            throw new InvalidOperationException("No level entity class found with the name " + name);
        return (LevelEntity)Activator.CreateInstance(type);
    }
}