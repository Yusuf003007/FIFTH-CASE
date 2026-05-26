// Abstracts PlayerPrefs so tests don't need the engine
public interface IKeyStorage {
  string GetString(string key, string defaultValue);
  void SetString(string key, string value);
}
