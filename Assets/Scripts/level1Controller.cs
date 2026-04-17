using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class level1Controller : MonoBehaviour

{
  public npcDialogueController inspector;
  public npcDialogueController policeman1;
  public npcDialogueController policeman2;
  public npcDialogueController policeman3;
  public npcDialogueController witness;
  public npcDialogueController cat;
  [Header("Display")]
  public GameObject questHintPanel;
  public Text questHintText;

  void Start() {
    string dialogue = "hey friend";
    ChangeDialogue(inspector, dialogue);
    questHintPanel.SetActive(true);
    questHintText.text = "Go talk to the inspector";
  }
  public void ChangeDialogue(npcDialogueController npc, string dialogue) {
    npc.SetDialogue(new string[] { dialogue });
    // Debug.Log("Changed dialogue for " + npc.name);
  }
}
