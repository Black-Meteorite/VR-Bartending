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
    int dropCount;
    public bool isMixed;
    public bool isStirred;
    public activeIngredient currentActiveIngredient;

    public bool canRegisterDrop = true;
    public float delayBeforeRegistering = 0.5f; // Delay in seconds

    public struct activeIngredient
    {
        public string name;
        public string type;
    }

    public string ingredientUnit;

    void Start()
    {
        // Sample ingredients
        ingredients.Add("Tequila", new IngredientData(50, "Alcohol"));
        ingredients.Add("CranberryJuice", new IngredientData(10, "CranberryJuice"));
        ingredients.Add("OrangeJuice", new IngredientData(100, "Alcohol"));
        ingredients.Add("Ice", new IngredientData(1, "Ice"));
    }

    void Update()
    {
        //Calls the CraftingManager
        if (ingredients.Count > 0)
        {
            string CraftedDrink;
            string CraftedIngredient;

            CraftedDrink = FindFirstObjectByType<CraftingManager>().CraftDrink(ingredients, CupRecipes, isStirred);
            CraftedIngredient = FindAnyObjectByType<CraftingManager>().CraftIngredient(ingredients, ShakerRecipes, isMixed);

            if (CraftedIngredient != null)
            {
                //Prints alcohol crafted
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
                //Resets the shakers distance
                this.GetComponent<CocktailShakerController>().currentDistance = 0;
                this.GetComponent<CocktailShakerController>().canShake = false;
                ingredients.Clear();
            }

            if (CraftedDrink != null)
            {
                Debug.Log($"CraftedDrink: {CraftedDrink}");
                //Resets to no drink crafted by cup
                CraftedDrink = null;
                
                //Resets Stirring distance
                //this.GetComponent<StirringDetector>().currentDistance = 0;
                //this.GetComponent<StirringDetector>().canStir = true;

                ingredients.Clear();
            }
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        // If the timer is active, ignore the drop
        if (!canRegisterDrop && collision.transform.tag.Equals("Drop"))
        {
            Destroy(collision.gameObject);
            return;
        }

        // Start the delay timer
        StartCoroutine(DelayBeforeNextDrop(collision));

        //If lid on then no pouring allow
        if (collision.transform.tag.Equals("Lid"))
        {
            return;
        }

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

    void OnTriggerExit(Collider collision)
    {
        if (collision.transform.tag.Equals("Drop"))
        {
            
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

    // Coroutine to handle the delay before registering the next drop
    private IEnumerator DelayBeforeNextDrop(Collider collision)
    {
        canRegisterDrop = false; // Disable drop registration
        yield return new WaitForSeconds(delayBeforeRegistering); // Wait for the delay
        canRegisterDrop = true; // Re-enable drop registration
    }
}