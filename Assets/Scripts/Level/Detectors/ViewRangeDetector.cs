using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

public class GameEntityDetectionService : MonoBehaviour
{

    private GameEntity _gameEntity;

    [UsedImplicitly]
    void Start()
    {
        _gameEntity = GetComponentInParent<GameEntity>();
    }

    [UsedImplicitly]
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.transform.tag == "Detector")
        {
            _gameEntity.AI.OnEntityEnteredViewRadius(GetTargetEntityFromCollider(coll));
        }
    }
    public void SetVisibility(float Visibility)
    {
        GetComponent<CircleCollider2D>().radius = Visibility;
    }
    
    private GameEntity GetTargetEntityFromCollider(Collider2D coll)
    {
        return coll.GetComponentInParent<GameEntity>();
    }

    [UsedImplicitly]
    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.transform.tag == "Detector")
        {
            _gameEntity.AI.OnEntityLeftViewRadius(GetTargetEntityFromCollider(coll));
        }

    }

}
