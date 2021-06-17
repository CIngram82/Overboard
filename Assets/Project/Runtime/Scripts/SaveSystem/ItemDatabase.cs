using UnityEngine;

namespace Inventory.Database
{
    [CreateAssetMenu(fileName = "ItemDatabase", menuName = "Create Item Database")]
    [System.Serializable]
    public class ItemDatabase : InventoryDatabase<Item>
    {

    }

    [System.Serializable]
    public class Item : InventoryItem
    {
        [TextArea(5, 10)]
        [SerializeField] string _description;

        public string Description { get => _description; private set => _description = value; }


        public Item(int id, string name, string description)
        {
            ID = id;
            Name = name;
            Description = description;
        }
    } 
}
