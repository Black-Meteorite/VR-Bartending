using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using static AlcoholSO;
using System;
using static UnityEngine.EventSystems.EventTrigger;

public class CraftingManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<RecipeSO> recipes = new List<RecipeSO>();

    private Dictionary<string, float> ingredients = new Dictionary<string, float>();
    void Start()
    {
        /*ingredients.Add("Strawberry", 2);
      ingredients.Add("Soju", 3);
      ingredients.Add("Sprite", 3);
      ingredients.Add("Calpico", 4);
      //ingredients.Add("isMixed", 1);*/
      ingredients = (Dictionary<string, float>)this.GetComponent<dropInCupDetection>().ingredients;
      CraftDrink(ingredients, recipes);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CraftDrink(Dictionary<string, float> ingredients, List<RecipeSO> recipes)
    {
        //Looks through all the recipes
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
        //Compares current ingredient with current recipe
        if (currentIngredients.Count != recipe.ingredients.Count)
        {
            Debug.Log("count not match");
            return false;
        }

        foreach (var ingredient in recipe.ingredients)
        {
            if (!currentIngredients.ContainsKey(ingredient.ingredientName) ||
                currentIngredients[ingredient.ingredientName] != ingredient.amount)
            {
                Debug.Log("missing an ingredient or amount");
                return false; //Missing ingredient or incorrect amount
            }
        }

        return true;
    }

}
