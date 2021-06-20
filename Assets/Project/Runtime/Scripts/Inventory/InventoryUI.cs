using System.Collections.Generic;
using UnityEngine;
using Inventory.Database;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] ItemUI _uiItemPrefab;
    [SerializeField] Transform _uiItemsParent;
    [SerializeField] List<ItemUI> _uiItems;

    public List<ItemUI> UIItems { get => _uiItems; private set => _uiItems = value; }


    void AddUIItem(Item item)
    {
        ItemUI uiItemInstance = Instantiate(_uiItemPrefab, _uiItemsParent);
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

    void OnEnable()
    {
        SubToEvents(true);
    }
    void OnDisable()
    {
        SubToEvents(false);
    }
    void Start()
    {
        Inventory.Inventory.RemoveAllUIChildren(_uiItemsParent);
    }
}
