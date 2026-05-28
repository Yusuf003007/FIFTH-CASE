using UnityEngine;
using System.Collections;
using UnityEngine.Playables;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class HouseDoor : MonoBehaviour {

  [Header("Teleport destination")]
  public Transform targetPosition;
  [Header("Ui")]
  public GameObject HintInteract;
  private bool isPlayerInZone = false;
  private Transform player;
  public Transform cameraTarget; // drag your camera or camera target here

  [Header("CutScene")]
  public GameObject cutScenePanel;
  [Header("Outside related")]
  public GameObject door;

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
          "Press '" + interactKey + "' to open the door.";
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

    // Store the renderer once at the top of the coroutine
    SpriteRenderer playerSprite = player.GetComponent<SpriteRenderer>();

    Color c = playerSprite.color;
    c.a = 0f;
    playerSprite.color = c;

    yield return null;
    if (door != null) {
      door.SetActive(true);
      // Hide player (instead of SetActive false)
      yield return new WaitForSeconds(2f);
    }
    cutScenePanel.SetActive(true);

    TimelineAsset timeline = (TimelineAsset)cutsceneTimeline.playableAsset;
    foreach (var track in timeline.GetOutputTracks()) {
      if (track is SignalTrack)
        cutsceneTimeline.SetGenericBinding(
            track, this.gameObject.GetComponent<SignalReceiver>());
    }

    // play cutscene
    cutsceneTimeline.Play();

    // wait for it to finish
    // TeleportPlayer();

    yield return new WaitForSeconds((float)cutsceneTimeline.duration);

    cutScenePanel.SetActive(false);

    // Show player again
    c = playerSprite.color;
    c.a = 1f;
    playerSprite.color = c;
    if (door != null) {
      door.SetActive(false);
    }
  }
  public void TeleportPlayer() {
    if (player != null && targetPosition != null) {
      isPlayerInZone = false;
      HintInteract.SetActive(false);

      player.position = targetPosition.position;
    }
  }

  public void TeleportCamera() {

    Transform cameraTransform = Camera.main.transform;
    cameraTransform.position =
        new Vector3(targetPosition.position.x, targetPosition.position.y,
                    cameraTransform.position.z);
  }
}
