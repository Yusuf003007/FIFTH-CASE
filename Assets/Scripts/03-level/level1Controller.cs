using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

using UnityEngine.InputSystem; // for player key press

public class level1Controller : MonoBehaviour {
  public string playerName = "You";
  public Sprite playerAvatar;

  public npcDialogueController inspectorFriend;
  public npcDialogueController inspector;
  public npcDialogueController policeman1;
  public npcDialogueController policeman2;
  public npcDialogueController policeman3;
  public npcDialogueController witness;

  [Header("UI")]
  public GameObject questHintPanel;
  public Text questHintText;

  public GameObject menu;
  public GameObject inventory;
  public GameObject controlSettings;

  [Header("Progress")]
  public int questStage = 0;

  [Header("Cinematic 2")]

  public PlayableDirector cinematic2;
  public GameObject fadePanel;
  public GameObject doorHouseWitness;

  //[Header("Key binding")]
  // public RebindKey inventoryKeybind;
  // public RebindKey menuKeybind;

  void Start() {
    questStage = 0;

    fadePanel.SetActive(false);
    // questHintPanel.SetActive(true);

    questHintText.text = "Talk to the Inspector";
    HandleQuestProgress();
  }

  void Update() {
    // INVENTORY KEY
    // HandleQuestProgress();

    KeyCode inventoryKey = RebindKey.Instance.GetKey("Inventory");
    Key unityKey = (Key)System.Enum.Parse(typeof(Key), inventoryKey.ToString());
    if (Keyboard.current[unityKey].wasPressedThisFrame &&
        !controlSettings.gameObject.activeSelf) {
      Debug.Log("turn on i");

      bool inventoryOpen = inventory.gameObject.activeSelf;

      // Close menu if open
      menu.gameObject.SetActive(false);

      InventoryController.Instance.displayInventory();
      // Toggle inventory
      // inventory.gameObject.SetActive(!inventoryOpen);
    }

    // ESCAPE KEY
    KeyCode pauseMenuKey = RebindKey.Instance.GetKey("PauseMenu");
    unityKey = (Key)System.Enum.Parse(typeof(Key), pauseMenuKey.ToString());
    if (Keyboard.current[unityKey].wasPressedThisFrame &&
        !controlSettings.gameObject.activeSelf) {
      bool menuOpen = menu.gameObject.activeSelf;

      // Close inventory if open
      inventory.gameObject.SetActive(false);

      // Toggle menu
      menu.gameObject.SetActive(!menuOpen);
    }

    // Quest hint visibility
    if (inspectorFriend.dialogueDone) {
      questHintPanel.gameObject.SetActive(!menu.gameObject.activeSelf &&
                                          !inventory.gameObject.activeSelf);
    }
  }
  public void displaySettings() {

    if (MenuManager.Instance == null) {
      Debug.LogError("MenuManager Instance is NULL!");
      return;
    }

    MenuManager.Instance.openSettings();
  }

  public void displayMenu() {
    inventory.SetActive(false);
    menu.SetActive(true);
  }

  public void displayInventory() {
    menu.SetActive(false);
    InventoryController.Instance.displayInventory();
  }

  public void OnNPCDialogueFinished(npcDialogueController npc) {
    Debug.Log(npc.npcName + " finished dialogue");

    HandleQuestProgress();
  }
  public void checkQuestStage() { HandleQuestProgress(); }

  void HandleQuestProgress() {
    // INSPECTOR TALK
    if (questStage == 0) {
      TalkToInspector();
      setNullDiscussion(policeman1);
      setNullDiscussion(policeman2);
      setNullDiscussion(policeman3);
      setNullDiscussion(witness);

      // Debug.Log("Inspector dialogueDone = " + inspector.dialogueDone);
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
      if (witness.dialogueDone == true) {
        questStage = 3;
      }
    }
    if (questStage == 3) {
      questHintPanel.SetActive(false); // SetActive is a METHOD, use () not =

      if (cinematic2 != null) {
        fadePanel.SetActive(true);
        doorHouseWitness.SetActive(false);
        cinematic2.Play();
      }
    }
  }

  private bool HasAllEvidence() {
    if (InventoryController.Instance.HasItems(new int[] { 0, 1, 2 })) {
      return true;
    }
    return false;
  }
  private void setNullDiscussion(npcDialogueController npc) {
    DialogueLine[] conversation = new DialogueLine[] {
      new DialogueLine { speakerName = playerName, speakerAvatar = playerAvatar,
                         text = "Something tells you this isn’t the right " +
                                "moment, let's talk with them later." }
    };
    npc.SetDialogue(conversation);
    npc.npcNotAvailable = true;
  }

  private void TalkToInspector() {
    DialogueLine[] conversation = new DialogueLine[] {
      new DialogueLine { speakerName = inspector.npcName,
                         speakerAvatar = inspector.npcAvatar,
                         text = "Took you long enough. Thought you’d hang " +
                                "up on me like last time." },

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
                         text = "No promises." }

    };

    inspector.SetDialogue(conversation);
    inspector.npcNotAvailable = false;
  }

  private void TalkToPolice() {
    Policeman1Conversation();
    Policeman2Conversation();
    Policeman3Conversation();
  }
  private void TalkToWitness() {
    DialogueLine[] conversation = new DialogueLine[] {
      new DialogueLine { speakerName = witness.npcName,
                         speakerAvatar = witness.npcAvatar,
                         text =
                             "I already told the officers everything… I was " +
                             "just returning something to my neighbor." },

      new DialogueLine { speakerName = playerName, speakerAvatar = playerAvatar,
                         text = "Returning what?" },

      new DialogueLine {
        speakerName = witness.npcName, speakerAvatar = witness.npcAvatar,
        text = "His shovel. He lent it to me a few days ago for some yard work."
      },

      new DialogueLine { speakerName = playerName, speakerAvatar = playerAvatar,
                         text = "So you went to his house." },

      new DialogueLine { speakerName = witness.npcName,
                         speakerAvatar = witness.npcAvatar,
                         text = "Yeah… I knocked, but nobody answered. The " +
                                "front door was slightly open." },

      new DialogueLine {
        speakerName = witness.npcName, speakerAvatar = witness.npcAvatar,
        text = "(He swallows hard and rubs his hands together.)"
      },

      new DialogueLine { speakerName = witness.npcName,
                         speakerAvatar = witness.npcAvatar,
                         text = "I called out for him and stepped inside. " +
                                "That’s when I saw him… on the floor." },

      new DialogueLine { speakerName = playerName, speakerAvatar = playerAvatar,
                         text = "The body?" },

      new DialogueLine { speakerName = witness.npcName,
                         speakerAvatar = witness.npcAvatar,
                         text = "Yes… I froze. For a second I thought maybe " +
                                "he was unconscious, but…" },

      new DialogueLine { speakerName = witness.npcName,
                         speakerAvatar = witness.npcAvatar,
                         text = "(His voice shakes.)" },

      new DialogueLine { speakerName = witness.npcName,
                         speakerAvatar = witness.npcAvatar,
                         text = "There was blood everywhere. I panicked. I " +
                                "didn’t touch anything, I swear." },

      new DialogueLine { speakerName = playerName, speakerAvatar = playerAvatar,
                         text = "You called the police after that?" },

      new DialogueLine {
        speakerName = witness.npcName, speakerAvatar = witness.npcAvatar,
        text = "Immediately. Then I waited outside until they arrived."
      },

      new DialogueLine { speakerName = playerName, speakerAvatar = playerAvatar,
                         text = "And you didn’t see anyone leaving the " +
                                "house? Hear anything unusual?" },

      new DialogueLine { speakerName = witness.npcName,
                         speakerAvatar = witness.npcAvatar,
                         text = "No… nothing. It was completely quiet." },

      new DialogueLine { speakerName = witness.npcName,
                         speakerAvatar = witness.npcAvatar,
                         text = "(He avoids eye contact.)" },

      new DialogueLine { speakerName = witness.npcName,
                         speakerAvatar = witness.npcAvatar,
                         text = "Can I go now ?" }
    };

    witness.SetDialogue(conversation);
    witness.npcNotAvailable = false;
  }

  // private void ChangeDialogue(npcDialogueController npc, string text) {
  //   DialogueLine line =
  //       new DialogueLine { speakerName = "Inspector", // or npc.npcName
  //                          speakerAvatar = npc.npcAvatar, text = text };

  //  npc.SetDialogue(new DialogueLine[] { line });
  //}

  private void Policeman1Conversation() {
    DialogueLine[] conversation = new DialogueLine[] {
      new DialogueLine { speakerName = policeman1.npcName,
                         speakerAvatar = policeman1.npcAvatar,
                         text = "We searched the house top to bottom " +
                                "already. Nothing useful so far." },

      new DialogueLine { speakerName = policeman1.npcName,
                         speakerAvatar = policeman1.npcAvatar,
                         text = "No signs of forced entry, no obvious murder " +
                                "weapon… almost too clean." },

      new DialogueLine {
        speakerName = policeman1.npcName, speakerAvatar = policeman1.npcAvatar,
        text = "But outside’s a different story. The scene around the house " +
               "hasn’t been fully checked yet."
      },

      new DialogueLine { speakerName = policeman1.npcName,
                         speakerAvatar = policeman1.npcAvatar,
                         text =
                             "You should take a look around the yard and " +
                             "alley. Maybe the killer left something behind." },

      new DialogueLine {
        speakerName = policeman1.npcName, speakerAvatar = policeman1.npcAvatar,
        text = "Footprints, discarded items, anything unusual. Sometimes the " +
               "smallest clue breaks a case open."
      },

      new DialogueLine { speakerName = policeman1.npcName,
                         speakerAvatar = policeman1.npcAvatar,
                         text = "Just don’t touch the marked evidence. " +
                                "Forensics already hates us enough." }
    };

    policeman1.SetDialogue(conversation);
    policeman1.npcNotAvailable = false;
  }
  private void Policeman2Conversation() {
    DialogueLine[] conversation = new DialogueLine[] {
      new DialogueLine {
        speakerName = policeman2.npcName, speakerAvatar = policeman2.npcAvatar,
        text = "Honestly? We’ve got no idea what happened here yet."
      },

      new DialogueLine { speakerName = policeman2.npcName,
                         speakerAvatar = policeman2.npcAvatar,
                         text = "No witnesses saw anything useful, no clear " +
                                "motive, no suspect… nothing." },

      new DialogueLine {
        speakerName = policeman2.npcName, speakerAvatar = policeman2.npcAvatar,
        text = "Feels like we’re missing a piece of the puzzle."
      },

      new DialogueLine { speakerName = policeman2.npcName,
                         speakerAvatar = policeman2.npcAvatar,
                         text = "Maybe you’ll spot something we overlooked. " +
                                "Fresh eyes help sometimes." },

      new DialogueLine { speakerName = policeman2.npcName,
                         speakerAvatar = policeman2.npcAvatar,
                         text =
                             "Just don’t expect me to point you in the right " +
                             "direction. I’m as lost as everyone else." }
    };

    policeman2.SetDialogue(conversation);
    policeman2.npcNotAvailable = false;
  }
  private void Policeman3Conversation() {
    DialogueLine[] conversation = new DialogueLine[] {
      new DialogueLine { speakerName = policeman3.npcName,
                         speakerAvatar = policeman3.npcAvatar,
                         text =
                             "Detective… apparently the victim used to spend " +
                             "a lot of time here at his neighbor’s house." },

      new DialogueLine { speakerName = policeman3.npcName,
                         speakerAvatar = policeman3.npcAvatar,
                         text = "We searched the victim’s place already, but " +
                                "we couldn’t find his phone anywhere." },

      new DialogueLine { speakerName = policeman3.npcName,
                         speakerAvatar = policeman3.npcAvatar,
                         text = "So I was thinking… maybe he left it here by " +
                                "accident before everything happened." },

      new DialogueLine { speakerName = policeman3.npcName,
                         speakerAvatar = policeman3.npcAvatar,
                         text = "If the phone’s still around, it could tell " +
                                "us who he talked to last." },

      new DialogueLine {
        speakerName = policeman3.npcName, speakerAvatar = policeman3.npcAvatar,
        text = "Might be worth checking the rooms around here carefully."
      }
    };

    policeman3.SetDialogue(conversation);
    policeman3.npcNotAvailable = false;
  }
}
