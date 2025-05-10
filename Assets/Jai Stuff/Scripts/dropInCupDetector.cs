using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using static AlcoholSO;
using System;
using static UnityEngine.EventSystems.EventTrigger;
using static GarnishSO;

public class dropInCupDetector : MonoBehaviour
{
    // Define a struct to store ingredient amount and type
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

    // Update the dictionary to use IngredientData as the value
    public Dictionary<string, IngredientData> ingredients = new Dictionary<string, IngredientData>();
    public List<RecipeSO> CupRecipes = new List<RecipeSO>();
    public List<MixableRecipeSO> ShakerRecipes = new List<MixableRecipeSO>();
    public List<AlcoholSO> mixedAlcoholData = new List<AlcoholSO>();
    public GameObject drop;
    public bool isMixed;
    public bool isStirred;
    public activeIngredient currentActiveIngredient;

    public struct activeIngredient
    {
        public string name;
        public string type;
    }

    public string ingredientUnit;

    void Start()
    {
        // Sample ingredients
        /*ingredients.Add("DryVermouth", new IngredientData(30, "Alcohol"));
        ingredients.Add("Gin", new IngredientData(60, "Alcohol"));
        ingredients.Add("Ice", new IngredientData(1, "Ice"));*/
    }

    void Update()
    {
        if (ingredients.Count > 0)
        {
            string CraftedDrink;
            string CraftedIngredient;

            CraftedDrink = FindFirstObjectByType<CraftingManager>().CraftDrink(ingredients, CupRecipes, isStirred);
            CraftedIngredient = FindAnyObjectByType<CraftingManager>().CraftIngredient(ingredients, ShakerRecipes, isMixed);

            if (CraftedIngredient != null)
            {
                AlcoholSO alcoholSO = getAlcoholSO(CraftedIngredient);
                if (alcoholSO != null)
                {
                    GameObject mixedDrop = Instantiate(drop);
                    this.GetComponent<tiltBottleCode>().dropPrefab = mixedDrop;
                    mixedDrop.GetComponent<AlcoholController>().alcoholData = alcoholSO;
                    mixedDrop.name = CraftedIngredient + "Drop";
                    Debug.Log(mixedDrop.name);
                }
                CraftedIngredient = null;
                ingredients.Clear();
            }

            if (CraftedDrink != null)
            {
                Debug.Log($"CraftedDrink: {CraftedDrink}");
                CraftedDrink = null;
                ingredients.Clear();
            }
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag.Equals("Drop"))
        {
            string alcoholType = collision.GetComponent<AlcoholController>().alcoholType.ToString();
            int dropValue = collision.GetComponent<AlcoholController>().dropValue;
            //Increments current amount if found
            if (!ingredients.ContainsKey(alcoholType))
            {
                ingredients.Add(alcoholType, new IngredientData(dropValue, "Alcohol"));
            }
            else
            {
                var existingData = ingredients[alcoholType];
                ingredients[alcoholType] = new IngredientData(existingData.amount + dropValue, "Alcohol");
            }

            currentActiveIngredient.name = alcoholType;
            currentActiveIngredient.type = "Alcohol";

            Destroy(collision.gameObject);
        }

        if (collision.transform.tag.Equals("Garnish"))
        {
            string garnishType = collision.GetComponent<GarnishController>().garnishType.ToString();
            int garnishValue = collision.GetComponent<GarnishController>().garnishValue;

            if (!ingredients.ContainsKey(garnishType))
            {
                ingredients.Add(garnishType, new IngredientData(garnishValue, "Garnish"));
            }
            else
            {
                var existingData = ingredients[garnishType];
                ingredients[garnishType] = new IngredientData(existingData.amount + garnishValue, "Garnish");
            }

            currentActiveIngredient.name = garnishType;
            currentActiveIngredient.type = "Garnish";

            Destroy(collision.gameObject);
        }

        if (collision.transform.tag.Equals("Ice"))
        {
            FindFirstObjectByType<AudioManager>().Play("Scoop Ice");
            if (!ingredients.ContainsKey("Ice"))
            {
                ingredients.Add("Ice", new IngredientData(1, "Ice"));
            }
            else
            {
                var existingData = ingredients["Ice"];
                ingredients["Ice"] = new IngredientData(existingData.amount + 1, "Ice");
            }
            currentActiveIngredient.name = "Ice";
            currentActiveIngredient.type = "Ice";
            collision.gameObject.SetActive(false);
        }

        if (collision.transform.tag.Equals("PURGE"))
        {
            ingredients.Clear();

            Destroy(collision.gameObject);
        }
    }

    private AlcoholSO getAlcoholSO(string CraftedIngredient)
    {
        foreach (AlcoholSO alcoholSO in mixedAlcoholData)
        {
            if (alcoholSO.alcoholType.ToString() == CraftedIngredient)
            {
                return alcoholSO;
            }
        }
        return null;
    }
}