using System.Collections.Generic;
using UnityEngine;
using InventorySystem.Database;
using InventorySystem;

public class LockableObject : MonoBehaviour
{
    [DatabaseItem]
    [SerializeField] List<string> _keys;
    [SerializeField] ItemDatabase _database;
    [SerializeField] bool _isUnlocked = false;
    [SerializeField] Animator anim;
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
            // checks if player Inventory and if player has all keys for locked object.
            if (collider.gameObject.TryGetComponent(out Inventory inventory) &&
                HasAllKeys(inventory.Items, _keys.ConvertAll(key => _database.GetInventoryItem(key))))
            {
                // Unlock object
                IsUnlocked = true;
                Debug.Log("Object unlocked.");
                inventory.RemoveAllItems(_keys.ConvertAll(key => _database.GetInventoryItem(key)));
                anim.SetBool("isOpen",true);
            }
            else
            {
              // AudioScript._instance.PlaySoundEffect("Locked Door");
                Debug.LogWarning("You don't have all key objects.");
            }

            
        }
        if(Input.GetMouseButtonDown(0) && !IsUnlocked)
        {
            AudioScript._instance.PlaySoundEffect("Locked Door");
        }
    }
}
