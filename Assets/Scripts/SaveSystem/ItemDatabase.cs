using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Create Item Database")]
[System.Serializable]
public class ItemDatabase : ScriptableObject
{
    [SerializeField] List<Item> _items;
    
    public List<Item> Items { get => _items; }


    public Item GetItem(string itemName)
    {
        return Items.Find(x => x.Name == itemName);
    }
    public Item GetItem(int itemIndex)
    {
        return Items[itemIndex];
    }
}

[System.Serializable]
public class Item
{
    [SerializeField] int _id;
    [SerializeField] string _name;
    [TextArea(5, 10)]
    [SerializeField] string _description;

    public int ID
    {
        get => _id;
        private set => _id = value;
    }
    public string Name
    {
        get => _name;
        private set => _name = value;
    }
    public string Description 
    { 
        get => _description; 
        private set => _description = value;
    }


    public Item(int id, string name, string description)
    {
        ID = id;
        Name = name;
        Description = description;
    }
}
