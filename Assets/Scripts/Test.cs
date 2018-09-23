using Assets.Scripts.Services;
using UnityEngine;

public class Test : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        var databaseService = GameObject.Find("ScriptObject").GetComponent<DatabaseService>();
        databaseService.Initialize();
        var repo = databaseService.GetRepository<Player>();
        var player = new Player
        {
            Email = "Hallo@Mail.de",
            Password = "Test123",
            Username = "Horst"
        };
        repo.Create(player);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
