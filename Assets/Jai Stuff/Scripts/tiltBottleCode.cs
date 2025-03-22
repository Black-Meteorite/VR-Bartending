using UnityEngine;

public class tiltBottleCode : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public Material dropColor;
    public GameObject dropPrefab;

    public string alchoholType;

    public Transform dropSpawnPoint;

    //How fast drops flow
    public float dropRate = 0.5f;

    //Top offset
    public float dropOffset = 0.2f;

    private float lastDropTime = 0f;
    void Update()
    {
        float tiltAmount = Vector3.Angle(dropSpawnPoint.up, Vector3.up);

        Debug.Log("Tilt Amount: " + tiltAmount);

        if (tiltAmount > 90f)
        {
            //Debug.Log("POURING!");
            if (Time.time > lastDropTime + dropRate)
            {
                if ((tiltAmount / 180) > 1){
                    dropRate = 0.1f;
                }else{
                    dropRate = 1 - (tiltAmount / 180);
                }
                
                lastDropTime = Time.time;
                SpawnDrop();
            }
        }
    }


    
    void SpawnDrop()
    {
        GameObject drop = Instantiate(dropPrefab, dropSpawnPoint.position, Quaternion.identity);
        
        if (alchoholType != ""){
            drop.name = alchoholType + "Drop";
        }else{
            drop.name = "alchoholDrop";
        }

        drop.transform.rotation = Quaternion.Euler(-90, 0, 0);

        if (dropColor != null)
        {
            Renderer dropRenderer = drop.GetComponent<Renderer>();
            if (dropRenderer != null)
            {
                dropRenderer.material = dropColor;
            }
        }

        // Collider dropCollider = drop.GetComponent<Collider>();
        // Collider[] allDrops = FindObjectsOfType<Collider>();
        // foreach (Collider other in allDrops)
        // {
        //     if (other.gameObject.name.Contains("Drop"))
        //     {
        //         Physics.IgnoreCollision(dropCollider, other);
        //     }
        // }

        // Destroys drop after x seconds
        Destroy(drop, 3f);
    }
}
