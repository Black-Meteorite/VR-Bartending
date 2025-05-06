
using System;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu( menuName = "RecipeSO/RecipeSO")]
public class RecipeSO : ScriptableObject
{
    public string recipeName;
    public List<Ingredient> ingredients;
    public bool isMixed;
    public bool isStirred;
}
