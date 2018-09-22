using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collidor)
    {
        if (collidor.name == "UnitDetector")
        {
            Debug.Log("Gegner gesichtet!");
        }
    }
	
}
