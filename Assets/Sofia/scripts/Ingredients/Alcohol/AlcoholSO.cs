using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AlcoholSO", menuName = "IngredientsSO/AlcoholSO")]
public class AlcoholSO : ScriptableObject
{
    public enum AlcoholType
    {
        MangoSoju,
        Whiskey,
        Vodka,
        Wine,
        DryVermouth,
        Gin, 
        OrangeJuice,
        Tequila,
        CranberryJuice,
        LemonSoda,
        GrapefruitSoda,
        Martini,
        TequilaSunrise

    }

    public AlcoholType alcoholType;
    public Material dropColor;
    public int dropValue;
  
 
}
