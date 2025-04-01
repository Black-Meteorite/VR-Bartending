using UnityEngine;

public class StirringDetector : MonoBehaviour
{
    public float currentDistance;
    public float stirredDistance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stirredDistance = 4f;
        currentDistance = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider collision)
    {

        if (collision.transform.tag.Equals("Spoon"))
        {
            Debug.Log("current stirring distance:" + currentDistance);
 
            currentDistance += Vector3.Distance(transform.position, collision.transform.position);


        }
    }
}
