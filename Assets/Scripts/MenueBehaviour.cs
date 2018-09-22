using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenueBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    public void LoadGameScene()
    {
        StartCoroutine(_LoadGameScene());
    }

    private IEnumerator _LoadGameScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("HomeTown");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void LoadOptionsScene()
    {
        Debug.Log("Implentation later...!");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
	// Update is called once per frame
	void Update () {
		
	}
}
