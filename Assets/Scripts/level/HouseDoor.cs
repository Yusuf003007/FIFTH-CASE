using UnityEngine;
using System.Collections;
using UnityEngine.Playables;
using TMPro;
using UnityEngine.InputSystem;

public class HouseDoor : MonoBehaviour {

  [Header("Teleport destination")]
  public Transform targetPosition;
  [Header("Ui")]
  public GameObject HintInteract;
  private bool isPlayerInZone = false;
  private Transform player;

  [Header("CutScene")]
  public GameObject cutScenePanel;

  public PlayableDirector cutsceneTimeline;
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
      HintInteract.SetActive(false);
    }
  }

  private void Update() {
    if (isPlayerInZone) {
      HintInteract.SetActive(true);

      KeyCode interactKey = RebindKey.Instance.GetKey("Interact");
      Key unityKey =
          (Key)System.Enum.Parse(typeof(Key), interactKey.ToString());
      if (Keyboard.current[unityKey].wasPressedThisFrame) {

        StartCoroutine(TeleportRoutine());
      }
    }
  }

  private IEnumerator TeleportRoutine() {

    // enable camera script after delay
    // GameObject.FindWithTag("MainCamera").GetComponent<cameraFollow>().enabled
    // =
    //    false;

    yield return null;

    TeleportPlayer();
    cutScenePanel.SetActive(true);
    // play cutscene
    cutsceneTimeline.Play();

    // wait for it to finish
    yield return new WaitForSeconds((float)cutsceneTimeline.duration);
    cutScenePanel.SetActive(false);

    // yield return new WaitForSeconds(1f); // wait 1 second

    // enable camera script after delay
    // GameObject.FindWithTag("MainCamera").GetComponent<cameraFollow>().enabled
    // =
    //    true;
  }
  private void TeleportPlayer() {
    if (player != null && targetPosition != null) {
      isPlayerInZone = false;
      HintInteract.SetActive(false);

      player.position = targetPosition.position;
    }
  }
}
