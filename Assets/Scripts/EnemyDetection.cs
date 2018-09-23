using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDetection : MonoBehaviour {
    public class Target
    {
        public Vector3 Position { get; set; }

        public string Name { get; set; }
    }
    public Target _nearstTarget;
    public string NearstTargetName;
    public List<Target> _targetCollisions = new List<Target>();
    private CharacterMovement _characterMovement;
    private bool _newTarget = false;

    void Start()
    {
        _characterMovement = GameObject.Find("Fred").GetComponent<CharacterMovement>();
    }

    void OnTriggerEnter2D(Collider2D collidor)
    {
        if (collidor.name == "UnitDetector")
        {
            Debug.Log("Gegner gesichtet! position: "+collidor.transform.position);
            _targetCollisions.Add(new Target { Position = collidor.transform.position, Name = collidor.transform.name });
            _newTarget = true;
        }
    }

    void Update()
    {
        if (_targetCollisions.Count != 0 && _characterMovement.CharacterState == CharacterMovement.State.Run && _newTarget)
        {
            _characterMovement.RunToTarget(TargetNearstEnemy());
        }
    }

    private float TargetNearstEnemy()
    {
        float actualNearstContactDistance = 0.0f;
        Target actualNearstContact = new Target();
        foreach (var targetCollision in _targetCollisions)
        {
            if (actualNearstContactDistance == 0.0f)
            {
                actualNearstContact = targetCollision;
            }
            else if (Vector3.Distance(GetComponentInParent<Transform>().position, targetCollision.Position) < actualNearstContactDistance && _nearstTarget != targetCollision)
            {
                actualNearstContact = targetCollision;
            }
        }
        _nearstTarget = actualNearstContact;
        _newTarget = false;
        //GetComponentInParent<Image>().color = new Color(35,35,35);
        NearstTargetName = actualNearstContact.Name +" position: "+actualNearstContact.Position;
        return actualNearstContact.Position.x;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("Trigger exit by: "+col.transform.name);
            RemoveTargetByName(col.transform.name);
    }

    private void RemoveTargetByName(string name)
    {
        Target obsoletTarget = null;
        foreach (var targetCollision in _targetCollisions)
        {
            if (targetCollision.Name == name)
            {
                obsoletTarget = targetCollision;
            }
        }

        if (obsoletTarget != null)
        {
            _targetCollisions.Remove(obsoletTarget);
        }
    }
}
