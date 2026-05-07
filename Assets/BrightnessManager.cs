using UnityEngine;
using UnityEngine.UI;

public class BrightnessManager : MonoBehaviour
{
    public Image brightnessOverlay;
    public Slider brightnessSlider;

    private float brightnessValue = 0.5f;

    void Start()
    {
        brightnessValue = PlayerPrefs.GetFloat("Brightness", 0.5f);

        brightnessSlider.value = brightnessValue;

        SetBrightness(brightnessValue);

        brightnessSlider.onValueChanged.AddListener(SetBrightness);
    }

    public void SetBrightness(float value)
    {
        brightnessValue = value;

        Color color = brightnessOverlay.color;
        color.a = 1f - value;

        brightnessOverlay.color = color;

        PlayerPrefs.SetFloat("Brightness", brightnessValue);
        PlayerPrefs.Save();
    }
}