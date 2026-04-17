using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[System.Serializable]
public class DialogueLine {
  public string speakerName;
  public Sprite speakerAvatar;
  public string text;
}

public class npcDialogueController : MonoBehaviour {
  [Header("NPC identity")]
  public string npcName;
  public Sprite npcAvatar;

  [Header("NPC Layout")]
  public Text displayName;
  public Image displayAvatar;
  public GameObject dialoguePanel;
  public Text dialogueText;
  public GameObject HintDialogueKey;

  [Header("Dialogue Settings")]
  public DialogueLine[] dialogue;
  public float wordSpeed;
  public bool playerIsClose = true;

  private int index = 0;
  public bool dialogueDone = false;

  void Start() {
    dialogueText.text = "";
    HintDialogueKey.SetActive(false);
  }

  void Update() {
    if (Keyboard.current.eKey.wasPressedThisFrame && playerIsClose) {
      if (!dialoguePanel.activeInHierarchy) {
        dialoguePanel.SetActive(true);
        HintDialogueKey.SetActive(false);

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
    HintDialogueKey.SetActive(true);
    dialogueDone = true;
  }

  public void SetDialogue(DialogueLine[] newDialogue) {
    StopAllCoroutines();
    dialogue = newDialogue;
    index = 0;
    dialogueText.text = "";
  }

  private void OnTriggerEnter2D(Collider2D other) {
    playerIsClose = true;
    HintDialogueKey.SetActive(true);
  }

  private void OnTriggerExit2D(Collider2D other) {
    playerIsClose = false;
    RemoveText();
    HintDialogueKey.SetActive(false);
  }
}
