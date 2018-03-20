using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMenuManager : MonoBehaviour {

    private int currentObject;
    private bool isMenuEnabled;

    public GameObject ObjectMenu;
    public List<GameObject> ObjectList;
    public List<GameObject> ObjectPrefabList;

	// Use this for initialization
	void Start ()
    {
        currentObject = 0;

        foreach (Transform child in ObjectMenu.transform)
        {
            ObjectList.Add(child.gameObject);
        }
	}

    public void EnableMenu()
    {
        ObjectMenu.SetActive(true);
    }

    public void DisableMenu()
    {
        ObjectMenu.SetActive(false);
    }

    public bool IsMenuEnabled()
    {
        return ObjectMenu.activeSelf;
    }

    public void MenuLeft()
    {
        ObjectList[currentObject].SetActive(false);
        currentObject--;
        if (currentObject < 0)
        {
            currentObject = ObjectList.Count - 1;
        }
        ObjectList[currentObject].SetActive(true);
    }

    public void MenuRight()
    {
        ObjectList[currentObject].SetActive(false);
        currentObject++;
        if (currentObject > ObjectList.Count - 1)
        {
            currentObject = 0;
        }
        ObjectList[currentObject].SetActive(true);
    }

    public void SpawnCurrentObject()
    {
        Instantiate(ObjectPrefabList[currentObject], ObjectList[currentObject].transform.position, ObjectList[currentObject].transform.rotation);
    }
}
