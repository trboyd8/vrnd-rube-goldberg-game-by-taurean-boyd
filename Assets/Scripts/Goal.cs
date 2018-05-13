using UnityEngine;

public class Goal : MonoBehaviour {

    public CollectableManager collectableManager;
    public SceneManager sceneManager;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Throwable"))
        {
            if (collectableManager.AnyActiveCollectables())
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
