using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class RebindKey : MonoBehaviour {

  public static RebindKey Instance;
  public string action;             // the action of the key
  private KeyCode key;              // the key
  public TextMeshProUGUI keyText;   // where to display the key
  public GameObject messageKeyUsed; // where to display the key

  private bool waitingForKey = false;

  public InputActionReference moveAction;
  void Start() {
    string[] actionList =
        new string[] { "MoveUp",   "MoveDown",  "MoveLeft", "MoveRight",
                       "Interact", "Inventory", "PauseMenu" };
    // moveAction.action.ApplyBindingOverride(1, "<Keyboard>/w");
    // moveAction.action.ApplyBindingOverride(2, "<Keyboard>/s");
    // moveAction.action.ApplyBindingOverride(3, "<Keyboard>/a");
    // moveAction.action.ApplyBindingOverride(4, "<Keyboard>/d");

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
            // moveAction.action.ApplyBindingOverride(1, "<Keyboard>/w");
            StartCoroutine(ApplyRebind(moveAction, key));

            // Debug.Log("Default Value assigned" + i + " = " + key);

            break;

          case "MoveDown":
            setKey(i, KeyCode.S);

            // moveAction.action.ApplyBindingOverride(2, "<Keyboard>/s");
            key = GetKey(i);
            StartCoroutine(ApplyRebind(moveAction, key));

            // Debug.Log("Default Value assigned" + i + " = " + key);

            break;

          case "MoveLeft":
            setKey(i, KeyCode.A);
            key = GetKey(i);

            // moveAction.action.ApplyBindingOverride(3, "<Keyboard>/a");
            StartCoroutine(ApplyRebind(moveAction, key));
            // Debug.Log("Default Value assigned" + i + " = " + key);

            break;

          case "MoveRight":
            setKey(i, KeyCode.D);

            // moveAction.action.ApplyBindingOverride(4, "<Keyboard>/d");
            key = GetKey(i);

            StartCoroutine(ApplyRebind(moveAction, key));
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
          Debug.Log("Done");
          List<string> movementAction =
              new List<string> { "MoveUp", "MoveDown", "MoveLeft",
                                 "MoveRight" };

          foreach (string i in movementAction) {
            if (action == i) {

              StartCoroutine(ApplyRebind(moveAction, key));
            }
          }
          UpdateKeyText(key);
          setKey(action, key);
        }
      }

    } else {
      // Debug.Log(e.keyCode + " is already in use!");
    }
  }
  private IEnumerator ApplyRebind(InputActionReference moveAction,
                                  KeyCode key) {
    yield return null;

    string keyBind = key.ToString().ToLower();
    string keySetup = $"<Keyboard>/{keyBind}";

    Dictionary<string, int> bindingIndexMap = new Dictionary<string, int> {
      { "MoveUp", 1 }, { "MoveDown", 2 }, { "MoveLeft", 3 }, { "MoveRight", 4 }
    };

    if (bindingIndexMap.TryGetValue(action, out int bindingIndex)) {
      moveAction.action.Disable(); // ← disable first
      moveAction.action.ApplyBindingOverride(bindingIndex, keySetup);
      moveAction.action.Enable(); // ← re-enable after
      Debug.Log($"Rebound {action} (index {bindingIndex}) to {keySetup}");
    }
  }
  void setMovementKey(string action, KeyCode key,
                      InputActionReference moveAction) {
    string keyBind = key.ToString().ToLower();
    string keySetup = $"<Keyboard>/{keyBind}";

    int id = 99;
    switch (action) {
    case "MoveUp":
      // code block
      id = 1;
      break;
    case "MoveDown":
      id = 2;
      break;
    case "MoveLeft":
      id = 3;
      break;
    case "MoveRight":
      id = 4;
      break;
    default:
      // code block
      break;
    }
    //    Dictionary<string, int> bindingIndex = new() {
    //      { "MoveUp", 0 }, { "MoveDown", 1 }, { "MoveLeft", 2 }, {
    //      "MoveRight", 3 }
    //    };

    Debug.Log("KeySetup =" + id + keySetup);
    moveAction.action.ApplyBindingOverride(id, keySetup);
    // moveAction.action.ApplyBindingOverride(bindingIndex[action],
    //                                       $"<Keyboard>/{key}");
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

        StartCoroutine(ShowMessageForSeconds(5f));
        return false;
      }
    }
    return true;
  }

  private IEnumerator ShowMessageForSeconds(float duration) {
    messageKeyUsed.SetActive(true);
    yield return new WaitForSeconds(duration);
    messageKeyUsed.SetActive(false);
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
