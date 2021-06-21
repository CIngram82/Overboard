using System.Collections.Generic;
using Inventory.Collectable;
using Inventory.Database;

namespace SaveSystem.Data
{
    [System.Serializable]
    public class InventoryData
    {
        public List<Item> Items;
        public List<Clue> Clues;
        public CollectibleItemSet CollectibleWorldItems;
        public CollectibleItemSet CollectibleWorldClues;

        public InventoryData()
        {
            Items = new List<Item>();
            Clues = new List<Clue>();
            CollectibleWorldItems = new CollectibleItemSet();
            CollectibleWorldClues = new CollectibleItemSet();
        }
    }
}
