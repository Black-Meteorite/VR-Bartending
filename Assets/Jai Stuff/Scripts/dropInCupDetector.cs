using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    List<string> listOfDrops = new List<string>();
    HashSet<string> uniqueDrops = new HashSet<string>();


    void Update()
    {
        //Debug.Log(listOfDrops.Count);

        // foreach (string drop in uniqueDrops){
        //     int count = listOfDrops.Count(name => name == drop);
        //     Debug.Log(drop + " appears " + count + " times");
        // }
    }

    
    void OnTriggerEnter(Collider collision)
    {
        //
        //Debug.Log("Collided with: " + collision.gameObject.name);
        if (collision.transform.tag.Equals("Drop"))
        {
            listOfDrops.Add(collision.gameObject.name);
            // uniqueDrops.Add(collision.gameObject.name);
            Destroy(collision.gameObject);
        }
    }
}
