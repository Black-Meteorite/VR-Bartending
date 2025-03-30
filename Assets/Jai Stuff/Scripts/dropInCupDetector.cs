using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using static AlcoholSO;
using System;
using static UnityEngine.EventSystems.EventTrigger;

public class dropInCupDetector : MonoBehaviour
{
    /*public class IngredientData
    {
        public float dropAmount;
        public bool isMixed;
    }*/


    private Dictionary<string, float> ingredients = new Dictionary<string, float>();
    public List<RecipeSO> recipes = new List<RecipeSO>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ingredients.Add("Strawberry", 2);
        ingredients.Add("Soju", 3);
        ingredients.Add("Sprite", 3);
        ingredients.Add("Calpico", 4);
        //ingredients.Add("isMixed", 1);

        CraftDrink(ingredients, recipes);
    }


    void Update()
    {


        /*foreach (var item in ingredients)
        {
            Debug.Log($"Key: {item.Key}, dropAmount: {item.Value}");
        }*/
    }


    void OnTriggerEnter(Collider collision)
    {
 
        if (collision.transform.tag.Equals("Drop"))
        {

            string alcoholType = collision.GetComponent<AlcoholController>().alcoholType.ToString();
            float dropValue = collision.GetComponent<AlcoholController>().dropValue;
            bool isMixed = collision.GetComponent<AlcoholController>().isMixed;
            if (!ingredients.ContainsKey(alcoholType))
            {
                ingredients.Add(alcoholType, dropValue);
            }
            else
            {
                ingredients[alcoholType] += dropValue;
                
            }
            Destroy(collision.gameObject);
        }
    }

    public void CraftDrink(Dictionary<string, float> ingredients, List<RecipeSO> recipes)
    {
        foreach (var recipe in recipes)
        {
            if (isMatchingRecipe(ingredients, recipe))
            {
                Debug.Log($"Crafted: {recipe.recipeName} ");
            }
        }
    }

    private bool isMatchingRecipe(Dictionary<string, float> currentIngredients, RecipeSO recipe)
    {
        if(currentIngredients.Count != recipe.ingredients.Count)
        {
            Debug.Log("count not match");
            return false; 
        }
        
        foreach(var ingredient in recipe.ingredients)
        {
            if(!currentIngredients.ContainsKey(ingredient.ingredientName) ||
                currentIngredients[ingredient.ingredientName] != ingredient.amount)
            {
                Debug.Log("missing an ingredient or amount");
                return false; //Missing ingredient or incorrect amount
            }
        }

        return true;
    }


}

