using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

// Fake storage — no PlayerPrefs, no engine
public class FakeKeyStorage : IKeyStorage {
  private Dictionary<string, string> _data = new();
  public string GetString(string key,
                          string def) => _data.TryGetValue(key, out var v)
                                             ? v
                                             : def;
  public void SetString(string key, string value) => _data[key] = value;
}

[TestFixture]
public class RebindKeyTests {
  private RebindKeyLogic _logic;
  private FakeKeyStorage _storage;

  [SetUp]
  public void SetUp() {
    _storage = new FakeKeyStorage();
    _logic = new RebindKeyLogic(_storage);
  }

  // --- GetKey ---

  [Test]
  public void GetKey_ReturnsDefault_WhenNeverSet() {
    KeyCode result = _logic.GetKey("MoveUp");
    Assert.AreEqual(KeyCode.W, result);
  }

  [Test]
  public void GetKey_ReturnsStoredValue_WhenPreviouslySet() {
    _storage.SetString("MoveUp", "T");
    Assert.AreEqual(KeyCode.T, _logic.GetKey("MoveUp"));
  }

  [TestCase("MoveUp", KeyCode.W)]
  [TestCase("MoveDown", KeyCode.S)]
  [TestCase("MoveLeft", KeyCode.A)]
  [TestCase("MoveRight", KeyCode.D)]
  [TestCase("Interact", KeyCode.E)]
  [TestCase("Inventory", KeyCode.I)]
  [TestCase("PauseMenu", KeyCode.Escape)]
  public void GetKey_AllDefaultsAreCorrect(string action, KeyCode expected) {
    Assert.AreEqual(expected, _logic.GetKey(action));
  }

  // --- SetKey ---

  [Test]
  public void SetKey_PersistsToStorage() {
    _logic.SetKey("MoveUp", KeyCode.T);
    Assert.AreEqual("T", _storage.GetString("MoveUp", "None"));
  }

  [Test]
  public void SetKey_ThenGetKey_ReturnsSameKey() {
    _logic.SetKey("Interact", KeyCode.F);
    Assert.AreEqual(KeyCode.F, _logic.GetKey("Interact"));
  }

  // --- IsKeyAvailable ---

  [Test]
  public void IsKeyAvailable_ReturnsFalse_WhenKeyUsedByOtherAction() {
    // S is default for MoveDown — trying to bind MoveUp to S should fail
    bool available =
        _logic.IsKeyAvailable("MoveUp", KeyCode.S, out string conflict);
    Assert.IsFalse(available);
    Assert.AreEqual("MoveDown", conflict);
  }

  [Test]
  public void IsKeyAvailable_ReturnsTrue_ForUnusedKey() {
    bool available =
        _logic.IsKeyAvailable("MoveUp", KeyCode.T, out string conflict);
    Assert.IsTrue(available);
    Assert.IsNull(conflict);
  }

  [Test]
  public void IsKeyAvailable_AllowsSameActionToKeepItsOwnKey() {
    // W is MoveUp's default — rebinding MoveUp to W should be fine
    bool available = _logic.IsKeyAvailable("MoveUp", KeyCode.W, out _);
    Assert.IsTrue(available);
  }

  // --- KeyCodeToInputPath ---

  [Test]
  public void KeyCodeToInputPath_FormatsCorrectly() {
    Assert.AreEqual("<Keyboard>/w", _logic.KeyCodeToInputPath(KeyCode.W));
    Assert.AreEqual("<Keyboard>/escape",
                    _logic.KeyCodeToInputPath(KeyCode.Escape));
  }

  // --- GetMovementBindingIndex ---

  [TestCase("MoveUp", 1)]
  [TestCase("MoveDown", 2)]
  [TestCase("MoveLeft", 3)]
  [TestCase("MoveRight", 4)]
  [TestCase("Interact", -1)]
  [TestCase("Inventory", -1)]
  public void GetMovementBindingIndex_ReturnsCorrectIndex(string action,
                                                          int expected) {
    Assert.AreEqual(expected, _logic.GetMovementBindingIndex(action));
  }
}
