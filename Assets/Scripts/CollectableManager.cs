using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour {

    private List<GameObject> collectables;

    private void Start()
    {
        collectables = new List<GameObject>();

        foreach (Transform child in this.transform)
        {
            collectables.Add(child.gameObject);
        }
    }

    public bool AnyActiveCollectables()
    {
        foreach (GameObject collectable in collectables)
        {
            if (collectable.activeSelf)
                return true;
        }

        return false;
    }

    public void EnableAllCollectables()
    {
        foreach (GameObject collectable in collectables)
        {
            collectable.SetActive(true);
        }
    }
}
