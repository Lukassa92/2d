using System.Collections.Generic;
using System.Linq;
using MoreLinq;
using UnityEngine;

public class DestroyService : MonoBehaviour
{

    private GameObject _entities;
    // Use this for initialization
    void Start()
    {
        _entities = GameObject.Find("Entities");
        InvokeRepeating("CheckDeadGuys", 1.0f, 1.0f);
    }

    public void CheckDeadGuys()
    {
        foreach (var componentsInChild in _entities.GetComponentsInChildren<GameEntity>())
        {
            if (!componentsInChild.IsAlive)
            {
                var entityName = componentsInChild.GetComponentInParent<Transform>().name;
                Debug.Log("Destroy: " + entityName);
                DestroyGameObjectByName(entityName);
            }
        }
    }

    public void DestroyGameObjectByName(string name)
    {
        if (name == "MeeleUnit")
            GameObject.Find("MainCamera").GetComponent<FollowingCamera>().Follow = false;

        var gameObjectToDestroy = GameObject.Find(name);
        var gameEntityToDestroy = gameObjectToDestroy.GetComponent<GameEntity>();
        // TODO: Später wo anders handeln
        NotifyAllGameEntitiesOfDeath(gameEntityToDestroy);
        NotifyAllGameEntitiesOfDestroyed(gameEntityToDestroy);
        DestroyImmediate(gameObjectToDestroy);
    }

    private List<GameEntity> GetAllEntitiesInScene()
    {
        var entityGroup = GameObject.Find("Entities");
        return entityGroup.GetComponentsInChildren<GameEntity>().ToList();
    }

    public void NotifyAllGameEntitiesOfDestroyed(GameEntity entity)
    {
        GetAllEntitiesInScene().Where(e => e != entity).ForEach(e => e.AI.OnEntityDestroyed(e));
    }

    public void NotifyAllGameEntitiesOfDeath(GameEntity entity)
    {
        GetAllEntitiesInScene().Where(e => e != entity).ForEach(e => e.AI.OnEntityDied(e));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
