using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeManager : MonoBehaviour
{
    public List<Recipe> recipes;
    public TMP_Text recipeTitle;
    public TMP_Text ingredientsText;
    public TMP_Text garnishText;
    public Image drinkImage;
    private int currentIndex= 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShowRecipe(currentIndex);
    }

    public void ShowRecipe (int index){
        if(recipes.Count == 0 ) return;
        recipeTitle.text = recipes[index].recipeName;
        ingredientsText.text = string.Join("\n" , recipes[index].ingredients);
        garnishText.text = string.Join("\n" , recipes[index].Garnish);
        drinkImage.sprite = recipes[index].drinkImage;
    }

    public void NextRecipe(){
        currentIndex = (currentIndex+1 ) % recipes.Count;
        ShowRecipe(currentIndex);
    }

    public void PreviousRecipe(){
        currentIndex = (currentIndex -1 +recipes.Count) % recipes.Count ;
        ShowRecipe(currentIndex);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
