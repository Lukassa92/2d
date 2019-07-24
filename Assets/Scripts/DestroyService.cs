using UnityEngine;

public class DestroyService : MonoBehaviour
{

    private GameObject _entities;

    private GameEntity[] _allGameEntities;
    // Use this for initialization
    void Start()
    {
        _entities = GameObject.Find("Entities");
//        InvokeRepeating("CheckDeadGuys", 1.0f, 1.0f);
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

        var entityToDestroy = GameObject.Find(name);
        // TODO: Später wo anders handeln
        NotifyAllGameEntitiesOfDeath(entityToDestroy.GetComponent<GameEntity>());
        NotifyAllGameEntitiesOfDestroyed(entityToDestroy);

        DestroyImmediate(entityToDestroy);
    }

    public void NotifyAllGameEntitiesOfDestroyed(GameObject go)
    {
        // TODO
    }

    public void NotifyAllGameEntitiesOfDeath(GameEntity entity)
    {
        // TODO
    }

    // Update is called once per frame
    void Update()
    {

    }
}
