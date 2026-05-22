using NUnit.Framework;
using UnityEngine;

public class NewTestScript {
  private AudioVolumeLogic _audio;

  [SetUp]
  public void SetUp() {
    _audio = new AudioVolumeLogic();
    PlayerPrefs.DeleteAll();
  }

  [TearDown]
  public void TearDown() { PlayerPrefs.DeleteAll(); }

  [Test]
  public void SetMasterVolume_SavesCorrectly() {
    _audio.SetMasterVolume(0.5f);
    Assert.AreEqual(0.5f, _audio.GetMasterVolume());
  }

  [Test]
  public void GetMasterVolume_ReturnsDefault_WhenNotSet() {
    Assert.AreEqual(1f, _audio.GetMasterVolume());
  }
}
