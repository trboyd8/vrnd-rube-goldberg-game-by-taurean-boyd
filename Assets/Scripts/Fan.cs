using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour {

    private GameObject fanBlades;
    
	// Use this for initialization
	void Start () {
        this.fanBlades = this.gameObject.transform.GetChild(0).gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        this.fanBlades.transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), 4.0f);
	}
}
