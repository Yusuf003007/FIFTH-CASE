using UnityEngine;

public class PlayerPrefsStorage : IKeyStorage {
  public string GetString(string key, string defaultValue) =>
      PlayerPrefs.GetString(key, defaultValue);

  public void SetString(string key, string value) {
    PlayerPrefs.SetString(key, value);
    PlayerPrefs.Save();
  }
}
