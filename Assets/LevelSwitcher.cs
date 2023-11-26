using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class LevelSwitcher : MonoBehaviour
{
    [SerializeField]
    private GameObject endScreen;

    public int levelToLoad = 1;
    public bool async = true;

    [Header("Async Only")]
    public UnityEvent OnLoadStart;
    public UnityEvent OnLoadComplete;


    private void OnTriggerEnter(Collider other)
    {
        Load();
    }

    private void Load()
    {
        if (levelToLoad == -1)
        {
            Game.EndGame();
            endScreen.active = true;
        }

        if (async)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync("Level" + levelToLoad, LoadSceneMode.Single);
            operation.completed += LoadComplete;
            OnLoadStart.Invoke();
        }
        else
            SceneManager.LoadScene("Level" + levelToLoad, LoadSceneMode.Single);
    }

    private void LoadComplete(AsyncOperation obj)
    {
        OnLoadComplete.Invoke();
    }
}
