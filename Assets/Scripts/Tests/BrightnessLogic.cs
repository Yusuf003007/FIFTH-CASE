// BrightnessLogic.cs
public class BrightnessLogic {
  private const string BrightnessKey = "Brightness";

  public float GetBrightness() {
    return UnityEngine.PlayerPrefs.GetFloat(BrightnessKey, 0.5f);
  }

  public void SaveBrightness(float value) {
    UnityEngine.PlayerPrefs.SetFloat(BrightnessKey, value);
    UnityEngine.PlayerPrefs.Save();
  }

  // Pure math — no Image/UI dependency
  public float CalculateAlpha(float brightnessValue) {
    return 1f - brightnessValue;
  }
}
