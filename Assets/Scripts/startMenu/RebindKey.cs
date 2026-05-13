using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class RebindKey : MonoBehaviour {

  public static RebindKey Instance;
  public string action;           // the action of the key
  private KeyCode key;            // the key
  public TextMeshProUGUI keyText; // where to display the key
  private bool waitingForKey = false;

  void Start() {
    string[] actionList =
        new string[] { "MoveUp",   "MoveDown",  "MoveLeft", "MoveRight",
                       "Interact", "Inventory", "PauseMenu" };

    // if (PlayerPrefs.GetString("bleh", "None") == "None") {
    //  //Debug.Log("condition work");
    //}
    foreach (string i in actionList) {

      if (action == i) {
        // Set default keybinds if do not exist
        if (PlayerPrefs.GetString(action, "None") == "None") {

          // Debug.Log("Was not set" + i);
          switch (i) {
          case "MoveUp":
            setKey(i, KeyCode.W);
            key = GetKey(i);
            // Debug.Log("Default Value assigned" + i + " = " + key);

            break;

          case "MoveDown":
            setKey(i, KeyCode.S);
            key = GetKey(i);
            // Debug.Log("Default Value assigned" + i + " = " + key);

            break;

          case "MoveLeft":
            setKey(i, KeyCode.A);
            key = GetKey(i);
            // Debug.Log("Default Value assigned" + i + " = " + key);

            break;

          case "MoveRight":
            setKey(i, KeyCode.D);
            key = GetKey(i);
            // Debug.Log("Default Value assigned" + i + " = " + key);

            break;

          case "Interact":
            setKey(i, KeyCode.E);
            key = GetKey(i);
            // Debug.Log("Default Value assigned" + i + " = " + key);

            break;

          case "Inventory":
            setKey(i, KeyCode.I);
            key = GetKey(i);
            // Debug.Log("Default Value assigned" + i + " = " + key);

            break;

          case "PauseMenu":
            setKey(i, KeyCode.Escape);
            key = GetKey(i);
            // Debug.Log("Default Value assigned" + i + " = " + key);

            break;
          }
        }
        KeyCode actualKey = GetKey(i);
        UpdateKeyText(actualKey);
      }
    }
  }
  void Awake() {
    Instance = this; // ← this must exist
  }
  void OnGUI() {
    if (waitingForKey) {
      Event e = Event.current;
      if (e.isKey && e.keyCode != KeyCode.None) {
        if (checkKey(action, e.keyCode)) {
          key = e.keyCode;
          waitingForKey = false;
          UpdateKeyText(key);
          setKey(action, key);
        } else {
          // Debug.Log(e.keyCode + " is already in use!");
        }
      }
    }
  }

  public void StartRebind() {
    waitingForKey = true;
    keyText.text = "Press key...";
  }

  bool checkKey(string action, KeyCode currentKey) {

    List<string> actionList =
        new List<string> { "MoveUp",   "MoveDown",  "MoveLeft", "MoveRight",
                           "Interact", "Inventory", "PauseMenu" };

    actionList.Remove(action);

    foreach (string i in actionList) {
      if (currentKey == GetKey(i)) {

        return false;
      }
    }
    return true;
  }

  public KeyCode GetKey(string action) {
    string savedValue = PlayerPrefs.GetString(action, "Space");
    // //Debug.Log("Saved value :" + savedValue);
    KeyCode value = (KeyCode)System.Enum.Parse(typeof(KeyCode), savedValue);
    // Debug.Log("GetKey of " + action + "=" + value);

    return value;
  }

  void UpdateKeyText(KeyCode key) { keyText.text = key.ToString(); }

  void setKey(string action, KeyCode currentKey) {
    PlayerPrefs.SetString(action, currentKey.ToString());
    PlayerPrefs.Save();
    // Debug.Log("SetKey : " + action + " = " + key);
  }
}
