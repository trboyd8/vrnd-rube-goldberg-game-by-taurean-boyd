using UnityEngine;
using System.Collections;

public class ControllerInputManager : MonoBehaviour
{
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;
    private float swipeSum;
    private float touchLast;
    private float touchCurrent;
    private float distance;
    private bool hasSwipedLeft;
    private bool hasSwipedRight;
    private float throwForce = 1.5f;

    public bool isLeftHand;
    public ObjectMenuManager objectMenuManager;
    public PlayerMovementManager playerMovementManager;
    public GameObject platform;

    // Use this for initialization
    void Start()
    {
        trackedObject = this.GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update()
    {
        device = SteamVR_Controller.Input((int)trackedObject.index);

        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            if (!isLeftHand)
            {
                objectMenuManager.EnableMenu();
                touchLast = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x;
            }
        }

        if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
        {
            if (!isLeftHand)
            {
                // TODO: This resets when I move the figure back to the middle
                touchCurrent = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x;
                distance = touchCurrent - touchLast;
                touchLast = touchCurrent;
                swipeSum += distance;

                if (!hasSwipedRight)
                { 
                    if (swipeSum > 0.5f)
                    {
                        swipeSum = 0;
                        objectMenuManager.MenuRight();
                        hasSwipedRight = true;
                        hasSwipedLeft = false;
                    }
                }

                if (!hasSwipedLeft)
                {
                    if (swipeSum < -0.5f)
                    {
                        swipeSum = 0;
                        objectMenuManager.MenuLeft();
                        hasSwipedLeft = true;
                        hasSwipedRight = false;
                    }
                }
            }
        }

        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            if (!isLeftHand)
            {
                objectMenuManager.DisableMenu();
                swipeSum = 0;
                touchCurrent = 0;
                touchLast = 0;
                hasSwipedLeft = false;
                hasSwipedRight = false;
            }
        }

        if (device.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            if (isLeftHand)
            {
                playerMovementManager.DisplayTeleportMarker();
            }
        }

        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            if (isLeftHand)
            {
                playerMovementManager.ClearTeleportMarker();
            }
        }

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            if (!isLeftHand && objectMenuManager.IsMenuEnabled())
            {
                objectMenuManager.SpawnCurrentObject();
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Throwable"))
        {
            if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                // Check if the position of the 'other' object is outside of the platform bounds
                if (!IsWithinBounds(other.transform.position))
                {
                    other.gameObject.GetComponent<BallReset>().ColorBall();
                }
                
                ThrowObject(other);
            }
            else if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                GrabObject(other);
            }
        }

        if (other.gameObject.CompareTag("Structure"))
        {
            if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                LeaveObject(other);
            }
            else if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                GrabObject(other);
            }
        }
    }

    private void ThrowObject(Collider otherObject)
    {
        otherObject.transform.SetParent(null);
        Rigidbody rigidBody = otherObject.GetComponent<Rigidbody>();
        rigidBody.isKinematic = false;
        rigidBody.velocity = device.velocity * throwForce;
        rigidBody.angularVelocity = device.angularVelocity;
    }

    private void GrabObject(Collider otherObject)
    {
        otherObject.transform.SetParent(gameObject.transform);
        otherObject.GetComponent<Rigidbody>().isKinematic = true;
        device.TriggerHapticPulse(2000);
    }

    private void LeaveObject(Collider otherObject)
    {
        otherObject.transform.SetParent(null);
        Rigidbody rigidBody = otherObject.GetComponent<Rigidbody>();
        rigidBody.isKinematic = true;
    }

    private bool IsWithinBounds(Vector3 position)
    {
        Collider platformCollider = platform.GetComponent<Collider>();
        if (platformCollider.bounds.min.x <= position.x && platformCollider.bounds.max.x >= position.x)
            if (platformCollider.bounds.min.z <= position.z && platformCollider.bounds.max.z >= position.z)
                return true;

        return false;
    }
}
