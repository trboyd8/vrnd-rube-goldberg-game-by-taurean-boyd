﻿using System.Collections;
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
        Debug.Log(col.gameObject.tag);

        if (col.gameObject.CompareTag ("Ground"))
        {
            this.gameObject.transform.position = originalPosition;
            Rigidbody ballRigidBody = this.GetComponent<Rigidbody> ();
            ballRigidBody.velocity = Vector3.zero;
            ballRigidBody.angularVelocity = Vector3.zero;
        }
        
        if (col.gameObject.CompareTag("Collectable"))
        {
            col.gameObject.SetActive(false);
            // Need to notify something that the collectable is collected.
        }
    }
}
