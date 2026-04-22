using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.InputSystem; // for player key press

public class InventoryController : MonoBehaviour {
  public static InventoryController Instance;

  public List<int> inventory = new List<int>();

  [Header("In game notification")]
  public GameObject inventoryNofification;
  public Text inventoryNotificationMesssage;

  [Header("Inventory Menu")]
  public GameObject inventoryMenu;
  public Transform inventoryPanelFlexbox; // parent UI containing the item
  public GameObject itemSlotPrefab;       // prefab with Image

  void Update() {
    if (Keyboard.current.escapeKey.wasPressedThisFrame) {
      inventoryMenu.gameObject.SetActive(!inventoryMenu.gameObject.activeSelf);
      if (inventoryMenu.gameObject.activeSelf)
        RefreshUI();
    }
  }
  void Awake() { Instance = this; }

  private void RefreshUI() {
    // Clear old UI
    for (int i = inventoryPanelFlexbox.childCount - 1; i >= 0; i--) {
      DestroyImmediate(inventoryPanelFlexbox.GetChild(i).gameObject);
    }
    Debug.Log("Inventory content: " + string.Join(", ", inventory));
    //  Loop inventory
    foreach (int id in inventory) {
      ItemData item = ItemDatabase.Instance.GetItemById(id);

      if (item != null) {
        GameObject slot = Instantiate(itemSlotPrefab, inventoryPanelFlexbox);
        Debug.Log("Created slot: " + slot.name);

        Image img = slot.GetComponent<Image>();
        img.sprite = item.avatar;
        img.enabled = true;

        Text txt = slot.GetComponentInChildren<Text>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(
            txt.GetComponent<RectTransform>());
        txt.text = item.name; // or whatever field you have
        txt.enabled = true;
        Debug.Log("item text: " + item.name);
      }
    }
  }

  public void AddItem(int id) {
    inventory.Add(id);

    ItemData item = ItemDatabase.Instance.GetItemById(id);

    if (item != null) {
      // Debug.Log("Picked up: " + item.name);

      inventoryNofification.SetActive(true);

      inventoryNotificationMesssage.text =
          "Item " + item.name + " added to the inventory";

      StartCoroutine(HidePanelAfterDelay(3f));
    } else {
      // Debug.LogWarning("Item ID not found: " + id);
    }
  }

  private IEnumerator HidePanelAfterDelay(float delay) {
    // Debug.Log("Coroutine started");
    yield return new WaitForSeconds(delay);
    // Debug.Log("Coroutine finished");
    inventoryNofification.SetActive(false);
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
