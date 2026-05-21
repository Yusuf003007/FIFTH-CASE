using UnityEngine;
using UnityEngine.Playables;

public class DisableSelfOnEnd : MonoBehaviour {
  public PlayableDirector director;

  void Start() { director.stopped += OnTimelineFinished; }

  void OnTimelineFinished(PlayableDirector pd) { gameObject.SetActive(false); }
}
