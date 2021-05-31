using System.Collections.Generic;
using UnityEngine;
using SaveSystem;

public class CollectibleItemSet : MonoBehaviour, ISavable
{
    [SerializeField] string _saveKey = "CollectedItems";

    public string SaveKey { get => _saveKey; private set => _saveKey = value; }
    public HashSet<string> CollectedItems { get; private set; } = new HashSet<string>();


    public void Save()
    {
        SaveLoad.Save(CollectedItems, SaveKey);
    }
    public void Load()
    {
        if (SaveLoad.SaveExists(SaveKey))
            CollectedItems = SaveLoad.Load<HashSet<string>>(SaveKey);
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





