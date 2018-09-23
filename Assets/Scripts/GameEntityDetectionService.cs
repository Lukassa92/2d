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
        if (coll.tag == "Detector")
        {
            //            Debug.Log("Game Entity gesichtet! position: "+coll.transform.position);
            _gameEntity.AI.OnEntityEnteredViewRadius(GetTargetEntityFromCollider(coll));
        }
    }

    private TargetEntity GetTargetEntityFromCollider(Collider2D coll)
    {
        var gameEntity = coll.GetComponentInParent<GameEntity>();
        var targetEntity = new TargetEntity
        {
            LevelEntity = gameEntity.LevelEntity,
            GameEntity = gameEntity
        };
        return targetEntity;
    }

    [UsedImplicitly]
    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Detector")
        {
            _gameEntity.AI.OnEntityLeftViewRadius(GetTargetEntityFromCollider(coll));
        }

    }


    [UsedImplicitly]
    void Update()
    {
        if (_targetCollisions.Count > 0 && _gameEntity.GetState() == States.State.Run)
        {
            TargetNearstEnemy();
        }
    }
}
