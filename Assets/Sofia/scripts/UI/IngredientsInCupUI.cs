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
    public Dictionary<string, float> listedIngredients = new Dictionary<string, float>();


    private float amount;
    private string activeIngredient;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var detector = cupDetectionArea.GetComponent<dropInCupDetector>();
        activeIngredient = detector.activeIngredient;
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
                ingredientsText.GetComponent<TextMeshProUGUI>().text += item.Key + ": " + item.Value + "ML\n";
            }


        }

    }

    // Update is called once per frame
    void Update()
    {
        var detector = cupDetectionArea.GetComponent<dropInCupDetector>();
        activeIngredient = detector.activeIngredient;
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
                ingredientsText.GetComponent<TextMeshProUGUI>().text += item.Key + ": " + item.Value + "ML\n";
            }


        }

       
        

        
    }
}
