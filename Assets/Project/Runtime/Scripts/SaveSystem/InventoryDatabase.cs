using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Database
{
    [System.Serializable]
    public class InventoryDatabase<T> : ScriptableObject where T : InventoryItem
    {
        [SerializeField] protected List<T> _items;

        public List<T> Items { get => _items; }


        public T GetInventoryItem(string itemName)
        {
            return Items.Find(x => x.Name == itemName);
        }
        public T GetInventoryItem(int itemIndex)
        {
            return Items[itemIndex];
        }
    }

    [System.Serializable]
    public abstract class InventoryItem
    {
        [SerializeField] protected int _id;
        [SerializeField] protected string _name;

        public int ID { get => _id; protected set => _id = value; }
        public string Name { get => _name; protected set => _name = value; }
    } 
}