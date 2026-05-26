using UnityEngine;

public class NpcDialogueLogic {
  public string npcName { get; set; }
  public Sprite npcAvatar { get; set; }
  public bool playerIsClose { get; set; }
  public bool dialogueDone { get; private set; }
  public bool npcNotAvailable { get; set; } = true;
  public DialogueLine[] dialogue { get; private set; } = new DialogueLine[0];

  public int Index { get; private set; } = 0;
  public bool IsLastLine => dialogue.Length > 0 && Index >= dialogue.Length - 1;

  // Optional callbacks
  public INpcFinishedCallback QuestManager { get; set; }
  public INpcFinishedCallback CinematicEntry { get; set; }
  public INpcFinishedCallback Cinematic1 { get; set; }

  /// Loads new dialogue and resets state
  public void SetDialogue(DialogueLine[] lines) {
    dialogue = lines;
    Index = 0;
    dialogueDone = false;
  }

  /// Returns the current line, or null if dialogue is empty
  public DialogueLine GetCurrentLine() {
    if (dialogue == null || dialogue.Length == 0)
      return null;
    return dialogue[Index];
  }

  /// Advances to next line. Returns true if advanced, false if at end.
  public bool AdvanceIndex() {
    if (Index < dialogue.Length - 1) {
      Index++;
      return true;
    }
    return false;
  }

  /// Full NextLine logic — mirrors the MonoBehaviour
  public void NextLine() {
    if (!AdvanceIndex()) {
      // Reached end of dialogue
      if (npcNotAvailable == false) {
        dialogueDone = true;
        playerIsClose = false;
        QuestManager?.OnNPCDialogueFinished(this);
        CinematicEntry?.OnNPCDialogueFinished(this);
      }

      if (Cinematic1 != null) {
        dialogueDone = true;
        playerIsClose = false;
        Cinematic1.OnNPCDialogueFinished(this);

        RemoveLine();
      }
    }
  }

  /// Resets index and clears done flag (mirrors RemoveText)
  public void RemoveLine() {
    Index = 0;
    dialogueDone = true;
  }

  /// Returns the correct text alignment for a speaker name
  public TextAlignment GetAlignmentForSpeaker(string speakerName) {
    return speakerName == "You" ? TextAlignment.Right : TextAlignment.Left;
  }
}
