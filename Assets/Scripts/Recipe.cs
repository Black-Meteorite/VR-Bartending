using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe")]
public class Recipe : ScriptableObject
{
    public string recipeName;
    public string[] ingredients;
    public string[] Garnish;
    public Sprite drinkImage;
}
