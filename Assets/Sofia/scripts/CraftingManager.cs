using System.Collections.Generic;
using UnityEngine;
using static AlcoholSO;

public class CraftingManager : MonoBehaviour
{
    void Start()
    {
        // Initialization logic if needed
    }

    void Update()
    {
        // Update logic if needed
    }

    // Craft Drinks for non-mixable recipes
    public string CraftDrink(Dictionary<string, dropInCupDetector.IngredientData> ingredients, List<RecipeSO> recipes)
    {
        bool craftedDrink = false;

        // Looks through all the recipes
        foreach (var recipe in recipes)
        {
            //Determines if drink needs to be stirred
            craftedDrink = isMatchingRecipe(ingredients, recipe);
            
           
            if (craftedDrink)
            {
                // Spawns drink
               
                
                return recipe.recipeName;
            }
        }
        return null;
    }

    // Craft Ingredients for mixable recipes
    public string CraftIngredient(Dictionary<string, dropInCupDetector.IngredientData> ingredients, List<MixableRecipeSO> recipes)
    {
        bool mixedDrink;

        // Looks through all the recipes
        foreach (var recipe in recipes)
        {
            mixedDrink = isMatchingRecipe(ingredients, recipe);
            if (mixedDrink)
            {
                return recipe.resultIngredient.ingredientName;
            }
        }

        // Ensure all code paths return a value
        return null;
    }

    private bool isMatchingRecipe(Dictionary<string, dropInCupDetector.IngredientData> currentIngredients, RecipeSO recipe)
    {
       

        // Compares current ingredients with the recipe
        if (currentIngredients.Count != recipe.ingredients.Count)
        {
            return false;
        }

        foreach (var ingredient in recipe.ingredients)
        {
            if (!currentIngredients.ContainsKey(ingredient.ingredientName) ||
                currentIngredients[ingredient.ingredientName].amount != ingredient.amount)
            {
                return false; // Missing ingredient or incorrect amount
            }
        }

        return true;
    }

    private bool isMatchingRecipe(Dictionary<string, dropInCupDetector.IngredientData> currentIngredients, MixableRecipeSO recipe)
    {
        // Compares current ingredients with the recipe
        if (currentIngredients.Count != recipe.ingredients.Count)
        {
            return false;
        }

        foreach (var ingredient in recipe.ingredients)
        {
            if (!currentIngredients.ContainsKey(ingredient.ingredientName) ||
                currentIngredients[ingredient.ingredientName].amount != ingredient.amount)
            {
                return false; // Missing ingredient or incorrect amount
            }
        }

        return true;
    }


}
