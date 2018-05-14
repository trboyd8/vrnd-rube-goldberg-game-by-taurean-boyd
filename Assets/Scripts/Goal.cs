using UnityEngine;

public class Goal : MonoBehaviour {

    public CollectableManager collectableManager;
    public SceneManager sceneManager;
    public static bool CanCompleteLevel = true;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Throwable"))
        {
            if (collectableManager.AnyActiveCollectables() || !Goal.CanCompleteLevel)
            {
                sceneManager.ResetLevel();
            }
            else
            {
                col.gameObject.SetActive(false);
                sceneManager.LevelComplete();
            }
        }
    }
}
