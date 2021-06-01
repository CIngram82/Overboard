using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] ItemUI _uiItem;

    public List<ItemUI> UIItems { get; private set; } = new List<ItemUI>();

    void AddUIItem(Item item)
    {
        ItemUI uiItemInstance = Instantiate(_uiItem, transform);
        uiItemInstance.Setup(item);
        UIItems.Add(uiItemInstance);
        Debug.Log("Item added to UI.");
    }
    void RemoveUIItem(Item item)
    {
        ItemUI uIItem = UIItems.Find(x => x.Item.Name == item.Name);
        UIItems.Remove(uIItem);
        Destroy(uIItem.gameObject);
        Debug.Log("Item Removed from UI.");
    }
    void RemoveUIItemAt(int index)
    {
        ItemUI uIItem = UIItems[index];
        UIItems.Remove(uIItem);
        Destroy(uIItem.gameObject);
        Debug.Log("Item Removed from UI.");
    }

    public void SubscribeToEvents()
    {
        GameEvents.InventoryItemAdded += AddUIItem;
        GameEvents.InventoryItemRemoved += RemoveUIItem;
    }
    public void UnsubscribeFromEvents()
    {
        GameEvents.InventoryItemAdded -= AddUIItem;
        GameEvents.InventoryItemRemoved -= RemoveUIItem;
    }

    private void OnEnable()
    {
        SubscribeToEvents();
    }
    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }
}
