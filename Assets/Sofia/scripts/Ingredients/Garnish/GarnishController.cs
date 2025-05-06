using UnityEngine;

public class GarnishController : MonoBehaviour
{
    public GarnishSO garnishData;
    public int garnishValue;
    public GarnishSO.GarnishType garnishType;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        garnishValue = garnishData.garnishValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
