using Assets.Scripts.Services;
using UnityEngine;

public class Init : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        var service = DatabaseService.GetService();
        service.Initialize();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
