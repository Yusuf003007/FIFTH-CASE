// AudioVolumeLogic.cs — pure C#, no MonoBehaviour
public class AudioVolumeLogic {
  // Same logic from your AudioManager, no engine refs
  public void SetMasterVolume(float value) {
    UnityEngine.PlayerPrefs.SetFloat("MasterVolume", value);
    UnityEngine.PlayerPrefs.Save();
  }

  public void SetMusicVolume(float value) {
    UnityEngine.PlayerPrefs.SetFloat("MusicVolume", value);
    UnityEngine.PlayerPrefs.Save();
  }

  public void SetSFXVolume(float value) {
    UnityEngine.PlayerPrefs.SetFloat("SFXVolume", value);
    UnityEngine.PlayerPrefs.Save();
  }

  public float
  GetMasterVolume() => UnityEngine.PlayerPrefs.GetFloat("MasterVolume", 1f);

  public float
  GetMusicVolume() => UnityEngine.PlayerPrefs.GetFloat("MusicVolume", 1f);

  public float GetSFXVolume() => UnityEngine.PlayerPrefs.GetFloat("SFXVolume",
                                                                  1f);
}
