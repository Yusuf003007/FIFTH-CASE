using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.InputSystem; // for player key press

public class InventoryController : MonoBehaviour {
  public static InventoryController Instance;

  public List<int> inventory = new List<int>();
  public GameObject Panel;
  public Text textPanel;
  public Transform inventoryPanel;  // parent UI panel
  public GameObject itemSlotPrefab; // prefab with Image

  void Update() {
    if (Keyboard.current.escapeKey.wasPressedThisFrame) {
      inventoryPanel.gameObject.SetActive(
          !inventoryPanel.gameObject.activeSelf);
      if (inventoryPanel.gameObject.activeSelf)
        RefreshUI();
    }
  }
  void Awake() { Instance = this; }

  public void RefreshUI() {
    // Clear old UI
    foreach (Transform child in inventoryPanel) {
      Destroy(child.gameObject);
    }

    // Loop inventory
    foreach (int id in inventory) {
      ItemData item = ItemDatabase.Instance.GetItemById(id);

      if (item != null) {
        GameObject slot = Instantiate(itemSlotPrefab, inventoryPanel);

        Image img = slot.GetComponent<Image>();
        img.sprite = item.avatar;
        img.enabled = true;
      }
    }
  }

  public void AddItem(int id) {
    inventory.Add(id);

    ItemData item = ItemDatabase.Instance.GetItemById(id);

    if (item != null) {
      // Debug.Log("Picked up: " + item.name);

      Panel.SetActive(true);

      textPanel.text = "Item " + item.name + " added to the inventory";

      StartCoroutine(HidePanelAfterDelay(3f));
    } else {
      // Debug.LogWarning("Item ID not found: " + id);
    }
  }

  private IEnumerator HidePanelAfterDelay(float delay) {
    // Debug.Log("Coroutine started");
    yield return new WaitForSeconds(delay);
    // Debug.Log("Coroutine finished");
    Panel.SetActive(false);
  }
  public bool HasItems(int[] requiredItems) {
    foreach (int id in requiredItems) {
      if (!inventory.Contains(id)) {
        return false;
      }
    }

    return true;
  }
}
