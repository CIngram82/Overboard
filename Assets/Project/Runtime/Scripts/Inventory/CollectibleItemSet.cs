using System.Collections.Generic;

namespace InventorySystem.Collectable
{
    [System.Serializable]
    public class CollectibleItemSet
    {
        public HashSet<string> CollectedItems { get; private set; }


        public CollectibleItemSet()
        {
            CollectedItems = new HashSet<string>();
        }
    } 
}
