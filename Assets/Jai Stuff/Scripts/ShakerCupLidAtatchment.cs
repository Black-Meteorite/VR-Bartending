using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

public class ShakerCupSnap : MonoBehaviour
{
    public Transform lid;
    public Transform snapPoint;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable lidGrab;
    private Rigidbody lidRb;
    private bool lidSnapped = false;

    private InputDevice rightHandDevice;

    void Start()
    {
        if (lid != null)
        {
            lidGrab = lid.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
            lidRb = lid.GetComponent<Rigidbody>();
            lidGrab.selectEntered.AddListener(OnLidGrabbed);
        }

        TryInitializeRightHandDevice();
    }

    void Update()
    {
        // Reacquire if controller is not valid
        if (!rightHandDevice.isValid)
            TryInitializeRightHandDevice();

        if (lidSnapped && rightHandDevice.isValid)
        {
            if (rightHandDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed) && triggerPressed)
            {
                ReleaseLid();
            }
        }
    }

    private void TryInitializeRightHandDevice()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, devices);
        if (devices.Count > 0)
            rightHandDevice = devices[0];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!lidSnapped && other.gameObject == lid.gameObject)
        {
            SnapLid();
        }
    }

    private void SnapLid()
    {
        lid.position = snapPoint.position;
        lid.rotation = snapPoint.rotation;

        lidRb.isKinematic = true;
        lidRb.useGravity = false;
        lidGrab.enabled = false;

        lid.SetParent(transform);
        lidSnapped = true;
    }

    private void ReleaseLid()
    {
        lid.SetParent(null);
        lidRb.isKinematic = false;
        lidRb.useGravity = true;
        lidGrab.enabled = true;
        lidSnapped = false;
    }

    private void OnLidGrabbed(SelectEnterEventArgs args)
    {
        if (lidSnapped)
            ReleaseLid();
    }
}