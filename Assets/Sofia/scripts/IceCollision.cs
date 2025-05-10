using UnityEngine;

public class IceCollision : MonoBehaviour
{
    public GameObject Ice;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag.Equals("Ice Box"))
        {
            FindFirstObjectByType<AudioManager>().Play("Scoop Ice");
            Ice.SetActive(true);
        }
    }
}
