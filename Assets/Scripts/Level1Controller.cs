using UnityEngine;

public class Level1Controller : MonoBehaviour

{
  public NPCDialogueController npc1;
  public NPCDialogueController npc2;

  void Start() {
    ChangeAllDialogues();
    InvokeRepeating("PrintState", 10f, 10f);
  }

  public void ChangeAllDialogues() {
    npc1.SetDialogue(new string[] { "Hi, I'm NPC 1!", "My dialogue changed!" });

    npc2.SetDialogue(new string[] { "Hello from NPC 2!", "I also changed!" });
    Debug.Log("Change dialogue");
  }

  void PrintState() { Debug.Log($"State of NPC 1: {npc1.state}"); }
}
