using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System;

public class RecipeManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;

    public List<RecipeSO> recipes;
    public List<MixableRecipeSO> shakerRecipes;
    public GameObject recipeText;
    public GameObject recipeHeader;


    private void Awake()
    {
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

   

    void Update()
    {
        
    }

    public void ShowRecipe()
    {
        int entryIndex = dropdown.value;
        string recipeName = dropdown.options[entryIndex].text;
        RecipeSO recipe = Array.Find<RecipeSO>(recipes.ToArray(), recipe => recipe.recipeName == recipeName);
        MixableRecipeSO shakerRecipe = Array.Find<MixableRecipeSO>(shakerRecipes.ToArray(), recipe => recipe.resultIngredient.ingredientName == recipeName);
        recipeHeader.GetComponent<TextMeshProUGUI>().text = recipeName;
        recipeText.GetComponent<TextMeshProUGUI>().text = " ";

        if(recipe == null && shakerRecipe == null)
        {

            Debug.LogError("Recipe not found: " + recipeName);
            recipeText.GetComponent<TextMeshProUGUI>().text = "Recipe not found.";
            return;
        }
        if (shakerRecipe != null)
        {
            recipeText.GetComponent<TextMeshProUGUI>().text += "Drop into the Shaker: \n";
            foreach (var shakeIngredient in shakerRecipe.ingredients)
            {
                string units = shakeIngredient.type switch
                {
                    "Alcohol" => "ML",
                    "Garnish" => "Slice",
                    "Ice" => "Portion",
                    _ => ""
                };
                recipeText.GetComponent<TextMeshProUGUI>().text += $"{shakeIngredient.ingredientName}: {shakeIngredient.amount} {units}\n";
            }
        };

        recipeText.GetComponent<TextMeshProUGUI>().text += "\n Then shake. \n \n";

        //List all the recipes for cup
        recipeText.GetComponent<TextMeshProUGUI>().text += "Drop into the Cup: \n";
        foreach (var ingredient in recipe.ingredients)
        {
            string units = ingredient.type switch
            {
                "Alcohol" => "ML",
                "Garnish" => "Slice",
                "Ice" => "Portion",
                _ => ""
            };
            recipeText.GetComponent<TextMeshProUGUI>().text += $"{ingredient.ingredientName}: {ingredient.amount} {units}\n";
        }
    }  
}
