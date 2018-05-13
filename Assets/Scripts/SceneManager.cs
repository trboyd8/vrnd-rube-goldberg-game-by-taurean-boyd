using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {

    public CollectableManager collectableManager;
    public GameObject completeUI;
    private bool isLevelComplete;

    public void ResetLevel()
    {
        collectableManager.EnableAllCollectables();
    }

    public void LevelComplete()
    {
        this.isLevelComplete = true;
        completeUI.SetActive(true);
    }

    public bool IsLevelComplete()
    {
        return this.isLevelComplete;
    }

    public void LoadNextLevel()
    {
        Debug.Log("Loading the next level.");
    }
}
