using UnityEngine;

[CreateAssetMenu(fileName = "GarnishSO", menuName = "IngredientSO/GarnishSO")]
public class GarnishSO : ScriptableObject
{
    public enum GarnishType
    {
        Lemon,
        Mint
    }

    public GarnishType garnishType;
    public int garnishValue;
 
}
