using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using static AlcoholSO;
using TMPro;
using System;
using static UnityEngine.EventSystems.EventTrigger;

public class FillProgressBar : MonoBehaviour
{
    public GameObject measurementDisplay;
    public GameObject cupDetectionArea;
    
    private float amount;
    private string activeIngredient;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
        //Checks if an ingredient is actively being used
        var detector = cupDetectionArea.GetComponent<dropInCupDetector>();
        if (detector.activeIngredient != null && detector.ingredients.ContainsKey(detector.activeIngredient))
        {
            activeIngredient = detector.activeIngredient;
            amount = detector.ingredients[activeIngredient];

            measurementDisplay.GetComponent<TextMeshProUGUI>().text = activeIngredient + ": " + amount + "ML";
        }
    }
}
