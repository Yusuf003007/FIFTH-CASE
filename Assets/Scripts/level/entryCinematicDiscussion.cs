using UnityEngine;
using UnityEngine.Playables;
using System.Collections;

public class entryCinematicDiscussion : MonoBehaviour {
  public GameObject controlPanel;

  public PlayableDirector director;

  public string playerName = "You";
  public Sprite playerAvatar;
  public npcDialogueController friendInspector;

  private void Awake() {
    director = GetComponent<PlayableDirector>();
    director.played += Director_Played;
    director.stopped += Director_Stopped;
  }

  private void Director_Stopped(PlayableDirector obj) {
    controlPanel.SetActive(true);
  }

  private void Director_Played(PlayableDirector obj) {
    controlPanel.SetActive(false);
  }

  public void StartTimeline() { director.Play(); }

  // PAUSE TIMELINE
  public void PauseTimeline() {
    director.Pause();

    Debug.Log("Timeline Paused");
  }

  // RESUME TIMELINE
  public void ResumeTimeline() {
    director.Resume();

    Debug.Log("Timeline Resumed");
  }
  public void SpawnEnemy() { Debug.Log("Enemy Spawned!"); }

  public void startEntryDiscussion(npcDialogueController npc) {
    playerController2D.Instance.lockPlayer = true;
    entryDiscussion(friendInspector);
    npc.displayDialogue();
  }

  public void OnNPCDialogueFinished(npcDialogueController npc) {
    playerController2D.Instance.lockPlayer = false;

    Debug.Log(npc.npcName + " finished dialogue");
  }

  private void entryDiscussion(npcDialogueController npc) {
    DialogueLine[] conversation = new DialogueLine[] {

      new DialogueLine {
        speakerName = npc.npcName, speakerAvatar = npc.npcAvatar,
        text = "Glad you came. I know you’re retired, but I wouldn’t have " +
               "called if this case didn’t feel strange."
      },

      new DialogueLine {
        speakerName = playerName, speakerAvatar = playerAvatar,
        text = "You said very little on the phone. What exactly happened?"
      },

      new DialogueLine {
        speakerName = npc.npcName, speakerAvatar = npc.npcAvatar,
        text =
            "Victim’s a middle-aged man. Found dead inside his own house. " +
            "Body’s still in the living room where patrol first discovered him."
      },

      new DialogueLine { speakerName = playerName, speakerAvatar = playerAvatar,
                         text = "Any idea on cause of death?" },

      new DialogueLine {
        speakerName = npc.npcName, speakerAvatar = npc.npcAvatar,
        text = "Not yet. Coroner hasn’t arrived. We only secured the scene " +
               "a little while ago."
      },

      new DialogueLine {
        speakerName = npc.npcName, speakerAvatar = npc.npcAvatar,
        text =
            "There are already officers inside keeping the place locked down."
      },

      new DialogueLine {
        speakerName = npc.npcName, speakerAvatar = npc.npcAvatar,
        text = "And... one of our inspectors is not exactly happy you’re here."
      },

      new DialogueLine {
        speakerName = playerName, speakerAvatar = playerAvatar,
        text =
            "Let me guess. Thinks a retired inspector will just get in the way?"
      },

      new DialogueLine {
        speakerName = npc.npcName, speakerAvatar = npc.npcAvatar,
        text = "Something like that. He’s good at his job, just a bit grumpy " +
               "and territorial."
      },

      new DialogueLine { speakerName = playerName, speakerAvatar = playerAvatar,
                         text =
                             "I didn’t come here to step on anyone’s shoes." },

      new DialogueLine { speakerName = npc.npcName,
                         speakerAvatar = npc.npcAvatar,
                         text = "I know. That’s why I called you." },

      new DialogueLine { speakerName = playerName, speakerAvatar = playerAvatar,
                         text = "Anyone see what happened?" },

      new DialogueLine {
        speakerName = npc.npcName, speakerAvatar = npc.npcAvatar,
        text = "We have one witness. Neighbor says they heard shouting earlier."
      },

      new DialogueLine {
        speakerName = npc.npcName, speakerAvatar = npc.npcAvatar,
        text =
            "Witness should still be at their house nearby. You can question " +
            "them whenever you’re ready."
      },

      new DialogueLine {
        speakerName = playerName, speakerAvatar = playerAvatar,
        text = "Alright. First I’ll take a look at the scene, then I’ll have " +
               "a talk with this witness."
      }
    };

    npc.SetDialogue(conversation);
  }
}
