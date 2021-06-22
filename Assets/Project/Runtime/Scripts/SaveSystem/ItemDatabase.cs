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
        [SerializeField] Image _image;
        [TextArea(5, 10)]
        [SerializeField] string _description;

        public GameObject Prefab { get => _prefab; private set => _prefab = value; }
        public Image Image { get => _image; private set => _image = value; }
        public string Description { get => _description; private set => _description = value; }


        public Item(int id, string name, string description, Image image)
        {
            ID = id;
            Name = name;
            Description = description;
            Image = image;
        }
    } 
}
