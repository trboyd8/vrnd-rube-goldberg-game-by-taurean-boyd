using UnityEngine;

public class SceneManager : MonoBehaviour {

    public CollectableManager collectableManager;
    public GameObject completeUI;
    private bool isLevelComplete;
    public string nextLevelName;

    public void ResetLevel()
    {
        collectableManager.EnableAllCollectables();
        Goal.CanCompleteLevel = true;
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
        SteamVR_LoadLevel.Begin(nextLevelName);
    }
}
