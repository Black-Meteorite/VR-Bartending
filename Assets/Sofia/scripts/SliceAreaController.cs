using System;
using UnityEngine;

public class SliceAreaController : MonoBehaviour
{
    public GameObject slicedLemon;
    public GameObject parent;
    private Vector3 currentPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.tag.Equals("Blade"))
        {

            currentPosition = transform.position;
            Instantiate(slicedLemon, currentPosition, transform.rotation);
            Destroy(parent);
        }
    }
}
