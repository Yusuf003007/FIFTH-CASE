using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {
  public AudioSource musicSource;
  public AudioSource sfxSource;

  public Slider masterSlider;
  public Slider musicSlider;
  public Slider sfxSlider;

  void Start() {
    float master = GetMasterVolume();
    float music = GetMusicVolume();
    float sfx = GetSFXVolume();

    AudioListener.volume = master;
    musicSource.volume = music;
    sfxSource.volume = sfx;

    masterSlider.value = master;
    musicSlider.value = music;
    sfxSlider.value = sfx;

    masterSlider.onValueChanged.AddListener(SetMasterVolume);
    musicSlider.onValueChanged.AddListener(SetMusicVolume);
    sfxSlider.onValueChanged.AddListener(SetSFXVolume);
  }

  public void SetMasterVolume(float value) {
    AudioListener.volume = value;
    PlayerPrefs.SetFloat("MasterVolume", value);
    PlayerPrefs.Save();
  }

  public void SetMusicVolume(float value) {
    musicSource.volume = value;
    PlayerPrefs.SetFloat("MusicVolume", value);
    PlayerPrefs.Save();
  }

  public void SetSFXVolume(float value) {
    sfxSource.volume = value;
    PlayerPrefs.SetFloat("SFXVolume", value);
    PlayerPrefs.Save();
  }

  public float GetMasterVolume() {
    return PlayerPrefs.GetFloat("MasterVolume", 1f);
  }

  public float GetMusicVolume() {
    return PlayerPrefs.GetFloat("MusicVolume", 1f);
  }

  public float GetSFXVolume() { return PlayerPrefs.GetFloat("SFXVolume", 1f); }
}
