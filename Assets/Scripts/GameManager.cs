using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // keep track of on what level currently in
    // load and unload level
    // keep track of game state
    // generate other persistent method


    // keeping track of count of async operation
    List<AsyncOperation> loadOperations;


    private string currentLevelName = string.Empty;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        loadOperations = new List<AsyncOperation>();
        LoadLevel("Level1");
    }
    // create load and unload method 
    public void LoadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName,LoadSceneMode.Additive);
        if (ao == null)
        {
            Debug.LogError("[GameManager] unable to load scene named" + levelName);
            return;
        }
        loadOperations.Add(ao);
        ao.completed += OnLoadOperationCompleted;
        currentLevelName = levelName;
    }

    public void UnloadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);
        if (ao == null)
        {
            Debug.LogError("[GameManager] unable to unload scene named" + levelName);
            return;
        }
        ao.completed += OnUnloadOperationCompleted;
    }

    private void OnUnloadOperationCompleted(AsyncOperation obj)
    {
        Debug.Log("Unload Completed");
    }

    private void OnLoadOperationCompleted(AsyncOperation ao)
    {
        if (loadOperations.Contains(ao))
        {
            loadOperations.Remove(ao);
            // dispatch message
            // transition between scene

               
        }
        Debug.Log("Load Completed");
    }
    
}
