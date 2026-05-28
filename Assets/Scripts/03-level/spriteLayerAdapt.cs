using UnityEngine;

public class ZoneSortingChanger : MonoBehaviour {
  [Header("Target to change")]
  public SpriteRenderer targetRenderer; // drag your sprite here

  [Header("Sorting settings")]
  public int orderInsideZone = 5;
  public int orderOutsideZone = 0;
  // Optional: also switch sorting layer name
  public string layerInsideZone = "Character";
  public string layerOutsideZone = "Character";

  private void OnTriggerEnter2D(Collider2D other) {
    if (!other.CompareTag("Player"))
      return;

    targetRenderer.sortingOrder = orderInsideZone;
    targetRenderer.sortingLayerName = layerInsideZone; // remove if not needed
  }

  private void OnTriggerExit2D(Collider2D other) {
    if (!other.CompareTag("Player"))
      return;

    targetRenderer.sortingOrder = orderOutsideZone;
    targetRenderer.sortingLayerName = layerOutsideZone; // remove if not needed
  }
}
