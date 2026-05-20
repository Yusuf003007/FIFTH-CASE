using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class HouseDoor : MonoBehaviour {

  [Header("Teleport destination")]
  public Transform targetPosition;
  [Header("Ui")]
  public GameObject HintInteract;
  private bool isPlayerInZone = false;
  private Transform player;

  [Header("Keybind purpose")]
  public GameObject controlSettings;

  private void OnTriggerEnter2D(Collider2D other) {
    if (other.CompareTag("Player")) {
      isPlayerInZone = true;
      player = other.transform;
      Debug.Log("In");
      HintInteract.SetActive(true);

      KeyCode interactKey = RebindKey.Instance.GetKey("Interact");
      Key unityKey =
          (Key)System.Enum.Parse(typeof(Key), interactKey.ToString());
      HintInteract.GetComponentInChildren<TextMeshProUGUI>().text =
          "Press" + interactKey + "to open the door.";
    }
  }

  private void OnTriggerExit2D(Collider2D other) {
    if (other.CompareTag("Player")) {
      isPlayerInZone = false;
      player = null;
      Debug.Log("Out");
    }
  }

  private void Update() {
    if (isPlayerInZone) {
      HintInteract.SetActive(true);

      KeyCode interactKey = RebindKey.Instance.GetKey("Interact");
      Key unityKey =
          (Key)System.Enum.Parse(typeof(Key), interactKey.ToString());
      if (Keyboard.current[unityKey].wasPressedThisFrame) {
        TeleportPlayer();
        isPlayerInZone = false;
        HintInteract.SetActive(false);
      }
    }
  }

  private void TeleportPlayer() {
    if (player != null && targetPosition != null) {
      player.position = targetPosition.position;
    }
  }
}
