using NUnit.Framework;
using UnityEngine;

public class BrightnessTests {
  private BrightnessLogic _brightness;

  [SetUp]
  public void SetUp() {
    _brightness = new BrightnessLogic();
    PlayerPrefs.DeleteAll();
  }

  [TearDown]
  public void TearDown() { PlayerPrefs.DeleteAll(); }

  // --- Default value ---

  [Test]
  public void GetBrightness_ReturnsDefault_WhenNotSet() {
    Assert.AreEqual(0.5f, _brightness.GetBrightness());
  }

  // --- Save and Get ---

  [Test]
  public void SaveBrightness_SavesCorrectly() {
    _brightness.SaveBrightness(0.8f);
    Assert.AreEqual(0.8f, _brightness.GetBrightness());
  }

  [Test]
  public void SaveBrightness_Overwrite_UpdatesValue() {
    _brightness.SaveBrightness(0.3f);
    _brightness.SaveBrightness(0.9f);
    Assert.AreEqual(0.9f, _brightness.GetBrightness());
  }

  // --- Edge cases ---

  [Test]
  public void SaveBrightness_Zero_SavesCorrectly() {
    _brightness.SaveBrightness(0f);
    Assert.AreEqual(0f, _brightness.GetBrightness());
  }

  [Test]
  public void SaveBrightness_One_SavesCorrectly() {
    _brightness.SaveBrightness(1f);
    Assert.AreEqual(1f, _brightness.GetBrightness());
  }

  // --- Alpha calculation ---

  [Test]
  public void CalculateAlpha_ReturnsCorrectValue() {
    float alpha = _brightness.CalculateAlpha(0.8f);
    Assert.AreEqual(0.2f, alpha, 0.001f); // 0.001f = float tolerance
  }

  [Test]
  public void CalculateAlpha_WhenZero_ReturnsOne() {
    float alpha = _brightness.CalculateAlpha(0f);
    Assert.AreEqual(1f, alpha);
  }

  [Test]
  public void CalculateAlpha_WhenOne_ReturnsZero() {
    float alpha = _brightness.CalculateAlpha(1f);
    Assert.AreEqual(0f, alpha, 0.001f);
  }
}
