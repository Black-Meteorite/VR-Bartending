using System;
using UnityEngine;

public class DrinkManager : MonoBehaviour
{
    public Drink[] Drinks;

    public static DrinkManager instance;

    public Vector3 spawnPosition;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void SpawnDrink(string name)
    {
        // Finds the drink from the list  
        Drink drink = Array.Find(Drinks, drink => drink.name == name);

        // Spawns the game object  
        GameObject drinkPrefab = Instantiate(drink.drink, spawnPosition, Quaternion.identity);

        
        Transform liquidTransform = drinkPrefab.transform.Find("Liquid");
        if (liquidTransform != null)
        {
            GameObject liquid = liquidTransform.gameObject;
            //liquid.GetComponent<Material>().material = drink.
            // Additional logic for 'liquid' can be added here if needed  
        }
    }
}
