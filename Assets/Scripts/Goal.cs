using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

    public GameObject collectables;
    public SceneManager sceneManager;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Throwable"))
        {
            bool foundActiveCollectable = false;
            foreach (Transform child in collectables.transform)
            {
                if (child.gameObject.activeSelf)
                {
                    foundActiveCollectable = true;
                    break;
                }
            }

            if (foundActiveCollectable)
            {
                sceneManager.ResetLevel();
            }
            else
            {
                sceneManager.LoadNextLevel();
            }
        }
    }
}
