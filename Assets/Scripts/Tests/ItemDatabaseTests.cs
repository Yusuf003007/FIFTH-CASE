using NUnit.Framework;
using System.Collections.Generic;

[TestFixture]
public class ItemDatabaseTests {
  private ItemDatabaseLogic _logic;

  private List<ItemData> MakeDatabase() {
    return new List<ItemData> {
      new ItemData { id = 1, name = "Key", description = "An old key" },
      new ItemData { id = 2, name = "Knife", description = "A sharp knife" },
      new ItemData { id = 3, name = "Letter", description = "A torn letter" },
    };
  }

  [SetUp]
  public void SetUp() { _logic = new ItemDatabaseLogic(MakeDatabase()); }

  // --- GetItemById ---

  [Test]
  public void GetItemById_ReturnsCorrectItem_WhenIdExists() {
    ItemData result = _logic.GetItemById(1);
    Assert.AreEqual("Key", result.name);
  }

  [Test]
  public void GetItemById_ReturnsNull_WhenIdDoesNotExist() {
    ItemData result = _logic.GetItemById(99);
    Assert.IsNull(result);
  }

  [Test]
  public void GetItemById_ReturnsNull_WhenDatabaseIsEmpty() {
    var emptyLogic = new ItemDatabaseLogic(new List<ItemData>());
    Assert.IsNull(emptyLogic.GetItemById(1));
  }

  [Test]
  public void GetItemById_ReturnsCorrectItem_ForEachId() {
    Assert.AreEqual("Key", _logic.GetItemById(1).name);
    Assert.AreEqual("Knife", _logic.GetItemById(2).name);
    Assert.AreEqual("Letter", _logic.GetItemById(3).name);
  }

  [Test]
  public void GetItemById_ReturnsFirstMatch_WhenDuplicateIdsExist() {
    var duplicates = new List<ItemData> {
      new ItemData { id = 1, name = "First", description = "First item" },
      new ItemData { id = 1, name = "Second", description = "Second item" },
    };
    var logic = new ItemDatabaseLogic(duplicates);
    Assert.AreEqual("First", logic.GetItemById(1).name);
  }

  [Test]
  public void GetItemById_ReturnsItemWithCorrectDescription() {
    ItemData result = _logic.GetItemById(2);
    Assert.AreEqual("A sharp knife", result.description);
  }

  [Test]
  public void GetItemById_ReturnsNull_ForNegativeId() {
    Assert.IsNull(_logic.GetItemById(-1));
  }

  [Test]
  public void GetItemById_ReturnsNull_ForZeroId() {
    Assert.IsNull(_logic.GetItemById(0));
  }
}
