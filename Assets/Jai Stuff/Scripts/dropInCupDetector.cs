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
    //Should be populated if a cup
    public List<RecipeSO> CupRecipes = new List<RecipeSO>();
    //Should be populated if a shaker
    public List<MixableRecipeSO> ShakerRecipes = new List<MixableRecipeSO>();
    public List<AlcoholSO> mixedAlcoholData = new List<AlcoholSO>();
    public GameObject drop;
    public bool isMixed;
    public bool isStirred;
    public string activeIngredient; 
    public string ingredientUnit;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*ingredients.Add("Gin", 60);
        ingredients.Add("DryVermouth", 30);*/
    
    }


    void Update()
    {
        if (ingredients.Count > 0)
        {
            string CraftedDrink;
            string CraftedIngredient;
            // Crafts drinks only in cups  
            CraftedDrink = this.GetComponent<CraftingManager>().CraftDrink(ingredients, CupRecipes);
            // Crafts iced/mixed drinks  
            CraftedIngredient = this.GetComponent<CraftingManager>().CraftIngredient(ingredients, ShakerRecipes, isMixed);

            //Assigns the drop type once a mixed drink crafted
            if (CraftedIngredient != null)
            {
            
                AlcoholSO alcoholSO = getAlcoholSO(CraftedIngredient);
                if (alcoholSO != null)
                {
                    // Creates a new drop object and assigns the proper alcoholSO. Then stores in titleBottleCode
                    GameObject mixedDrop = Instantiate(drop);
                    this.GetComponent<tiltBottleCode>().dropPrefab = mixedDrop;
                    mixedDrop.GetComponent<AlcoholController>().alcoholData = alcoholSO;
                    mixedDrop.name = CraftedIngredient + "Drop";
                    Debug.Log(mixedDrop.name);
                }
                CraftedIngredient = null;
                // Clears ingredient list once crafted
                ingredients.Clear();
            }

            if (CraftedDrink != null)
            {
                Debug.Log($"CraftedDrink: {CraftedDrink}");
                CraftedDrink = null;
                // Clears ingredient list once crafted
                ingredients.Clear();
            }
        }


    }


    void OnTriggerEnter(Collider collision)
    {
        //Detects ingredient [Drop, Garnish, etc] type is dropped
        if (collision.transform.tag.Equals("Drop"))
        {
            //Obtains the drop data 
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
  
            activeIngredient = alcoholType;

            Destroy(collision.gameObject);
        }

        if(collision.transform.tag.Equals("Garnish"))
        {
            string garnishType = collision.GetComponent<GarnishController>().garnishType.ToString();
            int garnishValue = collision.GetComponent<GarnishController>().garnishValue;
            
            if(!ingredients.ContainsKey(garnishType))
            {
                ingredients.Add(garnishType, garnishValue);
            }
            else
            {
                ingredients[garnishType] += garnishValue;
            }

            activeIngredient = garnishType;

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
            activeIngredient = "Ice";
            Destroy(collision.gameObject);
        }
    }

    //Finds the alcohol type matching the recipe
    private AlcoholSO getAlcoholSO(string CraftedIngredient)
    {
        foreach (AlcoholSO alcoholSO in mixedAlcoholData)
        {
            if(alcoholSO.alcoholType.ToString() == CraftedIngredient)
            {
                return alcoholSO;
            }
        }
        return null;
    }
    
  

}

