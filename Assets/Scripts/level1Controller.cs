using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem; // for player key press

public class level1Controller : MonoBehaviour {
  public string playerName = "You";
  public Sprite playerAvatar;

  public npcDialogueController inspector;
  public npcDialogueController policeman1;
  public npcDialogueController policeman2;
  public npcDialogueController policeman3;
  public npcDialogueController witness;

  [Header("UI")]
  public GameObject questHintPanel;
  public Text questHintText;

  [Header("Progress")]
  public int questStage = 0;

  void Start() {
    questStage = 0;

    questHintPanel.SetActive(true);
    questHintText.text = "Talk to the Inspector";
    HandleQuestProgress();
  }

  void Update() {}

  public void OnNPCDialogueFinished(npcDialogueController npc) {
    Debug.Log(npc.npcName + " finished dialogue");

    HandleQuestProgress();
  }
  void HandleQuestProgress() {
    // INSPECTOR TALK
    if (questStage == 0) {
      TalkToInspector();

      Debug.Log("Inspector dialogueDone = " + inspector.dialogueDone);
      if (inspector.dialogueDone == true) {
        questStage = 1;
        Debug.Log("Inspector dialogueDone = " + inspector.dialogueDone);
        inspector.dialogueDone = false;
      }
    }

    // POLICE TALK + ITEMS
    if (questStage == 1) {
      questHintText.text = "Talk to the Policeman and collect all items";
      TalkToPolice();
      if (policeman1.dialogueDone == true && policeman2.dialogueDone == true &&
          policeman3.dialogueDone == true && HasAllEvidence()) {
        questStage = 2;
      }
    }

    if (questStage == 2) {
      questHintText.text = "Talk to the Witness";
      TalkToWitness();
    }
  }

  public bool HasAllEvidence() {
    if (InventoryController.Instance.HasItems(new int[] { 0, 1, 2 })) {
      return true;
    }
    return false;
  }

  public void TalkToInspector() {
    DialogueLine[] conversation = new DialogueLine[] {
      new DialogueLine {
        speakerName = inspector.npcName, speakerAvatar = inspector.npcAvatar,
        text =
            "Took you long enough. Thought you’d hang up on me like last time."
      },

      new DialogueLine { speakerName = playerName, speakerAvatar = playerAvatar,
                         text = "You said it was bad. I’m here, aren’t I?" },

      new DialogueLine {
        speakerName = inspector.npcName, speakerAvatar = inspector.npcAvatar,
        text = "Yeah… well. I wish you weren’t. This one’s messy."
      },

      new DialogueLine { speakerName = inspector.npcName,
                         speakerAvatar = inspector.npcAvatar,
                         text = "(Inspector gestures toward the scene, " +
                                "lowering his voice slightly.)" },

      new DialogueLine {
        speakerName = inspector.npcName, speakerAvatar = inspector.npcAvatar,
        text =
            "Male, mid-40s. Name’s still being confirmed, but we’ve got ID " +
            "suggesting he’s a local business owner. Found about an hour ago."
      },

      new DialogueLine { speakerName = playerName, speakerAvatar = playerAvatar,
                         text = "Cause of death?" },

      new DialogueLine {
        speakerName = inspector.npcName, speakerAvatar = inspector.npcAvatar,
        text = "That’s the thing… it’s not clean. Multiple injuries. Could " +
               "be blunt force, could be something else. Forensics is still " +
               "arguing about it."
      },

      new DialogueLine { speakerName = inspector.npcName,
                         speakerAvatar = inspector.npcAvatar,
                         text = "(Pauses, looks back at the body.)" },

      new DialogueLine {
        speakerName = inspector.npcName, speakerAvatar = inspector.npcAvatar,
        text = "No signs of a struggle in the immediate area. Either he " +
               "didn’t see it coming… or it didn’t happen here."
      },

      new DialogueLine { speakerName = playerName, speakerAvatar = playerAvatar,
                         text = "So we’re looking at a dump site?" },

      new DialogueLine { speakerName = inspector.npcName,
                         speakerAvatar = inspector.npcAvatar,
                         text =
                             "Maybe. But there’s blood here. Not enough for " +
                             "what it should be, though. Doesn’t add up." },

      new DialogueLine { speakerName = inspector.npcName,
                         speakerAvatar = inspector.npcAvatar,
                         text = "(Leans in slightly, more serious tone.)" },

      new DialogueLine {
        speakerName = inspector.npcName, speakerAvatar = inspector.npcAvatar,
        text =
            "And before you ask—no witnesses. Just one guy who found the " +
            "body. Patrol picked him up, he’s over there shaking like a leaf."
      },

      new DialogueLine { speakerName = playerName, speakerAvatar = playerAvatar,
                         text = "You think he’s involved?" },

      new DialogueLine { speakerName = inspector.npcName,
                         speakerAvatar = inspector.npcAvatar,
                         text =
                             "I think he’s hiding something. Whether that’s " +
                             "guilt or fear… that’s your job to figure out." },

      new DialogueLine { speakerName = inspector.npcName,
                         speakerAvatar = inspector.npcAvatar,
                         text = "(Straightens up, more formal now.)" },

      new DialogueLine {
        speakerName = inspector.npcName, speakerAvatar = inspector.npcAvatar,
        text = "Scene’s yours. Forensics is working the perimeter. Try not " +
               "to step on anything important—unlike last time."
      },

      new DialogueLine { speakerName = playerName, speakerAvatar = playerAvatar,
                         text = "No promises." },

      new DialogueLine { speakerName = inspector.npcName,
                         speakerAvatar = inspector.npcAvatar,
                         text = "Yeah… that’s why I called you." }
    };

    inspector.SetDialogue(conversation);
  }

  public void TalkToPolice() {
    Policeman1Conversation();
    Policeman2Conversation();
    Policeman3Conversation();
  }
  public void TalkToWitness() {
    DialogueLine[] conversation = new DialogueLine[] {
      new DialogueLine {
        speakerName = witness.npcName, speakerAvatar = witness.npcAvatar,
        text = "I… I already told the other officers everything. I just came " +
               "through here like I always do."
      },

      new DialogueLine { speakerName = playerName, speakerAvatar = playerAvatar,
                         text = "Start from the beginning." },

      new DialogueLine {
        speakerName = witness.npcName, speakerAvatar = witness.npcAvatar,
        text = "Fine… I was walking to work. Cut through the alley. Saw " +
               "something on the ground… thought it was trash at first."
      },

      new DialogueLine { speakerName = witness.npcName,
                         speakerAvatar = witness.npcAvatar,
                         text = "(He swallows, avoids eye contact.)" },

      new DialogueLine { speakerName = witness.npcName,
                         speakerAvatar = witness.npcAvatar,
                         text = "Then I got closer and… it was him. I " +
                                "panicked. I didn’t touch anything, I swear." },

      new DialogueLine { speakerName = playerName, speakerAvatar = playerAvatar,
                         text = "You called it in?" },

      new DialogueLine {
        speakerName = witness.npcName, speakerAvatar = witness.npcAvatar,
        text = "Yes! Immediately. I didn’t even stay long after that. I " +
               "just… I just waited at the corner until police arrived."
      },

      new DialogueLine { speakerName = witness.npcName,
                         speakerAvatar = witness.npcAvatar,
                         text = "(A beat. His hands are trembling slightly.)" },

      new DialogueLine { speakerName = playerName, speakerAvatar = playerAvatar,
                         text =
                             "Funny place to cut through. This alley isn’t " +
                             "exactly on the way to anywhere important." },

      new DialogueLine {
        speakerName = witness.npcName, speakerAvatar = witness.npcAvatar,
        text = "It is for me! I mean—it’s a shortcut. Saves time."
      },

      new DialogueLine { speakerName = witness.npcName,
                         speakerAvatar = witness.npcAvatar,
                         text =
                             "(He forces a nervous laugh that doesn’t land.)" },

      new DialogueLine { speakerName = witness.npcName,
                         speakerAvatar = witness.npcAvatar,
                         text = "Look, I don’t know what happened here. I " +
                                "just found him. That’s all." },

      new DialogueLine {
        speakerName = playerName, speakerAvatar = playerAvatar,
        text = "And you didn’t see or hear anything before that?"
      },

      new DialogueLine { speakerName = witness.npcName,
                         speakerAvatar = witness.npcAvatar,
                         text = "No… nothing. Just… quiet. Too quiet." },

      new DialogueLine { speakerName = witness.npcName,
                         speakerAvatar = witness.npcAvatar,
                         text = "(He finally looks away, voice lower.)" },

      new DialogueLine { speakerName = witness.npcName,
                         speakerAvatar = witness.npcAvatar,
                         text =
                             "Can I go now? I already did my part, right?" }
    };

    witness.SetDialogue(conversation);
  }

  public void ChangeDialogue(npcDialogueController npc, string text) {
    DialogueLine line =
        new DialogueLine { speakerName = "Inspector", // or npc.npcName
                           speakerAvatar = npc.npcAvatar, text = text };

    npc.SetDialogue(new DialogueLine[] { line });
  }

  public void Policeman1Conversation() {
    DialogueLine[] conversation = new DialogueLine[] {
      new DialogueLine { speakerName = policeman1.npcName,
                         speakerAvatar = policeman1.npcAvatar,
                         text =
                             "We’ve already sealed off the area, but there’s " +
                             "still plenty you might want to look at." },

      new DialogueLine {
        speakerName = policeman1.npcName, speakerAvatar = policeman1.npcAvatar,
        text =
            "Check near the east side of the alley—there’s scattered debris. " +
            "Could be nothing… could be something the killer dropped."
      },

      new DialogueLine {
        speakerName = policeman1.npcName, speakerAvatar = policeman1.npcAvatar,
        text = "Just don’t disturb the marked evidence. Forensics will lose " +
               "their minds if you mess that up."
      }
    };

    policeman1.SetDialogue(conversation);
  }
  public void Policeman2Conversation() {
    DialogueLine[] conversation = new DialogueLine[] {
      new DialogueLine { speakerName = policeman2.npcName,
                         speakerAvatar = policeman2.npcAvatar,
                         text =
                             "We cataloged the obvious stuff, but honestly… " +
                             "I don’t think we’ve got the full picture yet." },

      new DialogueLine {
        speakerName = policeman2.npcName, speakerAvatar = policeman2.npcAvatar,
        text = "There’s a strange footprint pattern near the drainage grate. " +
               "Not sure if it belongs to the victim."
      },

      new DialogueLine { speakerName = policeman2.npcName,
                         speakerAvatar = policeman2.npcAvatar,
                         text =
                             "If you’re good at spotting inconsistencies, " +
                             "start there. Something feels off about it." }
    };

    policeman2.SetDialogue(conversation);
  }
  public void Policeman3Conversation() {
    DialogueLine[] conversation = new DialogueLine[] {
      new DialogueLine { speakerName = policeman3.npcName,
                         speakerAvatar = policeman3.npcAvatar,
                         text = "Uh… sir? I mean—detective. You should " +
                                "probably check the victim’s belongings." },

      new DialogueLine { speakerName = policeman3.npcName,
                         speakerAvatar = policeman3.npcAvatar,
                         text = "We found a torn pocket and a missing item " +
                                "list doesn’t match what’s actually here." },

      new DialogueLine { speakerName = policeman3.npcName,
                         speakerAvatar = policeman3.npcAvatar,
                         text = "Also… there’s a small object near the wall. " +
                                "I didn’t touch it. Looked important." }
    };

    policeman3.SetDialogue(conversation);
  }
}
