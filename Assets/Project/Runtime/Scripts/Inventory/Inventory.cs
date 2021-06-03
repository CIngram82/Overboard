using System.Collections.Generic;
using UnityEngine;
using SaveSystem;

public class Inventory : MonoBehaviour, ISavable
{
    [SerializeField] string _saveKey = "Inventory";
    public string SaveKey { get => _saveKey; private set => _saveKey = value; }
    CollectibleItemSet _collectedWorldItems;

    public int Capacity { get; } = 6;
    public List<Item> Items { get; private set; } = new List<Item>();
    public CollectibleItemSet CollectedWorldItems => _collectedWorldItems;


    public void AddItem(Item item)
    {
        Items.Add(item);
        GameEvents.On_Inventory_Item_Added(item);
        Debug.Log("Item added.");
    }
    public void AddItems(List<Item> items)
    {
        foreach (Item item in items)
        {
            AddItem(item);
        }
    }
    public void RemoveItem(Item item)
    {
        Items.Remove(item);
        // TODO: add drop item feature if needed.
        GameEvents.On_Inventory_Item_Removed(item);
        Debug.Log("Item removed.");
    }
    public void RemoveItemAt(int index)
    {
        Items.RemoveAt(index);
        // TODO: add drop item feature if needed.
        //GameEvents.On_Inventory_Item_Removed(item);
        Debug.Log("Item removed.");
    }

    public void Save()
    {
        SaveLoad.Save(Items, SaveKey);
    }
    public void Load()
    {
        if (!SaveLoad.SaveExists(SaveKey))
        {
            Debug.LogWarning($"No save of {SaveKey} to load.");
            return;
        }

        AddItems(SaveLoad.Load<List<Item>>(SaveKey));
    }

    public void SubscribeToEvents()
    {
        GameEvents.SaveInitiated += Save;
        GameEvents.LoadInitiated += Load;
    }
    public void UnsubscribeFromEvents()
    {
        GameEvents.SaveInitiated -= Save;
        GameEvents.LoadInitiated -= Load;
    }

    private void OnEnable()
    {
        SubscribeToEvents();
    }
    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }
    private void Awake()
    {
        Load();
    }
}
