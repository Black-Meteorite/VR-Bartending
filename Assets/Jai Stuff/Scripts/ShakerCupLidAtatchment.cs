using UnityEngine;
using UnityEngine.InputSystem;

public class ShakerCupSnap : MonoBehaviour
{
    public Transform lid;
    public Transform snapPoint;

    private Rigidbody lidRb;
    private bool lidSnapped = false;

    void Start()
    {
        lidRb = lid.GetComponent<Rigidbody>();
    }

    void Update()
{
    if (lidSnapped && Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
    {
        lid.SetParent(null);
        lidRb.isKinematic = false;
        lidRb.useGravity = true;
        lidSnapped = false;

        // 🔼 Teleport the lid 5 meters upward
        lid.position += Vector3.up * 2f;

        Debug.Log("Lid released and teleported up.");
    }
}

    private void OnTriggerEnter(Collider other)
    {
        if (!lidSnapped && other.gameObject == lid.gameObject)
        {
            lid.position = snapPoint.position;
            lid.rotation = snapPoint.rotation;
            lid.SetParent(transform);

            lidRb.isKinematic = true;
            lidRb.useGravity = false;

            lidSnapped = true;
            Debug.Log("Lid snapped.");
        }
    }
}