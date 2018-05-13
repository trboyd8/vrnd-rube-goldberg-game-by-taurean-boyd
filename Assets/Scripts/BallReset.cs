using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReset : MonoBehaviour
{
    private Vector3 originalPosition;
    private Color originalColor;

    public SceneManager sceneManager;

	// Use this for initialization
	void Start ()
    {
        originalPosition = this.gameObject.transform.position;
        originalColor = this.gameObject.GetComponent<Renderer>().material.color;
	}

    // react on collisions
    void OnCollisionEnter (Collision col)
    {
        if (col.gameObject.CompareTag ("Ground"))
        {
            this.gameObject.transform.position = originalPosition;
            Rigidbody ballRigidBody = this.GetComponent<Rigidbody> ();
            ballRigidBody.velocity = Vector3.zero;
            ballRigidBody.angularVelocity = Vector3.zero;

            sceneManager.ResetLevel();
            this.gameObject.GetComponent<Renderer>().material.color = originalColor;
        }
    }

    public void ColorBall()
    {
        this.gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    // disable collectables on collision
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Collectable"))
        {
            col.gameObject.SetActive(false);
        }
    }
}
