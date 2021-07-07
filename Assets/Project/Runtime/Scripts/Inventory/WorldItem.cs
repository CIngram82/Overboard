using UnityEngine;
using Inventory.Database;

namespace Inventory.Collectable
{
    public class WorldItem : MonoBehaviour
    {
        [DatabaseItem]
        [SerializeField] string _itemName;
        [SerializeField] ItemDatabase _database;

        UID uniqueID;
        CollectibleItemSet _itemSet;

        public Item Item { get; private set; }
        public CollectibleItemSet CollectibleItems
        {
            get
            {
                if (_itemSet == null)
                {
                    _itemSet = FindObjectOfType<Inventory>().CollectedWorldItems;
                }
                return _itemSet;
            }
        }


        void CheckCollection()
        {
            if (CollectibleItems.CollectedItems.Contains(uniqueID.ID))
                //Destroy(gameObject);
                gameObject.SetActive(false);
        }

        public void PickUpItem(GameObject player)
        {
            Debug.Log("PickUpItem");
            if (player.TryGetComponent(out Inventory inventory))
            {
                if (inventory.Items.Count >= inventory.Capacity)
                {
                    Debug.Log("Inventory is full.");
                    return;
                }
                Debug.Log("start Pickup");
                inventory.CollectedWorldItems.CollectedItems.Add(uniqueID.ID);
                inventory.AddItem(Item);
                // Destroy(gameObject);
                gameObject.SetActive(false);
                Debug.Log("end PickUp");
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
            Item = _database.GetInventoryItem(_itemName);

            uniqueID = new UID(name, transform.position.sqrMagnitude.ToString(), transform.GetSiblingIndex().ToString());
            CheckCollection();
        }
    }
}
