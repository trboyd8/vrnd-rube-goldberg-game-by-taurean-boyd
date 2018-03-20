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

    public bool isLeftHand;
    public ObjectMenuManager objectMenuManager;
    public PlayerMovementManager playerMovementManager;

    // Use this for initialization
    void Start()
    {
        trackedObject = this.GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update()
    {
        device = SteamVR_Controller.Input((int)trackedObject.index);

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            if (!isLeftHand)
            {
                objectMenuManager.EnableMenu();
                touchLast = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x;

            }
        }

        if (device.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            if (isLeftHand)
            {
               playerMovementManager.DisplayTeleportMarker();
            }
            else
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

        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            if (isLeftHand)
            {
                playerMovementManager.ClearTeleportMarker();
            }
            else
            {
                objectMenuManager.DisableMenu();
                swipeSum = 0;
                touchCurrent = 0;
                touchLast = 0;
                hasSwipedLeft = false;
                hasSwipedRight = false;
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
}
