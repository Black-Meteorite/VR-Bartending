using UnityEngine;

public class tiltBottleCode : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public Material dropColor;
    public GameObject dropPrefab;

    //How fast drops flow
    public float dropRate = 0.5f;

    //Top offset
    public float dropOffset = 0.2f;

    private float lastDropTime = 0f;
    void Update()
    {
        Vector3 rotation = transform.eulerAngles;

        // Convert 0-360° to -180° to 180°
        rotation.x = (rotation.x > 180) ? rotation.x - 360 : rotation.x;
        rotation.y = (rotation.y > 180) ? rotation.y - 360 : rotation.y;
        rotation.z = (rotation.z > 180) ? rotation.z - 360 : rotation.z;
        
        //Debug.Log(rotation.y);

        if(rotation.x > 0){
            Debug.Log("POURING!");
            if (Time.time > lastDropTime + dropRate)
            {
                lastDropTime = Time.time;
                SpawnDrop();
            }
        }
    }


    public Transform dropSpawnPoint;
    void SpawnDrop()
    {
        // Creates drop clone and sets location to invisible drop point on bottle object
        GameObject drop = Instantiate(dropPrefab, dropSpawnPoint.position, Quaternion.identity);
        drop.transform.rotation = Quaternion.Euler(-90, 0, 0);

        // Apply color if needed
        if (dropColor != null)
        {
            Renderer dropRenderer = drop.GetComponent<Renderer>();
            if (dropRenderer != null)
            {
                dropRenderer.material = dropColor;
            }
        }

        Destroy(drop, 3f);
    }
}
