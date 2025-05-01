using UnityEngine;

public class IceTraySpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject iceprefab;
    public int numberofCubes = 20;
    public Vector2 traySize = new Vector2(2f,2f);
    void Start()
    {
        SpawnIce();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnIce(){
        for ( int i = 0; i < numberofCubes; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            Instantiate(iceprefab,randomPosition,Quaternion.identity,transform);
        }
    
    }

    Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(-traySize.x /2f, traySize.x/2f);
        float randomZ = Random.Range(-traySize.y /2f, traySize.y/2f);

        return transform.position + new Vector3(randomX,0f,randomZ);
    }
}
