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
        Debug.Log("Item removed from UI.");
    }

    void On_Add_Item(Item item) => AddUIItem(item);
    void On_Remove_Item(Item item) => RemoveUIItem(item);

    void SubToEvents(bool subscribe)
    {
        EventsManager.InventoryItemAdded -= On_Add_Item;
        EventsManager.InventoryItemRemoved -= On_Remove_Item;

        if (subscribe)
        {
            EventsManager.InventoryItemAdded += On_Add_Item;
            EventsManager.InventoryItemRemoved += On_Remove_Item;
        }
    }

    private void OnEnable()
    {
        SubToEvents(true);
    }
    private void OnDisable()
    {
        SubToEvents(false);
    }
}
