using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{

    public GameObject[] ingredients;
    public int ingredientIndex;
    public Vector3 spawnPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ingredientIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("Hand"))
        {
            spawnPosition = transform.position;
            Instantiate(ingredients[ingredientIndex], spawnPosition, transform.rotation);
        }
    }
}
