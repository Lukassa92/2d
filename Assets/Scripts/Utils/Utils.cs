using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public GameEntity[] GetAllGameEntities()
    {
        var entityGroup = GameObject.Find("entities");
        var allGameEntities = entityGroup.GetComponentsInChildren<GameEntity>();
        return allGameEntities;
    }
}
