using UnityEngine;
using UnityEngine.UI;

public class ShakerProgressBar : MonoBehaviour
{
    public GameObject fillBar;
    public GameObject shaker;

    public Image fillImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fillImage = fillBar.GetComponent<Image>();
        fillImage.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float progress = Mathf.Clamp01((shaker.GetComponent<CocktailShakerController>().currentDistance / shaker.GetComponent<CocktailShakerController>().shakedDistance));
        fillImage.fillAmount = progress;
    }
}
