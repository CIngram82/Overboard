using System.Collections.Generic;
using UnityEngine;
using Inventory.Database;

namespace Inventory.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] ItemUI _uiItemPrefab;
        [SerializeField] Transform _uiItemsParent;
        [SerializeField] List<ItemUI> _uiItems;

        public List<ItemUI> UIItems { get => _uiItems; private set => _uiItems = value; }


        void AddUIItem(Item item)
        {
            ItemUI itemUI = Instantiate(_uiItemPrefab, _uiItemsParent);
            itemUI.Setup(item);
            UIItems.Add(itemUI);
            Debug.Log($"Item added {itemUI.Item.Name} to UI.");
        }
        void RemoveUIItem(Item item)
        {
            ItemUI itemUI = UIItems.Find(x => x.Item.Name == item.Name);
            UIItems.Remove(itemUI);
            Destroy(itemUI.gameObject);
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
        void Awake()
        {
            SubToEvents(true);
        }
    } 
}
