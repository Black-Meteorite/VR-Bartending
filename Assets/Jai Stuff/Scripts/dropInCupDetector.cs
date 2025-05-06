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
    public Dictionary<string, int> ingredients = new Dictionary<string, int>();
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
        ingredients.Add("Strawberry", 2);
        ingredients.Add("Soju", 3);
        ingredients.Add("Sprite", 3);
        ingredients.Add("Calpico", 4);
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

            if (!ingredients.ContainsKey(alcoholType))
            {
                ingredients.Add(alcoholType, dropValue);
            }
            else
            {
                ingredients[alcoholType] += dropValue;
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
                ingredients.Add(garnishType, garnishValue);
            }
            else
            {
                ingredients[garnishType] += garnishValue;
            }

            currentActiveIngredient.name = garnishType;
            currentActiveIngredient.type = "Garnish";

            Destroy(collision.gameObject);
        }

        if (collision.transform.tag.Equals("Ice"))
        {
            if (!ingredients.ContainsKey("Ice"))
            {
                ingredients.Add("Ice", 1);
            }
            else
            {
                ingredients["Ice"] += 1;
            }
            currentActiveIngredient.name = "Ice";
            currentActiveIngredient.type = "Ice";
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

