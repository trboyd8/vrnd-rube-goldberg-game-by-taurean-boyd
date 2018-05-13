using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {

    public CollectableManager collectableManager;

    public void ResetLevel()
    {
        collectableManager.EnableAllCollectables();
    }

    public void LevelComplete()
    {
        Debug.Log("Level complete.");
        // TODO: Add code to display a level complete menu. User clicks a button to continue.
    }

    public void LoadNextLevel()
    {
        Debug.Log("Loading the next level.");
    }
}
