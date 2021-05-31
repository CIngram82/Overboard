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

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Inventory inventory))
        {
            _collectibleItemSet.CollectedItems.Add(uniqueID.ID);
            inventory.AddItem(Item);
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Item = _itemDatabase.GetItem(_itemName);

        uniqueID = new UID(name, transform.position.sqrMagnitude.ToString(), transform.GetSiblingIndex().ToString());
        CheckCollection();
    }
}





