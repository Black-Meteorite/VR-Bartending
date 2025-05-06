using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Recipe/MixableRecipeSO")]
public class MixableRecipeSO : ScriptableObject
{
    public List<Ingredient> ingredients;
    public Ingredient resultIngredient; // e.g. MixedMartini
}
