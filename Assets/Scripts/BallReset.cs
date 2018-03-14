using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReset : MonoBehaviour
{
    private Vector3 originalPosition;

	// Use this for initialization
	void Start ()
    {
        originalPosition = this.gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    // react on collisions
    void OnCollisionEnter (Collision col)
    {
        if (col.gameObject.CompareTag ("Ground"))
        {
            Debug.Log("The ball is being reset.");
            this.gameObject.transform.position = originalPosition;
        }
    }
}
