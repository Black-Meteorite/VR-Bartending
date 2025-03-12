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
        
        //Debug.Log(rotation.x);

        if(rotation.x > 0){
            //Debug.Log("POURING!");
            if (Time.time > lastDropTime + dropRate)
            {
                if ((rotation.x / 30) > 1){
                    dropRate = 0.1f;
                }else{
                    dropRate = 1 - (rotation.x / 30);
                }
                
                lastDropTime = Time.time;
                SpawnDrop();
            }
        }
    }


    public Transform dropSpawnPoint;
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
