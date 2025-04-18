using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using static AlcoholSO;
using System;
using static UnityEngine.EventSystems.EventTrigger;
using System.Runtime.CompilerServices;

public class CraftingManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created



    void Start()
    {
     

        /*ingredients.Add("Strawberry", 2);
      ingredients.Add("Soju", 3);
      ingredients.Add("Sprite", 3);
      ingredients.Add("Calpico", 4);
      //ingredients.Add("isMixed", 1);*/

    }

    // Update is called once per frame
    void Update()
    {   
      
       
    }
    //Craft Drinks for non mixable
    public string CraftDrink(Dictionary<string, float> ingredients, List<RecipeSO> recipes)
    {
        bool craftedDrink;
        //Looks through all the recipes
        foreach (var recipe in recipes)
        {
            craftedDrink = isMatchingRecipe(ingredients, recipe);
            if (craftedDrink)
            {
                return recipe.recipeName;
            }
        }
        return null;
    }
    public string CraftDrink(Dictionary<string, float> ingredients, List<MixableRecipeSO> recipes, bool isMixed)
    {
        bool mixedDrink;
        // Looks through all the recipes  
        foreach (var recipe in recipes)
        {
            mixedDrink = isMatchingRecipe(ingredients, recipe);
            if (mixedDrink && isMixed)
            {
                // Debug.Log($"Mixed: {recipe.resultIngredient.ingredientName} ");  
                return recipe.resultIngredient.ingredientName;
            }
        }

        // Ensure all code paths return a value  
        return null;
    }
    private bool isMatchingRecipe(Dictionary<string, float> currentIngredients, RecipeSO recipe)
    {
        //Compares current ingredient with current recipe
        if (currentIngredients.Count != recipe.ingredients.Count)
        {
            //Debug.Log("count not match");
            return false;
        }

        foreach (var ingredient in recipe.ingredients)
        {
            if (!currentIngredients.ContainsKey(ingredient.ingredientName) ||
                currentIngredients[ingredient.ingredientName] != ingredient.amount)
            {
               // Debug.Log("missing an ingredient or amount");
                return false; //Missing ingredient or incorrect amount
            }
        }

        return true;
    }

    private bool isMatchingRecipe(Dictionary<string, float> currentIngredients, MixableRecipeSO recipe)
    {
        //Compares current ingredient with current recipe
        if (currentIngredients.Count != recipe.ingredients.Count)
        {
            //Debug.Log("count not match");
            return false;
        }

        foreach (var ingredient in recipe.ingredients)
        {
            if (!currentIngredients.ContainsKey(ingredient.ingredientName) ||
                currentIngredients[ingredient.ingredientName] != ingredient.amount)
            {
               // Debug.Log("missing an ingredient or amount");
                return false; //Missing ingredient or incorrect amount
            }
        }

        return true;
    }

}
