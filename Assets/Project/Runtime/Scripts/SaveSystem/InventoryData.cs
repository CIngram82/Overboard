using System.Collections.Generic;
using Inventory.Collectable;

namespace SaveSystem.Data
{
    [System.Serializable]
    public class InventoryData
    {
        public List<Item> Items;
        public CollectibleItemSet CollectibleItemSet;


        public InventoryData()
        {
            Items = new List<Item>();
            CollectibleItemSet = new CollectibleItemSet();
        }
    }
}
