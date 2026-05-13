using UnityEngine;

public class keepGameObject : MonoBehaviour {
  // Start is called once before the first execution of Update after the
  // MonoBehaviour is created
  void Start() {}
  void Awake() {
    if (FindObjectsOfType<AudioManager>().Length > 1) {
      Destroy(gameObject);
      return;
    }

    DontDestroyOnLoad(gameObject);
  }

  // Update is called once per frame
  void Update() {}
}
