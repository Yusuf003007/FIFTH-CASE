using System.Collections.Generic;

public class ItemDatabaseLogic {
  private readonly List<ItemData> _items;

  public ItemDatabaseLogic(List<ItemData> items) { _items = items; }

  public ItemData GetItemById(int id) {
    return _items.Find(item => item.id == id);
  }
}
