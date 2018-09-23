using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class GameEntityDetectionService : MonoBehaviour
{

    private GameEntity _gameEntity;

    private List<GameTarget> _targetCollisions = new List<GameTarget>();

    [UsedImplicitly]
    void Start()
    {
        _gameEntity = GetComponentInParent<GameEntity>();
    }

    [UsedImplicitly]
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Detector" && coll.GetComponentInParent<Transform>().tag != _gameEntity.GetComponentInParent<Transform>().tag)
        {
//            Debug.Log("Game Entity gesichtet! position: "+coll.transform.position);
            _targetCollisions.Add(new GameTarget(){Name = coll.transform.name, Position = coll.transform.position, Tag = coll.GetComponentInParent<Transform>().tag});
        }
    }

    [UsedImplicitly]
    void OnTriggerExit2D(Collider2D col)
    {
//        Debug.Log("Trigger exit by: "+col.transform.name);
          RemoveTargetByName(col.transform.name);
    }

    public void SetVisibility(float Visibility)
    {
        GetComponent<CircleCollider2D>().radius = Visibility;
    }
    public void SetHitRange(float HitRange)
    {
        GetComponent<CircleCollider2D>(). radius = HitRange;
    }
    private void TargetNearstEnemy()
    {
        float actualNearstContactDistance = 0.0f;
        GameTarget actualNearstContact = null;
        foreach (var targetCollision in _targetCollisions)
        {
            if (actualNearstContactDistance == 0.0f)
            {
                actualNearstContact = targetCollision;
            }
            else if (Vector3.Distance(GetComponentInParent<Transform>().position, targetCollision.Position) < actualNearstContactDistance)
            {
                actualNearstContact = targetCollision;
            }
        }

        if (_gameEntity.GetNewTarget() != actualNearstContact)
        {
            _gameEntity.SwitchTargetHasChanged();
            _gameEntity.SetNewTarget(actualNearstContact);
        }
        else
        {
            _gameEntity.SetNewTarget(actualNearstContact);
        }
    }

    private void RemoveTargetByName(string name)
    {
        GameTarget obsoleteTarget = null;
        foreach (var targetCollision in _targetCollisions)
        {
            if (targetCollision.Name == name)
            {
                obsoleteTarget = targetCollision;
            }
        }

        if (obsoleteTarget != null)
        {
            _targetCollisions.Remove(obsoleteTarget);
            if (_targetCollisions.Count == 0)
            {
                _gameEntity.SwitchTargetHasChanged();
                _gameEntity.SetNewTarget(null);
            }
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
