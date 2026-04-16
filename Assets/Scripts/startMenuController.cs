using UnityEngine;
using UnityEngine.SceneManagement;

public class startMenuController : MonoBehaviour {
  public void StartGame() { SceneManager.LoadScene("level-1"); }
}
