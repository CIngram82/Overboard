using System.Collections.Generic;
using UnityEngine;

public class LockableObject : MonoBehaviour
{
    [WorldItem]
    [SerializeField] List<string> _keys;
    [SerializeField] ItemDatabase _itemDatabase;
    [SerializeField] bool _isUnlocked = false;

    public bool IsUnlocked { get => _isUnlocked; private set => _isUnlocked = value; }


    bool HasAllKeys<T>(List<T> collection, List<T> keys)
    {
        foreach (var key in keys)
        {
            if (!collection.Contains(key))
                return false;
        }
        return true;
    }

    void OnTriggerStay(Collider collider)
    {
        if (Input.GetMouseButton(0))
        {
            if (collider.gameObject.TryGetComponent(out Inventory.Inventory inventory) &&
                HasAllKeys(inventory.Items, _keys.ConvertAll(key => _itemDatabase.GetItem(key))))
            {
                // Unlock object
                IsUnlocked = true;
                Debug.Log("Object unlocked.");

                inventory.RemoveAllItems(_keys.ConvertAll(key => _itemDatabase.GetItem(key)));
            }
            else
            {
                Debug.LogWarning("You don't have all key objects.");
            }
        }
    }
}
