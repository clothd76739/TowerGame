using UnityEngine;
using UnityEngine.UI;

public class HealthControl : MonoBehaviour
{
    // HealthBar
    public Slider mainSlider;
    // NowHealth
    private float health;
    // after sub Health 
    private float resultHealth;
    // sub Health
    private bool HPdown = false;

    // Start is called before the first frame update
    public void Start()
    {
        // set maxHealth & minHealth & nowHealth
        mainSlider.maxValue = 100;
        mainSlider.minValue = 0;
        
        mainSlider.value = mainSlider.maxValue ;
        health = mainSlider.value;
        resultHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        // HP-
        if (HPdown)
        {
            resultHealth = mainSlider.value - 10 < mainSlider.minValue ? mainSlider.minValue : mainSlider.value - 10;
            HPdown = false;
        }
        // Lerp ( HP- )
        health = Mathf.Lerp(health, resultHealth, 0.05f);
        mainSlider.value = health;

        // control slider of Fill Area
        if (mainSlider.value <= 0.01)
            mainSlider.transform.GetChild(1).localScale = Vector3.zero;
        else
            mainSlider.transform.GetChild(1).localScale = Vector3.one;

    }
    // control HP-
    public void ReduceHealth()
    {
        HPdown = true;
    }
}
