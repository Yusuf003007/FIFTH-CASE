using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class npcDialogueController : MonoBehaviour {
  [Header("NPC identity")]
  public string npcName;
  public Sprite npcAvatar;

  [Header("NPC Layout")]
  public Text displayName;
  public Image displayAvatar;
  public GameObject dialoguePanel;
  public GameObject buttonNext;
  public GameObject buttonExit;

  public Text dialogueText;
  public GameObject HintDialogueKey;

  [Header("Dialogue Settings")]
  public DialogueLine[] dialogue;
  public float wordSpeed;
  public bool playerIsClose = false;

  private int index = 0;
  public bool dialogueDone = false;
  public bool npcNotAvailable = true;
  public level1Controller questManager;
  public entryCinematicDiscussion cinematicEntry;

  [Header("Keybind purpose")]
  public GameObject controlSettings;

  void Start() {
    dialogueText.text = "";
    HintDialogueKey.SetActive(false);
    // buttonNext.GetComponent<Button>().onClick.RemoveAllListeners();
    // buttonNext.GetComponent<Button>().onClick.AddListener(NextLine);

    // buttonExit.GetComponent<Button>().onClick.RemoveAllListeners();
    // buttonExit.GetComponent<Button>().onClick.AddListener(RemoveText);
  }

  void Update() {
    KeyCode inventoryKey = RebindKey.Instance.GetKey("Interact");
    Key unityKey = (Key)System.Enum.Parse(typeof(Key), inventoryKey.ToString());
    if (Keyboard.current[unityKey].wasPressedThisFrame && playerIsClose &&
        !controlSettings.gameObject.activeSelf) {
      if (!dialoguePanel.activeInHierarchy) {
        dialoguePanel.SetActive(true);
        HintDialogueKey.SetActive(false);

        ShowLine();
      } else if (dialogueText.text == dialogue[index].text) {
        NextLine();
      }
    }

    // too lazy to add the keybind in the control menu
    // if (Keyboard.current.qKey.wasPressedThisFrame &&
    //    dialoguePanel.activeInHierarchy &&
    //    RebindKey.Instance.waitingForKey == false) {
    //  RemoveText();
    //}
  }
  public void displayDialogue() {
    dialoguePanel.SetActive(true);
    HintDialogueKey.SetActive(false);

    NextLine();
  }

  void ShowLine() {
    dialogueText.text = "";

    displayName.text = dialogue[index].speakerName;
    displayAvatar.sprite = dialogue[index].speakerAvatar;

    // Optional: align text depending on speaker
    if (dialogue[index].speakerName == "You") {
      dialogueText.alignment = TextAnchor.MiddleRight;
    } else {
      dialogueText.alignment = TextAnchor.MiddleLeft;
    }

    StopAllCoroutines();
    StartCoroutine(Typing());
  }

  IEnumerator Typing() {
    foreach (char letter in dialogue[index].text.ToCharArray()) {
      dialogueText.text += letter;
      yield return new WaitForSeconds(wordSpeed);
    }
  }

  public void NextLine() {
    Debug.Log("Called nextline");
    if (index < dialogue.Length - 1) {
      index++;
      ShowLine();
    } else {
      if (npcNotAvailable == false) {
        dialogueDone = true;
        // Debug.Log(" OUT");
        if (questManager != null) {
          // Debug.Log(" IN");
          questManager.OnNPCDialogueFinished(this);
        }
      }
      RemoveText();

      if (cinematicEntry != null && npcNotAvailable == false) {
        Debug.Log("IN");

        HintDialogueKey.SetActive(false);
        dialogueDone = true;
        playerIsClose = false;
        playerController2D.Instance.lockPlayer = false;

        cinematicEntry.OnNPCDialogueFinished(this);
      }
    }
  }

  public void RemoveText() {
    StopAllCoroutines();
    dialogueText.text = "";
    index = 0;
    dialoguePanel.SetActive(false);
    Debug.Log("Hint dialogue TRUE from remove");

    HintDialogueKey.SetActive(true);
  }

  public void SetDialogue(DialogueLine[] newDialogue) {
    StopAllCoroutines();
    dialogue = newDialogue;
    index = 0;
    dialogueText.text = "";
  }

  private void OnTriggerEnter2D(Collider2D other) {
    if (other.CompareTag("Player")) {
      playerIsClose = true;
      Debug.Log("Hint dialogue TRUE from collide");
      HintDialogueKey.SetActive(true);
    }
  }
  private void OnTriggerExit2D(Collider2D other) {
    if (other.CompareTag("Player")) {
      playerIsClose = false;
      Debug.Log("Player entered zone", this.gameObject);
      Debug.Log("Hint dialogue False from collide");
      HintDialogueKey.SetActive(false);
      dialoguePanel.SetActive(false);
    }
  }
}
