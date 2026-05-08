using UnityEngine;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour {
  public static ItemDatabase Instance;

  public List<ItemData> items = new List<ItemData>();

  void Awake() { Instance = this; }

  public ItemData GetItemById(int id) {
    return items.Find(item => item.id == id);
  }
}
