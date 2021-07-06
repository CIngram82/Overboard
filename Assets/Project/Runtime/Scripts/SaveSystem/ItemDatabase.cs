using UnityEngine;
using UnityEngine.UI;

namespace Inventory.Database
{
    [CreateAssetMenu(fileName = "ItemDatabase", menuName = "Create Database/Item Database")]
    [System.Serializable]
    public class ItemDatabase : InventoryDatabase<Item>
    {

    }

    [System.Serializable]
    public class Item : InventoryItem
    {
        [SerializeField] GameObject _prefab;
        [SerializeField] Sprite _sprite;
        [TextArea(5, 10)]
        [SerializeField] string _description;

        public GameObject Prefab { get => _prefab; private set => _prefab = value; }
        public Sprite Sprite { get => _sprite; private set => _sprite = value; }
        public string Description { get => _description; private set => _description = value; }


        public Item(int id, string name, string description, Sprite sprite)
        {
            ID = id;
            Name = name;
            Description = description;
            Sprite = sprite;
        }
    } 
}
