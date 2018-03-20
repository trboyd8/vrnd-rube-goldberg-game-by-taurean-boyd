using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    private LineRenderer laser;
    private RaycastHit hit;
    private RaycastHit groundRay;
    private Vector3 teleportLocation;
    private int laserLength;
    private int groundRayLength;

    public GameObject TeleportObject;
    public GameObject Player;
    public LayerMask laserMask;

	// Use this for initialization
	void Start ()
    {
        laser = this.GetComponentInChildren<LineRenderer> ();
        laserLength = 8;
        groundRayLength = 17;
	}
	
    public void DisplayTeleportMarker()
    {
        // TODO: Maybe adjust the length of the laser as well? By moving the touchpad up or down?
        laser.gameObject.SetActive(true);
        TeleportObject.gameObject.SetActive(true);

        laser.SetPosition(0, this.gameObject.transform.position);
        if (Physics.Raycast(transform.position, transform.forward, out hit, laserLength))
        {
            teleportLocation = hit.point;
            laser.SetPosition(1, teleportLocation);
            TeleportObject.transform.position = new Vector3(teleportLocation.x, teleportLocation.y, teleportLocation.z);
        }
        else
        {
            teleportLocation = new Vector3(transform.forward.x * laserLength + transform.position.x, transform.forward.y * laserLength + transform.position.y, transform.forward.z * laserLength + transform.position.z);
            if (Physics.Raycast(teleportLocation, -Vector3.up, out groundRay, groundRayLength, laserMask))
            {
                teleportLocation = new Vector3(transform.forward.x * laserLength + transform.position.x, groundRay.point.y, transform.forward.z * laserLength + transform.position.z);
            }

            laser.SetPosition(1, transform.forward * laserLength + transform.position);
            TeleportObject.transform.position = teleportLocation;
        }
    }

    public void ClearTeleportMarker()
    {
        laser.gameObject.SetActive(false);
        TeleportObject.gameObject.SetActive(false);
        Player.transform.position = teleportLocation;
    }
}
