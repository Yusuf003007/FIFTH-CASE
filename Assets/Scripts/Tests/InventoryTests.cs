using NUnit.Framework;
using System.Collections.Generic;

public class FakeQuestManager : IQuestManager {
  public int CallCount { get; private set; }
  public void checkQuestStage() => CallCount++;
}

[TestFixture]
public class InventoryTests {
  private InventoryLogic _logic;
  private FakeQuestManager _questManager;

  [SetUp]
  public void SetUp() {
    _questManager = new FakeQuestManager();
    _logic = new InventoryLogic(_questManager);
  }

  // --- AddItem ---

  [Test]
  public void AddItem_AddsIdToInventory() {
    _logic.AddItem(1);
    Assert.Contains(1, _logic.inventory);
  }

  [Test]
  public void AddItem_IncreasesInventoryCount() {
    _logic.AddItem(1);
    _logic.AddItem(2);
    Assert.AreEqual(2, _logic.inventory.Count);
  }

  [Test]
  public void AddItem_AllowsDuplicateItems() {
    _logic.AddItem(1);
    _logic.AddItem(1);
    Assert.AreEqual(2, _logic.inventory.Count);
  }

  [Test]
  public void AddItem_CallsQuestManager() {
    _logic.AddItem(1);
    Assert.AreEqual(1, _questManager.CallCount);
  }

  [Test]
  public void AddItem_CallsQuestManager_EachTime() {
    _logic.AddItem(1);
    _logic.AddItem(2);
    Assert.AreEqual(2, _questManager.CallCount);
  }

  [Test]
  public void AddItem_WorksWithoutQuestManager() {
    var logicNoQuest = new InventoryLogic(null);
    Assert.DoesNotThrow(() => logicNoQuest.AddItem(1));
  }

  // --- HasItems ---

  [Test]
  public void HasItems_ReturnsTrue_WhenAllItemsPresent() {
    _logic.AddItem(1);
    _logic.AddItem(2);
    Assert.IsTrue(_logic.HasItems(new int[] { 1, 2 }));
  }

  [Test]
  public void HasItems_ReturnsFalse_WhenOneItemMissing() {
    _logic.AddItem(1);
    Assert.IsFalse(_logic.HasItems(new int[] { 1, 2 }));
  }

  [Test]
  public void HasItems_ReturnsFalse_WhenInventoryEmpty() {
    Assert.IsFalse(_logic.HasItems(new int[] { 1 }));
  }

  [Test]
  public void HasItems_ReturnsTrue_WhenRequiredListIsEmpty() {
    Assert.IsTrue(_logic.HasItems(new int[] {}));
  }

  [Test]
  public void HasItems_ReturnsFalse_WhenNoneOfTheItemsPresent() {
    _logic.AddItem(1);
    Assert.IsFalse(_logic.HasItems(new int[] { 2, 3 }));
  }

  // --- Clear ---

  [Test]
  public void Clear_EmptiesInventory() {
    _logic.AddItem(1);
    _logic.AddItem(2);
    _logic.Clear();
    Assert.AreEqual(0, _logic.inventory.Count);
  }

  [Test]
  public void Clear_OnEmptyInventory_DoesNotThrow() {
    Assert.DoesNotThrow(() => _logic.Clear());
  }
}
