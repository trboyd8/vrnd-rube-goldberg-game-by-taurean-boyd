using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour {

    // This class does 2 things. One: It will handle the collision with the ball and provide a force (to simulate a fan).
    // Two: It will animate the fan blades
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
