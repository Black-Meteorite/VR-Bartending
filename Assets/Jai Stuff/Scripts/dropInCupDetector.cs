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
    public string CraftedDrink;
    public string CraftedIngredient;
    public GameObject drop;
    int dropCount;
    public bool isMixed;
    public bool isStirred;
    public activeIngredient currentActiveIngredient;

    public bool canRegisterDrop = true;
    public float delayBeforeRegistering = 1.0f; // Delay in seconds

    public struct activeIngredient
    {
        public string name;
        public string type;
    }

    public string ingredientUnit;

    private void Awake()
    {
        // Sample ingredients
        ingredients.Add("MangoSoju", new IngredientData(50, "Alcohol"));
        
        ingredients.Add("LemonSoda", new IngredientData(100, "Alcohol"));
        ingredients.Add("Lemon", new IngredientData(1, "Garnish"));
        ingredients.Add("Ice", new IngredientData(1, "Ice"));
    }
    void Start()
    {
        
    }

    void Update()
    {
        //Calls the CraftingManager
        if (ingredients.Count > 0)
        {
       

            CraftedDrink = FindFirstObjectByType<CraftingManager>().CraftDrink(ingredients, CupRecipes);
            CraftedIngredient = FindAnyObjectByType<CraftingManager>().CraftIngredient(ingredients, ShakerRecipes);

            if (CraftedIngredient != null)
            {
                this.GetComponent<CocktailShakerController>().canShake = true;
                //Prints alcohol crafted
                AlcoholSO alcoholSO = getAlcoholSO(CraftedIngredient);
                if (alcoholSO != null && isMixed)
                {
                    GameObject mixedDrop = Instantiate(drop);
                    this.GetComponent<tiltBottleCode>().dropPrefab = mixedDrop;
                    mixedDrop.GetComponent<AlcoholController>().alcoholData = alcoholSO;
                    mixedDrop.name = CraftedIngredient + "Drop";
                    Debug.Log(mixedDrop.name);

                    ingredients.Clear();
                    CraftedIngredient = null;
                   // this.GetComponent<CocktailShakerController>().currentDistance = 0;
                    this.GetComponent<CocktailShakerController>().canShake = false;
                }
           
                //Resets the shakers distance
                
                

                
            }

            if (CraftedDrink != null)
            {
                //Debug.Log($"CraftedDrink: {CraftedDrink}");
                //Resets to no drink crafted by cup
     
                this.GetComponent<StirringDetector>().canStir = true;

                //Checks if the recipe needs stirring
                bool recipeIsStirred = GetRecipeSO(CraftedDrink).isStirred;
                if (isStirred == recipeIsStirred)
                {
                    //End of crafting drink sequence
                    FindFirstObjectByType<DrinkManager>().SpawnDrink(CraftedDrink);
                    ingredients.Clear();
                    CraftedDrink = null;

                    //Resets Stirring distance
                    //this.GetComponent<StirringDetector>().currentDistance = 0;
                    this.GetComponent<StirringDetector>().canStir = false;
                }

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

        //RESETS THE CUP
        if (collision.transform.tag.Equals("PURGE"))
        {
            ingredients.Clear();
            if (this.GetComponent<CocktailShakerController>())
            {
                //Resets the shakers distance and removes drop abilities
                this.GetComponent<CocktailShakerController>().currentDistance = 0;
                this.GetComponent<CocktailShakerController>().canShake = false;
                this.GetComponent<tiltBottleCode>().dropPrefab = null;
            }

            if (this.GetComponent<StirringDetector>())
            {
                //Resets Stirring distance
                this.GetComponent<StirringDetector>().currentDistance = 0;
                this.GetComponent<StirringDetector>().canStir = false;
            }

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

    private RecipeSO GetRecipeSO(string CraftedRecipe)
    {
        foreach(RecipeSO recipe in CupRecipes)
        {
            if(recipe.recipeName.ToString().Equals(CraftedRecipe))
            {
                return recipe;
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