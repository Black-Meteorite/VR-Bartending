using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using static AlcoholSO;
using TMPro;
using System;
using static UnityEngine.EventSystems.EventTrigger;

public class IngredientsInCupUI : MonoBehaviour
{
    public GameObject ingredientsText;
    public GameObject cupDetectionArea;
    public Dictionary<string, int> listedIngredients = new Dictionary<string, int>();


    private int amount;
    private string activeIngredient;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateIngredientsDisplayed();

    }

    // Update is called once per frame
    void Update()
    {
      
        UpdateIngredientsDisplayed();
        
    }


    void UpdateIngredientsDisplayed()
    {
        
        var detector = cupDetectionArea.GetComponent<dropInCupDetector>();
        if (detector == null)
        {
            return;
        }
        activeIngredient = detector.currentActiveIngredient.name;
        string activeIngredientType = detector.currentActiveIngredient.type;
        //If the ingredient is not in the list and active ingredient's amount does not equal the listed amount
        if (!string.IsNullOrEmpty(activeIngredient) && detector.ingredients.ContainsKey(activeIngredient))
        {
            

            ingredientsText.GetComponent<TextMeshProUGUI>().text = "";
            //Adds ingredient to the list
            if (!listedIngredients.ContainsKey(activeIngredient))
            {
                listedIngredients.Add(activeIngredient, detector.ingredients[activeIngredient]);
            }
            else //Updates ingredient amount
            {
                listedIngredients[activeIngredient] = detector.ingredients[activeIngredient];
            }
            foreach (var item in listedIngredients)
            {
                string units = "";
                //Determines units
                if (activeIngredientType == "Alcohol")
                {
                    units = "ML";
                }
                else if (activeIngredientType == "Garnish")
                {
                    units = "Slice";
                }
                else if (activeIngredientType == "Ice")
                {
                    units = "Portion";
                }
                ingredientsText.GetComponent<TextMeshProUGUI>().text += item.Key + ": " + item.Value + " " + units + "\n";
            }


        }

    }
}
