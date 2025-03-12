using UnityEngine;
using System.Collections.Generic;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    List<string> listOfDrops = new List<string>();
    
    void Update()
    {
        Debug.Log(listOfDrops.Count);
    }

    
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name);
        listOfDrops.Add(collision.gameObject.name);
        Destroy(collision.gameObject);
    }
}
