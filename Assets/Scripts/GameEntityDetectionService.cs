using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEntityDetectionService : MonoBehaviour
{

    private GameEntityBehaviour _gameEntityBehaviour;

    private List<GameTarget> _targetCollisions = new List<GameTarget>();

    void Start()
    {
        _gameEntityBehaviour = GetComponentInParent<GameEntityBehaviour>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Detector" && coll.GetComponentInParent<Transform>().tag != _gameEntityBehaviour.GetComponentInParent<Transform>().tag)
        {
//            Debug.Log("Game Entity gesichtet! position: "+coll.transform.position);
            _targetCollisions.Add(new GameTarget(){Name = coll.transform.name, Position = coll.transform.position, Tag = coll.GetComponentInParent<Transform>().tag});
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("Trigger exit by: "+col.transform.name);
            RemoveTargetByName(col.transform.name);
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

        if (_gameEntityBehaviour.GetNewTarget() != actualNearstContact)
        {
            _gameEntityBehaviour.SwitchTargetHasChanged();
            _gameEntityBehaviour.SetNewTarget(actualNearstContact);
        }
        else
        {
            _gameEntityBehaviour.SetNewTarget(actualNearstContact);
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

    void Update()
    {
        if (_targetCollisions.Count > 0)
        {
            TargetNearstEnemy();
        }
    }
}
