using System;
using UnityEngine;

public class AlcoholController : MonoBehaviour
{
    public AlcoholSO alcoholData;
    public float dropValue;
    public AlcoholSO.AlcoholType alcoholType;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.GetComponent<MeshRenderer>().material = alcoholData.dropColor;
        dropValue = alcoholData.dropValue;
        alcoholType = alcoholData.alcoholType;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
