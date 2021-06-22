using System.Collections.Generic;
using Inventory.Collectable;
using Inventory.Database;

namespace SaveSystem.Data
{
    [System.Serializable]
    public class InventoryData
    {
        //public List<Item> Items;
        //public List<string> Clues;
        public List<Clue> Clues;
        public List<string> Items;
        public CollectibleItemSet CollectibleWorldItems;
        public CollectibleItemSet CollectibleWorldClues;

        public InventoryData()
        {
            Items = new List<string>();
            Clues = new List<Clue>();
            CollectibleWorldItems = new CollectibleItemSet();
            CollectibleWorldClues = new CollectibleItemSet();
        }

        public List<Item> GetItems(ItemDatabase database)
        {
            List<Item> items = new List<Item>();
            foreach (var item in Items)
            {
                items.Add(database.GetInventoryItem(item));
            }
            return items;
        }
    }
}
