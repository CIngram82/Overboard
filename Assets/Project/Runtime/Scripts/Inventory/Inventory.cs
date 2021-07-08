using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SaveSystem.Data;
using InventorySystem.Collectable;
using InventorySystem.Database;

namespace InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] ItemDatabase _itemDatabase;
        [SerializeField] ClueDatabase _clueDatabase;

        List<Item> _items;
        CollectibleItemSet _collectedWorldItems;
        List<Clue> _clues;
        CollectibleItemSet _collectedWorldClues;

        public ItemDatabase ItemDatabase => _itemDatabase;
        public int Capacity { get; } = 6;
        public List<Item> Items { get => _items; private set => _items = value; }
        public CollectibleItemSet CollectedWorldItems => _collectedWorldItems;
        public List<Clue> Clues { get => _clues; private set => _clues = value; }
        public CollectibleItemSet CollectedWorldClues => _collectedWorldClues;


        #region Items
        public void AddItem(Item item)
        {
            _items.Add(item);
            Debug.Log($"Item {item.Name} added.");
            EventsManager.On_Inventory_Item_Added(item);
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

        }
        public void RemoveAllItems(List<Item> items)
        {
            foreach (Item item in items)
            {
                RemoveItem(item);
            }
        }
        public void DropItem(Item item)
        {
            GameObject itemGO = Instantiate(item.Prefab, transform.position, Quaternion.identity);
            itemGO.transform.Translate(Vector3.down * -2.25f);
            RemoveItem(item);
        }

        public void CombineItems(Item item)
        {
            //_items.
        }
        #endregion

        #region Clues
        public void AddClue(Clue clue)
        {
            _clues.Add(clue);
            EventsManager.On_Inventory_Clue_Added(clue);
        }
        public void AddAllClues(List<Clue> items)
        {
            foreach (Clue clue in items)
            {
                AddClue(clue);
            }
        }
        public void RemoveClue(Clue clue)
        {
            Clues.Remove(clue);
            EventsManager.On_Inventory_Clue_Removed(clue);
        }
        public void RemoveAllClues(List<Clue> items)
        {
            foreach (Clue clue in items)
            {
                RemoveClue(clue);
            }
        }
        #endregion

        public static void RemoveAllUIChildren(Transform parent)
        {
            foreach (Transform item in parent)
            {
                Destroy(item.gameObject);
            }
        }

        void SaveData()
        {
            InventoryData data = SaveDataManager.Save.InventoryData;
            data.CollectibleWorldItems = _collectedWorldItems;
            data.Items = _items.Select(item => item.Name).ToList();

            data.CollectibleWorldClues = _collectedWorldClues;
            data.Clues = _clues;
        }
        void LoadData()
        {
            InventoryData data = SaveDataManager.Save.InventoryData;
            _collectedWorldItems = data.CollectibleWorldItems;
            Items = new List<Item>();
            AddAllItems(data.GetItems(_itemDatabase));
            _collectedWorldClues = data.CollectibleWorldClues;
            Clues = new List<Clue>();
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
