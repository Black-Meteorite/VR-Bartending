using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class CocktailShakerController : MonoBehaviour
{
    public Vector3 startingPositions;
    public Vector3 endingPositions;
    public float currentDistance;
    public float shakedDistance;


    // Start is called once before the first execution of Update after the MonoBehaviour is created  
    void Start()
    {
        shakedDistance = 10f;
        currentDistance = 0f;
        startingPositions = transform.position;
    }

    // Update is called once per frame  
    void Update()
    {
       
         AddDistancesTraveled();
        
    }

    private void AddDistancesTraveled()
    {
        endingPositions = transform.position;
        currentDistance += Vector3.Distance(startingPositions, endingPositions);
        startingPositions = endingPositions;
        if (currentDistance >= shakedDistance)
        {
            this.GetComponent<dropInCupDetector>().isMixed = true;
        }
        //Debug.Log($"Shaked Distance: {currentDistance}");  
    }
}
