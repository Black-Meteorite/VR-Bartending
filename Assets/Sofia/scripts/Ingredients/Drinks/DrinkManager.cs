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
        FindFirstObjectByType<AudioManager>().Play("Success");
        // Finds the drink from the list  
        Drink drink = Array.Find(Drinks, drink => drink.name == name);

        // Spawns the game object  
        GameObject drinkPrefab = Instantiate(drink.drink, spawnPosition, Quaternion.Euler(-90, 0, 0));
        //Finds children to customize drink
        GameObject liquid = drinkPrefab.transform.Find("Liquid").gameObject;
        GameObject straw = drinkPrefab.transform.Find("Straw").gameObject;
        GameObject lemon = drinkPrefab.transform.Find("Lemon").gameObject;
        if (liquid != null)
        {
            
            Renderer renderer = liquid.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = drink.liquidColor;
            }
        }
        //Makes straw disappear 
        if (straw != null && !drink.hasStraw)
        {
            straw.SetActive(false);
        }

        //Makes lemon disappear 
        if (lemon != null && !drink.hasLemon)
        {
            lemon.SetActive(false);
        }

    }
}
