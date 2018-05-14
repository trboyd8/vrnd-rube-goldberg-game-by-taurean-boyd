using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    private LineRenderer laser;
    private RaycastHit hit;
    private RaycastHit groundHit;
    private Vector3 teleportLocation;
    private int laserLength;
    private int groundRayLength;

    public GameObject TeleportObject;
    public GameObject Player;
    public LayerMask activeLayers;

	// Use this for initialization
	void Start ()
    {
        laser = this.GetComponent<LineRenderer>();
        laserLength = 3;
        groundRayLength = 2;
	}
	
    public void DisplayTeleportMarker()
    {
        // Display the laser
        laser.enabled = true;
        TeleportObject.gameObject.SetActive(true);

        laser.SetPosition(0, transform.position);
        laser.SetPosition(1, transform.position + transform.forward * laserLength);
        TeleportObject.transform.position = transform.position + transform.forward * laserLength;
        teleportLocation = Player.transform.position;

        // Test if we teleport to something
        if (Physics.Raycast(transform.position, transform.forward, out hit, laserLength, activeLayers))
        {
            laser.SetPosition(1, hit.point);
            TeleportObject.transform.position = hit.point;
            teleportLocation = hit.point;
        }
        else
        {
            // Do a second test if we can hit something below where we are pointing
            Vector3 potentialEndpoint = transform.position + transform.forward * laserLength;
            if (Physics.Raycast(potentialEndpoint, Vector3.down, out groundHit, groundRayLength, activeLayers))
            {
                laser.SetPosition(1, groundHit.point);
                TeleportObject.transform.position = groundHit.point;
                teleportLocation = groundHit.point;
            }
        }
    }

    public void ClearTeleportMarker()
    {
        laser.enabled = false;
        TeleportObject.gameObject.SetActive(false);
        Player.transform.position = teleportLocation;
    }
}
