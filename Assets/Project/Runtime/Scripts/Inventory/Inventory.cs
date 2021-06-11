using System.Collections.Generic;
using UnityEngine;
using SaveSystem.Data;
using Inventory.Collectable;

namespace Inventory
{
    public class Inventory : MonoBehaviour
    {
        List<Item> _items;
        CollectibleItemSet _collectedWorldItems;
        CollectibleItemSet _collectedClues;

        public int Capacity { get; } = 6;
        public List<Item> Items { get => _items; private set => _items = value; }
        public CollectibleItemSet CollectedWorldItems => _collectedWorldItems;
        public CollectibleItemSet CollectedClues => _collectedClues;


        public void AddItem(Item item)
        {
            _items.Add(item);
            GameEvents.On_Inventory_Item_Added(item);
            Debug.Log("Item added.");
        }
        public void AddAllItems(List<Item> items)
        {
            foreach (Item item in items)
            {
                AddItem(item);
            }
        }
        public void RemoveItem(Item item)
        {
            Items.Remove(item);
            // TODO: add drop item feature if needed.
            Debug.Log("Item removed.");
        }
        public void RemoveAllItems(List<Item> items)
        {
            foreach (Item item in items)
            {
                RemoveItem(item);
            }
        }

        void SaveData()
        {
            SaveDataManager.Save.InventoryData = new InventoryData()
            {
                Items = _items,
                CollectibleItemSet = _collectedWorldItems,
            };
        }
        void LoadData()
        {
            InventoryData data = SaveDataManager.Save.InventoryData;
            _collectedWorldItems = data.CollectibleItemSet;
            Items = data.Items;
        }

        void On_Remove_Item(Item item) => RemoveItem(item);
        void On_SaveData_Loaded() => LoadData();
        void On_SaveData_PreSave() => SaveData();

        void SubToEvents(bool subscribe)
        {
            GameEvents.InventoryItemRemoved -= On_Remove_Item;
            SaveDataManager.SaveDataLoaded -= On_SaveData_Loaded;
            SaveDataManager.DataSavedPrepared -= On_SaveData_PreSave;

            if (subscribe)
            {
                GameEvents.InventoryItemRemoved += On_Remove_Item;
                SaveDataManager.SaveDataLoaded += On_SaveData_Loaded;
                SaveDataManager.DataSavedPrepared += On_SaveData_PreSave;
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
        private void Awake()
        {
            if (SaveDataManager.IsDataLoaded)
                On_SaveData_Loaded();
        }
    }
}
