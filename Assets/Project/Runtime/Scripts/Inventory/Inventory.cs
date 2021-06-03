using System;
using System.Collections.Generic;
using UnityEngine;
using SaveSystem;
using SaveSystem.Data;
using Inventory.Collectable;

namespace Inventory
{
    public class Inventory : MonoBehaviour
    {
        List<Item> _items;
        CollectibleItemSet _collectibleItemSet;

        public int Capacity { get; } = 6;
        public List<Item> Items => _items; 
        public CollectibleItemSet CollectibleItemSet => _collectibleItemSet; 


        public void AddItem(Item item)
        {
            _items.Add(item);
            GameEvents.On_Inventory_Item_Added(item);
            Debug.Log("Item added.");
        }
        public void AddItems(List<Item> items)
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
            GameEvents.On_Inventory_Item_Removed(item);
            Debug.Log("Item removed.");
        }
        public void RemoveItemAt(int index)
        {
            Items.RemoveAt(index);
            // TODO: add drop item feature if needed.
            //GameEvents.On_Inventory_Item_Removed(item);
            Debug.Log("Item removed.");
        }

        void SaveData()
        {
            SaveDataManager.Save.InventoryData = new InventoryData()
            {
                Items = _items,
                CollectibleItemSet = _collectibleItemSet,
            };
        }
        void LoadData()
        {
            InventoryData data = SaveDataManager.Save.InventoryData;
            _collectibleItemSet = data.CollectibleItemSet;
            AddItems(data.Items);
        }

        void On_SaveData_Loaded() => LoadData();
        void On_SaveData_PreSave() => SaveData();

        void SubToEvents(bool subscribe)
        {
            SaveDataManager.SaveDataLoaded += On_SaveData_Loaded;
            SaveDataManager.DataSavedPrepared += On_SaveData_PreSave;

            if (subscribe)
            {
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
