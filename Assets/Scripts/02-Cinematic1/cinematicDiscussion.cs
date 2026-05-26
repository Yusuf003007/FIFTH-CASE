using UnityEngine;
using UnityEngine.Playables;
using System.Collections;
using UnityEngine.SceneManagement;

public class FirstCinematicDiscussion : MonoBehaviour {
  // public GameObject questHintPanel;

  // public GameObject controlPanel;
  public PlayableDirector director;
  public string playerName = "You";
  public Sprite playerAvatar;
  public npcDialogueController friendInspector;

  // private void Awake() {
  //   director = GetComponent<PlayableDirector>();
  //   director.played += Director_Played;
  //   director.stopped += Director_Stopped;
  // }

  // private void Director_Stopped(PlayableDirector obj) {
  //   controlPanel.SetActive(true);
  // }

  // private void Director_Played(PlayableDirector obj) {
  //   controlPanel.SetActive(false);
  // }

  // public void StartTimeline() { director.Play(); }

  // // PAUSE TIMELINE
  // public void PauseTimeline() {
  //   director.Pause();

  //   Debug.Log("Timeline Paused");
  // }

  // // RESUME TIMELINE
  // public void ResumeTimeline() {
  //   director.Resume();

  //   Debug.Log("Timeline Resumed");
  // }

  public void SpawnEnemy() { Debug.Log("Enemy Spawned!"); }

  public void startEntryDiscussion(npcDialogueController npc) {
    npc.playerIsClose = true;

    Debug.Log("called entryDiscussion");

    entryDiscussion(friendInspector);
    npc.displayDialogue();
  }

  public void OnNPCDialogueFinished(npcDialogueController npc) {
    npc.playerIsClose = false;

    Debug.Log(npc.npcName +
              " finished dialogue, playerisClose =" + npc.playerIsClose);
    SceneManager.LoadScene("level-1");
  }

  private void entryDiscussion(npcDialogueController npc) {
    Debug.Log("called entryDiscussion");
    DialogueLine[] conversation = new DialogueLine[] {
      new DialogueLine {
        speakerName = npc.npcName, speakerAvatar = npc.npcAvatar,
        text = "I'm glad you picked up. I wouldn't have called if I didn't " +
               "need you on this one."
      },
      new DialogueLine { speakerName = playerName, speakerAvatar = playerAvatar,
                         text = "It's been a while. What's going on?" },
      new DialogueLine {
        speakerName = npc.npcName, speakerAvatar = npc.npcAvatar,
        text =
            "We've got a body. Middle-aged man, found dead inside his home. " +
            "Something about it doesn't sit right with me."
      },
      new DialogueLine { speakerName = playerName, speakerAvatar = playerAvatar,
                         text = "You've got a full team. Why call me?" },
      new DialogueLine {
        speakerName = npc.npcName, speakerAvatar = npc.npcAvatar,
        text = "Because I trust your eyes more than anyone else's. " +
               "You always saw things the rest of us missed."
      },
      new DialogueLine { speakerName = playerName, speakerAvatar = playerAvatar,
                         text = "I'm retired, you know that." },
      new DialogueLine {
        speakerName = npc.npcName, speakerAvatar = npc.npcAvatar,
        text = "I know. And I wouldn't be asking if I had another option. " +
               "Just come take a look. That's all I'm asking."
      },
      new DialogueLine { speakerName = playerName, speakerAvatar = playerAvatar,
                         text = "..." },
      new DialogueLine {
        speakerName = playerName, speakerAvatar = playerAvatar,
        text = "Alright. Send me the address. I'll head over now."
      },
      new DialogueLine { speakerName = npc.npcName,
                         speakerAvatar = npc.npcAvatar,
                         text = "Thank you. I'll call when you will be there" },
    };
    npc.SetDialogue(conversation);
  }
}
