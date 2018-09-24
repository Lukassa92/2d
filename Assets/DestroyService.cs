using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyService : MonoBehaviour
{

    private GameObject _entities;
	// Use this for initialization
	void Start () {
		_entities = GameObject.Find("Entities");
        InvokeRepeating("CheckDeadGuys", 1.0f, 1.0f);
	}

    public void CheckDeadGuys()
    {
        foreach (var componentsInChild in _entities.GetComponentsInChildren<GameEntity>())
        {
            if (!componentsInChild.IsAlive)
            {
                Debug.Log("Destroy: "+ componentsInChild.GetComponentInParent<Transform>().name);
                DestroyGameObjectByName(componentsInChild.GetComponentInParent<Transform>().name);
            }
        }
    }
    public void DestroyGameObjectByName(string name)
    {
        DestroyImmediate(GameObject.Find(name));
    }
	// Update is called once per frame
	void Update () {
		
	}
}
