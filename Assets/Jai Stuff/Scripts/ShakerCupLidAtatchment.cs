using UnityEngine;


public class ShakerCupSnap : MonoBehaviour
{
    public Transform lid;
    public Transform snapPoint;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable lidGrab;
    private Rigidbody lidRb;

    private bool lidSnapped = false;

    void Start()
    {
        if (lid != null)
        {
            lidGrab = lid.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
            lidRb = lid.GetComponent<Rigidbody>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!lidSnapped && other.transform == lid)
        {
            lidOn();
        }
    }

    void lidOn()
    {
        lid.position = snapPoint.position;
        lid.rotation = snapPoint.rotation;

        lidRb.isKinematic = true;
        lidRb.useGravity = false;

        lidGrab.enabled = false;

        lidSnapped = true;

        lid.SetParent(transform);
    }

    public void ReleaseLid()
    {
        lidRb.isKinematic = false;
        lidRb.useGravity = true;
        lidGrab.enabled = true;
        lidSnapped = false;
        lid.SetParent(null);
    }
}