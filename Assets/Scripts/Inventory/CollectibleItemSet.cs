using System.Collections.Generic;

namespace Inventory.Collectable
{
    public class CollectibleItemSet
    {
        public string SaveKey { get; private set; }
        public HashSet<string> CollectedItems { get; private set; } = new HashSet<string>();


        public CollectibleItemSet(string saveKey)
        {
            SaveKey = saveKey;
            CollectedItems = new HashSet<string>();
        }
    } 
}
