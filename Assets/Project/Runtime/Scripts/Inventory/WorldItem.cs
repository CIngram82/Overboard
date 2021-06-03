using UnityEngine;

public class WorldItem : MonoBehaviour
{
    [WorldItem]
    [SerializeField] string _itemName;
    [SerializeField] CollectibleItemSet _collectibleItemSet;
    [SerializeField] ItemDatabase _itemDatabase;

    UID uniqueID;

    public Item Item { get; private set; }


    void CheckCollection()
    {
        if (_collectibleItemSet.CollectedItems.Contains(uniqueID.ID))
            Destroy(gameObject);
    }

    public void PickUpItem(GameObject item)
    {
        if (item.TryGetComponent(out Inventory inventory))
        {
            if (inventory.Items.Count >= inventory.Capacity)
            {
                Debug.Log("Inventory is full.");
                return;
            }
            inventory.CollectedWorldItems.CollectedItems.Add(uniqueID.ID);
            inventory.AddItem(Item);
            Destroy(gameObject);
        }
    }

/*
    void OnTriggerEnter(Collider other)
    {
        PickUpItem(other.gameObject);
    }
*/
   
    void Start()
    {
        Item = _itemDatabase.GetItem(_itemName);

        uniqueID = new UID(name, transform.position.sqrMagnitude.ToString(), transform.GetSiblingIndex().ToString());
        CheckCollection();
    }
}





