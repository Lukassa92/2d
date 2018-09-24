using System;
using System.Linq;

public class AIFactory
{
    public static BaseAI CreateAI(string aiName, GameEntity owner)
    {
        var type = typeof(BaseAI).Assembly
            .GetTypes().FirstOrDefault(t => t.IsSubclassOf(typeof(BaseAI)) && !t.IsAbstract && t.Name == aiName);
        if (type == null)
            throw new InvalidOperationException("No AI class found with the name " + aiName);
        return (BaseAI)Activator.CreateInstance(type, new object[] { owner });
    }
}