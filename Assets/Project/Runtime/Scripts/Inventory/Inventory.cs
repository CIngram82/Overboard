using System.Collections.Generic;
using UnityEngine;
using SaveSystem.Data;
using Inventory.Collectable;
using Inventory.Database;

namespace Inventory
{
    public class Inventory : MonoBehaviour
    {
        List<Item> _items;
        CollectibleItemSet _collectedWorldItems;
        List<Clue> _clues;
        CollectibleItemSet _collectedClues;

        public int Capacity { get; } = 6;
        public List<Item> Items { get => _items; private set => _items = value; }
        public CollectibleItemSet CollectedWorldItems => _collectedWorldItems;
        public List<Clue> Clues { get => _clues; private set => _clues = value; }
        public CollectibleItemSet CollectedClues => _collectedClues;


        public void AddItem(Item item)
        {
            _items.Add(item);
            EventsManager.On_Inventory_Item_Added(item);
            Debug.Log($"Item {item.Name} added.");
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
            EventsManager.On_Inventory_Item_Removed(item);
            // TODO: add drop item feature if needed.
            Debug.Log($"Item {item.Name} removed.");
        }
        public void RemoveAllItems(List<Item> items)
        {
            foreach (Item item in items)
            {
                RemoveItem(item);
            }
        }
       
        public void AddClue(Clue clue)
        {
            _clues.Add(clue);
            EventsManager.On_Inventory_Clue_Added(clue);
            Debug.Log($"Clue {clue.Name} added.");
        }
        public void AddAllClues(List<Clue> items)
        {
            foreach (Clue clue in items)
            {
                AddClue(clue);
            }
        }
        public void RemoveItem(Clue clue)
        {
            Clues.Remove(clue);
            EventsManager.On_Inventory_Clue_Removed(clue);
            Debug.Log($"Clue {clue.Name} removed.");
        }
        public void RemoveAllClues(List<Clue> items)
        {
            foreach (Clue clue in items)
            {
                RemoveItem(clue);
            }
        }

        void SaveData()
        {
            InventoryData data = SaveDataManager.Save.InventoryData;
            data.CollectibleWorldItems = _collectedWorldItems;
            data.Items = _items;
            data.CollectibleClues = _collectedClues;
            data.Clues = _clues;
        }
        void LoadData()
        {
            InventoryData data = SaveDataManager.Save.InventoryData;
            _collectedWorldItems = data.CollectibleWorldItems;
            AddAllItems(data.Items);
            _collectedClues = data.CollectibleClues;
            AddAllClues(data.Clues);
        }

        void On_SaveData_Loaded() => LoadData();
        void On_SaveData_PreSave() => SaveData();

        void SubToEvents(bool subscribe)
        {
            SaveDataManager.SaveDataLoaded -= On_SaveData_Loaded;
            SaveDataManager.DataSavedPrepared -= On_SaveData_PreSave;

            if (subscribe)
            {
                SaveDataManager.SaveDataLoaded += On_SaveData_Loaded;
                SaveDataManager.DataSavedPrepared += On_SaveData_PreSave;
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
            if (SaveDataManager.IsDataLoaded)
                On_SaveData_Loaded();
        }
    }
}
