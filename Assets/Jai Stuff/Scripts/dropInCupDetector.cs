using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using static AlcoholSO;
using System;
using static UnityEngine.EventSystems.EventTrigger;

public class dropInCupDetector : MonoBehaviour
{



    public Dictionary<string, float> ingredients = new Dictionary<string, float>();
    //Should be populated if a cup
    public List<RecipeSO> CupRecipes = new List<RecipeSO>();
    //Should be populated if a shaker
    public List<MixableRecipeSO> ShakerRecipes = new List<MixableRecipeSO>();
    public List<AlcoholSO> mixedAlcoholData = new List<AlcoholSO>();
    public GameObject drop;
    public bool isMixed;
    public bool isStirred;
    public string activeIngredient; 



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
            string MixedDrink;
            // Crafts drinks only in cups  
            CraftedDrink = this.GetComponent<CraftingManager>().CraftDrink(ingredients, CupRecipes);
            // Crafts iced/mixed drinks  
            MixedDrink = this.GetComponent<CraftingManager>().CraftDrink(ingredients, ShakerRecipes, isMixed);

            //Assigns the drop type once a mixed drink crafted
            if (MixedDrink != null)
            {
            
                AlcoholSO alcoholSO = getAlcoholSO(MixedDrink);
                if (alcoholSO != null)
                {
                    // Creates a new drop object and assigns the proper alcoholSO. Then stores in titleBottleCode
                    GameObject mixedDrop = Instantiate(drop);
                    this.GetComponent<tiltBottleCode>().dropPrefab = mixedDrop;
                    mixedDrop.GetComponent<AlcoholController>().alcoholData = alcoholSO;
                    mixedDrop.name = MixedDrink + "Drop";
                    Debug.Log(mixedDrop);
                }
                MixedDrink = null;
                // Clears ingredient list  
                ingredients.Clear();
            }

            if (CraftedDrink != null)
            {
                Debug.Log($"CraftedDrink: {CraftedDrink}");
                CraftedDrink = null;
                // Clears ingredient list  
                ingredients.Clear();
            }
        }


    }


    void OnTriggerEnter(Collider collision)
    {
        //Detects ingredient [Drop, Fruit, etc] type is dropped
        if (collision.transform.tag.Equals("Drop"))
        {
            //Obtains the drop data 
            string alcoholType = collision.GetComponent<AlcoholController>().alcoholType.ToString();
            float dropValue = collision.GetComponent<AlcoholController>().dropValue;

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
    }

    //Finds the alcohol type matching the recipe
    private AlcoholSO getAlcoholSO(string mixedDrink)
    {
        foreach (AlcoholSO alcoholSO in mixedAlcoholData)
        {
            if(alcoholSO.alcoholType.ToString() == mixedDrink)
            {
                return alcoholSO;
            }
        }
        return null;
    }
  

}

