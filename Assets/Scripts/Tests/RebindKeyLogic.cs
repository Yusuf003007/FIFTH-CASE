using System.Collections.Generic;
using UnityEngine;

// Pure C# — no MonoBehaviour, no Unity engine calls
public class RebindKeyLogic {
  // Inject storage so tests can replace PlayerPrefs with a fake
  private readonly IKeyStorage _storage;

  public static readonly Dictionary<string, KeyCode> DefaultBindings = new() {
    { "MoveUp", KeyCode.W },         { "MoveDown", KeyCode.S },
    { "MoveLeft", KeyCode.A },       { "MoveRight", KeyCode.D },
    { "Interact", KeyCode.E },       { "Inventory", KeyCode.I },
    { "PauseMenu", KeyCode.Escape },
  };

  public RebindKeyLogic(IKeyStorage storage) { _storage = storage; }

  /// Returns the stored key, or the default if never set
  public KeyCode GetKey(string action) {
    string saved = _storage.GetString(action, "None");
    if (saved == "None")
      saved = DefaultBindings.TryGetValue(action, out KeyCode def)
                  ? def.ToString()
                  : KeyCode.Space.ToString();

    return (KeyCode)System.Enum.Parse(typeof(KeyCode), saved);
  }

  /// Persists a key binding
  public void SetKey(string action, KeyCode key) {
    _storage.SetString(action, key.ToString());
  }

  /// Returns false (and action name) if the key is already used by another
  /// action
  public bool IsKeyAvailable(string action, KeyCode candidate,
                             out string conflictingAction) {
    foreach (var kvp in DefaultBindings) {
      if (kvp.Key == action)
        continue;
      if (GetKey(kvp.Key) == candidate) {
        conflictingAction = kvp.Key;
        return false;
      }
    }
    conflictingAction = null;
    return true;
  }

  /// Converts a KeyCode to the Input System path string
  public string KeyCodeToInputPath(KeyCode key) {
    return $"<Keyboard>/{key.ToString().ToLower()}";
  }

  /// Returns the binding index for a movement action (or -1 if not movement)
  public int GetMovementBindingIndex(string action) {
    return action switch {
      "MoveUp" => 1, "MoveDown" => 2, "MoveLeft" => 3, "MoveRight" => 4,
      _ => -1,
    };
  }
}
