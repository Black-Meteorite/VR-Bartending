using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class IngredientsInCupUI : MonoBehaviour
{
    public GameObject ingredientsText;
    public GameObject cupDetectionArea;

    // Updated dictionary to store both amount and type
    public Dictionary<string, IngredientData> listedIngredients = new Dictionary<string, IngredientData>();

    // Struct to hold ingredient amount and type
    public struct IngredientData
    {
        public int amount;
        public string type;

        public IngredientData(int amount, string type)
        {
            this.amount = amount;
            this.type = type;
        }
    }

    private string activeIngredient;

    void Start()
    {
        var detector = cupDetectionArea.GetComponent<dropInCupDetector>();
        if (detector == null)
        {
            return;
        }

        foreach (var item in detector.ingredients)
        {
            string units = item.Value.type switch
            {
                "Alcohol" => "ML",
                "Garnish" => "Slice",
                "Ice" => "Portion",
                _ => ""
            };

            ingredientsText.GetComponent<TextMeshProUGUI>().text += $"{item.Key}: {item.Value.amount} {units}\n";
        }
    }

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

        // Clears ingredients listed in UI if no ingredients are present
        if (detector.ingredients.Count <= 0)
        {
            ingredientsText.GetComponent<TextMeshProUGUI>().text = "";
            listedIngredients.Clear();
            return;
        }

        activeIngredient = detector.currentActiveIngredient.name;
        string activeIngredientType = detector.currentActiveIngredient.type;

        // If the ingredient is not in the list or its amount has changed
        if (!string.IsNullOrEmpty(activeIngredient) && detector.ingredients.ContainsKey(activeIngredient))
        {
            ingredientsText.GetComponent<TextMeshProUGUI>().text = "";

            // Adds or updates the ingredient in the list
            if (!listedIngredients.ContainsKey(activeIngredient))
            {
                listedIngredients.Add(activeIngredient, new IngredientData(detector.ingredients[activeIngredient].amount, activeIngredientType));
            }
            else
            {
                listedIngredients[activeIngredient] = new IngredientData(detector.ingredients[activeIngredient].amount, activeIngredientType);
            }

            // Updates the UI with the ingredient list
            foreach (var item in listedIngredients)
            {
                string units = item.Value.type switch
                {
                    "Alcohol" => "ML",
                    "Garnish" => "Slice",
                    "Ice" => "Portion",
                    _ => ""
                };

                ingredientsText.GetComponent<TextMeshProUGUI>().text += $"{item.Key}: {item.Value.amount} {units}\n";
            }
        }
    }
}
