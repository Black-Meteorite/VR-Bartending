using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AlcoholSO", menuName = "IngredientsSO/AlcoholSO")]
public class AlcoholSO : ScriptableObject
{
    public enum AlcoholType
    {
        Soju,
        Whiskey,
        Vodka,
        Wine
    }

    public AlcoholType alcoholType;
    public Material dropColor;
    public float dropValue;
 
}
