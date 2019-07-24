using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

public class ViewRangeDetector : MonoBehaviour
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
        if (coll.transform.CompareTag("Detector"))
        {
            _gameEntity.AiManagerModule.OnEntityEnteredViewRadius(GetTargetEntityFromCollider(coll));
        }
    }

    public void SetVisibility(float visibility)
    {
        GetComponent<CircleCollider2D>().radius = visibility;
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
            _gameEntity.AiManagerModule.OnEntityLeftViewRadius(GetTargetEntityFromCollider(coll));
        }

    }

}
