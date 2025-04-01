using UnityEngine;
using System.Collections;
using System;

public class CocktailShakerController : MonoBehaviour
{
    public Vector3 startingPositions;
    public Vector3 endingPositions;
    public float currentDistance;
    public float shakedDistance;
    public bool isShaked;
    
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
        //Debug.Log($"Shaked Distance: {currentDistance}");
    }


}
