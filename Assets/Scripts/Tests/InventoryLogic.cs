using System.Collections.Generic;

public class InventoryLogic {
  public List<int> inventory { get; private set; } = new List<int>();

  private readonly IQuestManager _questManager;

  public InventoryLogic(IQuestManager questManager = null) {
    _questManager = questManager;
  }

  public void AddItem(int id) {
    inventory.Add(id);
    _questManager?.checkQuestStage();
  }

  public bool HasItems(int[] requiredItems) {
    foreach (int id in requiredItems)
      if (!inventory.Contains(id))
        return false;
    return true;
  }

  public void Clear() { inventory.Clear(); }
}
