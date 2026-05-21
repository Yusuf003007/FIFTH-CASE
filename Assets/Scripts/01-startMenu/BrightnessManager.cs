using UnityEngine;
using UnityEngine.UI;

public class BrightnessManager : MonoBehaviour {
  public Image brightnessOverlay;
  public Slider brightnessSlider;

  private const string BrightnessKey = "Brightness";

  void Start() {
    float savedBrightness = GetBrightness();

    brightnessSlider.value = savedBrightness;
    ApplyBrightness(savedBrightness);

    brightnessSlider.onValueChanged.AddListener(ApplyBrightness);
  }

  public float GetBrightness() {
    return PlayerPrefs.GetFloat(BrightnessKey, 0.5f);
  }

  public void ApplyBrightness(float value) {
    Color color = brightnessOverlay.color;
    color.a = 1f - value;
    brightnessOverlay.color = color;

    PlayerPrefs.SetFloat(BrightnessKey, value);
    PlayerPrefs.Save();
  }
}
