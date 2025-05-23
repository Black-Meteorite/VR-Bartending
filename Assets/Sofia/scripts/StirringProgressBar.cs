using UnityEngine;
using UnityEngine.UI; 

public class StirringProgressBar : MonoBehaviour
{
    public GameObject fillBar;
    public GameObject cupDetector;

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
        float progress = Mathf.Clamp01((cupDetector.GetComponent<StirringDetector>().currentDistance / cupDetector.GetComponent<StirringDetector>().stirredDistance));
        fillImage.fillAmount = progress;
    }
}
