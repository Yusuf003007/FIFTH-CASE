using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem; // for player key press

public class NPC : MonoBehaviour

{

  public GameObject dialoguePanel;
  public Text dialogueText;
  public string[] dialogue;
  private int index = 0;
  public float wordSpeed;
  public bool playerIsClose;

  void Start()

  {
    dialogueText.text = "";
  }

  // Update is called once per frame

  void Update() {
    if (Keyboard.current.eKey.wasPressedThisFrame && playerIsClose) {
      if (!dialoguePanel.activeInHierarchy) {
        dialoguePanel.SetActive(true);
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

  private void OnTriggerEnter2D(Collider2D other)

  {
    playerIsClose = true;
    Debug.Log("Player entered trigger!");
  }

  private void OnTriggerExit2D(Collider2D other) {
    playerIsClose = false;
    Debug.Log("Player exit trigger!");
    RemoveText();
  }
}
