using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class level1Controller : MonoBehaviour

{
  public Sprite playerAvatar;
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
    // string dialogue = "hey friend";
    // ChangeDialogue(inspector, dialogue);
    // questHintPanel.SetActive(true);
    // questHintText.text = "Go talk to the inspector";
    StartInspectorConversation();

    questHintPanel.SetActive(true);
    questHintText.text = "Go talk to the inspector";
  }
  public void ChangeDialogue(npcDialogueController npc, string text) {
    DialogueLine line =
        new DialogueLine { speakerName = "Inspector", // or npc.npcName
                           speakerAvatar = npc.npcAvatar, text = text };

    npc.SetDialogue(new DialogueLine[] { line });
  }

  public void StartInspectorConversation() {
    DialogueLine[] conversation = new DialogueLine[] {
      new DialogueLine { speakerName = "Inspector",
                         speakerAvatar = inspector.npcAvatar,
                         text = "Hey, we have a situation." },
      new DialogueLine { speakerName = "Player", speakerAvatar = playerAvatar,
                         text = "What happened?" },
      new DialogueLine { speakerName = "Inspector",
                         speakerAvatar = inspector.npcAvatar,
                         text = "Someone stole a wallet." },
      new DialogueLine {
        speakerName = "Player",
        speakerAvatar = playerAvatar,
        text = "I’ll check with the witness.",
      }
    };

    inspector.SetDialogue(conversation);
  }
}
