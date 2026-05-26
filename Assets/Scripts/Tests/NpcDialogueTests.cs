using NUnit.Framework;
using UnityEngine;

// Fake callback tracker
public class FakeCallback : INpcFinishedCallback {
  public bool WasCalled { get; private set; }
  public NpcDialogueLogic LastNpc { get; private set; }
  public void OnNPCDialogueFinished(NpcDialogueLogic npc) {
    WasCalled = true;
    LastNpc = npc;
  }
}

[TestFixture]
public class NpcDialogueTests {
  private NpcDialogueLogic _logic;

  private DialogueLine[] MakeDialogue(int count) {
    var lines = new DialogueLine[count];
    for (int i = 0; i < count; i++)
      lines[i] =
          new DialogueLine { speakerName = i % 2 == 0 ? "Inspector" : "You",
                             text = $"Line {i}" };
    return lines;
  }

  [SetUp]
  public void SetUp() {
    _logic = new NpcDialogueLogic { npcName = "Inspector Roux" };
  }

  // --- SetDialogue ---

  [Test]
  public void SetDialogue_ResetsIndexToZero() {
    _logic.SetDialogue(MakeDialogue(3));
    _logic.AdvanceIndex();
    _logic.SetDialogue(MakeDialogue(3)); // reload
    Assert.AreEqual(0, _logic.Index);
  }

  [Test]
  public void SetDialogue_StoresAllLines() {
    var lines = MakeDialogue(5);
    _logic.SetDialogue(lines);
    Assert.AreEqual(5, _logic.dialogue.Length);
  }

  // --- AdvanceIndex ---

  [Test]
  public void AdvanceIndex_IncreasesIndexByOne() {
    _logic.SetDialogue(MakeDialogue(3));
    _logic.AdvanceIndex();
    Assert.AreEqual(1, _logic.Index);
  }

  [Test]
  public void AdvanceIndex_ReturnsFalse_WhenAtLastLine() {
    _logic.SetDialogue(MakeDialogue(2));
    _logic.AdvanceIndex(); // go to index 1 (last)
    bool result = _logic.AdvanceIndex();
    Assert.IsFalse(result);
  }

  [Test]
  public void AdvanceIndex_ReturnsTrue_WhenNotAtLastLine() {
    _logic.SetDialogue(MakeDialogue(3));
    bool result = _logic.AdvanceIndex();
    Assert.IsTrue(result);
  }

  // --- GetCurrentLine ---

  [Test]
  public void GetCurrentLine_ReturnsFirstLine_AtStart() {
    var lines = MakeDialogue(3);
    _logic.SetDialogue(lines);
    Assert.AreEqual("Line 0", _logic.GetCurrentLine().text);
  }

  [Test]
  public void GetCurrentLine_ReturnsCorrectLine_AfterAdvance() {
    _logic.SetDialogue(MakeDialogue(3));
    _logic.AdvanceIndex();
    Assert.AreEqual("Line 1", _logic.GetCurrentLine().text);
  }

  [Test]
  public void GetCurrentLine_ReturnsNull_WhenDialogueEmpty() {
    _logic.SetDialogue(new DialogueLine[0]);
    Assert.IsNull(_logic.GetCurrentLine());
  }

  // --- NextLine / end of dialogue ---

  [Test]
  public void NextLine_SetsDialogueDone_WhenNpcAvailableAndAtEnd() {
    _logic.SetDialogue(MakeDialogue(1)); // 1 line = already at end
    _logic.npcNotAvailable = false;
    _logic.NextLine();
    Assert.IsTrue(_logic.dialogueDone);
  }

  [Test]
  public void NextLine_DoesNotSetDialogueDone_WhenNpcNotAvailable() {
    _logic.SetDialogue(MakeDialogue(1));
    _logic.npcNotAvailable = true;
    _logic.NextLine();
    Assert.IsFalse(_logic.dialogueDone);
  }

  [Test]
  public void NextLine_SetsPlayerIsCloseFalse_AtEndOfDialogue() {
    _logic.SetDialogue(MakeDialogue(1));
    _logic.npcNotAvailable = false;
    _logic.playerIsClose = true;
    _logic.NextLine();
    Assert.IsFalse(_logic.playerIsClose);
  }

  [Test]
  public void NextLine_CallsQuestManagerCallback_AtEnd() {
    var fake = new FakeCallback();
    _logic.QuestManager = fake;
    _logic.npcNotAvailable = false;
    _logic.SetDialogue(MakeDialogue(1));
    _logic.NextLine();
    Assert.IsTrue(fake.WasCalled);
  }

  [Test]
  public void NextLine_CallsCinematic1Callback_AtEnd() {
    var fake = new FakeCallback();
    _logic.Cinematic1 = fake;
    _logic.SetDialogue(MakeDialogue(1));
    _logic.NextLine();
    Assert.IsTrue(fake.WasCalled);
  }

  [Test]
  public void NextLine_DoesNotCallQuestManager_IfNotAtEnd() {
    var fake = new FakeCallback();
    _logic.QuestManager = fake;
    _logic.npcNotAvailable = false;
    _logic.SetDialogue(MakeDialogue(3));
    _logic.NextLine(); // index 0 → 1, not at end
    Assert.IsFalse(fake.WasCalled);
  }

  // --- ResetState ---

  [Test]
  public void ResetState_ResetsIndexToZero() {
    _logic.SetDialogue(MakeDialogue(3));
    _logic.AdvanceIndex();
    _logic.RemoveLine();
    Assert.AreEqual(0, _logic.Index);
  }

  // --- GetAlignmentForSpeaker ---

  [Test]
  public void GetAlignmentForSpeaker_ReturnsRight_ForPlayer() {
    Assert.AreEqual(TextAlignment.Right, _logic.GetAlignmentForSpeaker("You"));
  }

  [Test]
  public void GetAlignmentForSpeaker_ReturnsLeft_ForNpc() {
    Assert.AreEqual(TextAlignment.Left,
                    _logic.GetAlignmentForSpeaker("Inspector Roux"));
  }

  [Test]
  public void GetAlignmentForSpeaker_ReturnsLeft_ForUnknownSpeaker() {
    Assert.AreEqual(TextAlignment.Left,
                    _logic.GetAlignmentForSpeaker("Stranger"));
  }
}
