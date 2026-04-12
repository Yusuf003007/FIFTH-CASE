using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem; // for player key press
// https://www.youtube.com/watch?v=1nFNOyCalzo
public class NPCDialogueController : MonoBehaviour

{

  [Header("NPC idendity")]
  public string npcName;
  public Sprite npcAvatar;

  [Header("NPC Layout")]
  public Text displayName;
  public Image displayAvatar;
  public GameObject dialoguePanel;
  public Text dialogueText;
  public GameObject HintDialogueKey;

  [Header("NPC Settings")]
  public string[] dialogue;
  public float wordSpeed;
  public bool playerIsClose = true;

  private int index = 0;

  void Start()

  {
    dialogueText.text = "";
    HintDialogueKey.SetActive(false);
  }

  // Update is called once per frame

  void Update() {

    if (Keyboard.current.eKey.wasPressedThisFrame && playerIsClose) {
      if (!dialoguePanel.activeInHierarchy) {
        dialoguePanel.SetActive(true);
        HintDialogueKey.SetActive(false);
        displayName.text = npcName;
        displayAvatar.sprite = npcAvatar;
        StartCoroutine(Typing());
      } else if (dialogueText.text == dialogue[index]) {
        NextLine();
      }
    }

    if (Keyboard.current.qKey.wasPressedThisFrame &&
        dialoguePanel.activeInHierarchy) {
      RemoveText();
    }
  }

  public void RemoveText() {
    dialogueText.text = "";
    index = 0;
    dialoguePanel.SetActive(false);
    HintDialogueKey.SetActive(true);
  }

  public void SetDialogue(string[] newDialogue) {
    StopAllCoroutines();
    dialogue = newDialogue;
    index = 0;
    dialogueText.text = "";
  }
  IEnumerator Typing() {
    foreach (char letter in dialogue[index].ToCharArray()) {
      dialogueText.text += letter;
      yield return new WaitForSeconds(wordSpeed);
    }
  }

  public void NextLine() {
    if (index < dialogue.Length - 1)

    {
      index++;
      dialogueText.text = "";
      StartCoroutine(Typing());
    }

    else {
      RemoveText();
    }
  }

  private void OnTriggerEnter2D(Collider2D other) {
    playerIsClose = true;
    /*Debug.Log("Player entered trigger!");*/
    HintDialogueKey.SetActive(true);
  }

  private void OnTriggerExit2D(Collider2D other) {
    playerIsClose = false;
    /*Debug.Log("Player exit trigger!");*/
    RemoveText();
    HintDialogueKey.SetActive(false);
  }
}
