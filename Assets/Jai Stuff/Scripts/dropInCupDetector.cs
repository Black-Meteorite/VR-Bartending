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



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ingredients.Add("Strawberry", 2);
        ingredients.Add("Soju", 3);
        ingredients.Add("Sprite", 3);
        ingredients.Add("Calpico", 4);
        ingredients.Add("isMixed", 1);
    }


    void Update()
    {


        foreach (var item in ingredients)
        {
            Debug.Log($"Key: {item.Key}, dropAmount: {item.Value}");
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
            bool isMixed = collision.GetComponent<AlcoholController>().isMixed;
            if (!ingredients.ContainsKey(alcoholType))
            {
                ingredients.Add(alcoholType, dropValue);
            }
            else
            {
                ingredients[alcoholType] += dropValue;
                
            }
            Destroy(collision.gameObject);
        }
    }

  

}

