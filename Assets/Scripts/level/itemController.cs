using UnityEngine;
using System.Collections.Generic;

public class ItemController : MonoBehaviour {
  public int itemId;

  private void OnTriggerEnter2D(Collider2D other) {
    if (other.CompareTag("Player")) {
      InventoryController.Instance.AddItem(itemId);
      Destroy(gameObject); // optional: remove item from world
    }
  }
}
