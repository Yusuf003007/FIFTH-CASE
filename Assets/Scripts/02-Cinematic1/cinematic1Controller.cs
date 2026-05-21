using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class cinematic1Controller : MonoBehaviour {
  [Header("Player")]
  public string playerName;
  public Sprite playerAvatar;

  [Header("Stranger in the nightmare")]
  public string strangerName;
  public Sprite strangerAvatar;

  [Header("Alarm clock")]
  public string alarmName;
  public Sprite alarmAvatar;

  [Header("Inspector friend")]
  public string inspectorName;
  public Sprite inspectorAvatar;

  [Header("Display Layout")]
  public GameObject dialoguePanel;
  public Image displayAvatar;
  public Text displayName;
  public Text dialogueText;

  [Header("Dialogue Settings")]
  public DialogueLine[] dialogue;
  public float wordSpeed;
  private int index = 0;
  private bool dialogueDone = false;

  void Start() {
    dialogueText.text = "";
    Nightmare();
  }

  void Update() {
    if (Keyboard.current.eKey.wasPressedThisFrame) {
      if (!dialoguePanel.activeInHierarchy) {
        dialoguePanel.SetActive(true);

        ShowLine();
      } else if (dialogueText.text == dialogue[index].text) {
        NextLine();
      }
    }

    if (Keyboard.current.qKey.wasPressedThisFrame &&
        dialoguePanel.activeInHierarchy) {
      RemoveText();
    }
  }

  void ShowLine() {
    dialogueText.text = "";

    displayName.text = dialogue[index].speakerName;
    displayAvatar.sprite = dialogue[index].speakerAvatar;

    // Optional: align text depending on speaker
    if (dialogue[index].speakerName == "Player") {
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
    if (index < dialogue.Length - 1) {
      index++;
      ShowLine();
    } else {
      RemoveText();
    }
  }

  public void RemoveText() {
    StopAllCoroutines();
    dialogueText.text = "";
    index = 0;
    dialoguePanel.SetActive(false);
  }

  private void SetDialogue(DialogueLine[] newDialogue) {
    StopAllCoroutines();
    dialogue = newDialogue;
    index = 0;
    dialogueText.text = "";
  }
  public void StartDialogue(DialogueLine[] newDialogue) {
    SetDialogue(newDialogue);
    dialoguePanel.SetActive(true);
    ShowLine();
  }
  private void Nightmare() {
    DialogueLine[] conversation = new DialogueLine[] {
      new DialogueLine {
        speakerName = strangerName, speakerAvatar = strangerAvatar,
        text = "I… I already told the other officers everything. I just came " +
               "through here like I always do."
      },

      new DialogueLine { speakerName = playerName, speakerAvatar = playerAvatar,
                         text = "Start from the beginning." }
    };

    StartDialogue(conversation);
  }
}
