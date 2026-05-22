using NUnit.Framework;
using UnityEngine;

public class AudioVolumeTests {
  private AudioVolumeLogic _audio;

  [SetUp]
  public void SetUp() {
    _audio = new AudioVolumeLogic();
    // Clean slate before every test
    PlayerPrefs.DeleteAll();
  }

  [TearDown]
  public void TearDown() {
    // Clean up after every test
    PlayerPrefs.DeleteAll();
  }

  // --- Default values ---

  [Test]
  public void GetMasterVolume_ReturnsDefault_WhenNotSet() {
    Assert.AreEqual(1f, _audio.GetMasterVolume());
  }

  [Test]
  public void GetMusicVolume_ReturnsDefault_WhenNotSet() {
    Assert.AreEqual(1f, _audio.GetMusicVolume());
  }

  [Test]
  public void GetSFXVolume_ReturnsDefault_WhenNotSet() {
    Assert.AreEqual(1f, _audio.GetSFXVolume());
  }

  // --- Set then Get ---

  [Test]
  public void SetMasterVolume_SavesCorrectly() {
    _audio.SetMasterVolume(0.5f);
    Assert.AreEqual(0.5f, _audio.GetMasterVolume());
  }

  [Test]
  public void SetMusicVolume_SavesCorrectly() {
    _audio.SetMusicVolume(0.3f);
    Assert.AreEqual(0.3f, _audio.GetMusicVolume());
  }

  [Test]
  public void SetSFXVolume_SavesCorrectly() {
    _audio.SetSFXVolume(0.8f);
    Assert.AreEqual(0.8f, _audio.GetSFXVolume());
  }

  // --- Edge cases ---

  [Test]
  public void SetMasterVolume_Zero_SavesCorrectly() {
    _audio.SetMasterVolume(0f);
    Assert.AreEqual(0f, _audio.GetMasterVolume());
  }

  [Test]
  public void SetMasterVolume_One_SavesCorrectly() {
    _audio.SetMasterVolume(1f);
    Assert.AreEqual(1f, _audio.GetMasterVolume());
  }

  [Test]
  public void SetVolumes_DoNotAffectEachOther() {
    _audio.SetMasterVolume(0.2f);
    _audio.SetMusicVolume(0.5f);
    _audio.SetSFXVolume(0.8f);

    Assert.AreEqual(0.2f, _audio.GetMasterVolume());
    Assert.AreEqual(0.5f, _audio.GetMusicVolume());
    Assert.AreEqual(0.8f, _audio.GetSFXVolume());
  }

  // --- Overwrite ---

  [Test]
  public void SetMasterVolume_Overwrite_UpdatesValue() {
    _audio.SetMasterVolume(0.3f);
    _audio.SetMasterVolume(0.9f);
    Assert.AreEqual(0.9f, _audio.GetMasterVolume());
  }
}
