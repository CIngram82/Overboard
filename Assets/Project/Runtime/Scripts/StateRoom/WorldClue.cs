using UnityEngine;
using InventorySystem.Database;

namespace InventorySystem.Collectable
{
    public class WorldClue : MonoBehaviour
    {
        [DatabaseItem]
        [SerializeField] string _clueName;
        [SerializeField] ClueDatabase _database;

        UID uniqueID;
        CollectibleItemSet _clueSet;

        public Clue Clue { get; private set; }
        public CollectibleItemSet CollectibleClues
        {
            get
            {
                if (_clueSet == null)
                {
                    _clueSet = FindObjectOfType<Inventory>().CollectedWorldClues;
                }
                return _clueSet;
            }
        }


        void CheckCollection()
        {
            if (CollectibleClues.CollectedItems.Contains(uniqueID.ID))
                gameObject.SetActive(false);
        }

        public void PickUpClue(GameObject player)
        {
            if (player.TryGetComponent(out Inventory inventory))
            {
                inventory.CollectedWorldItems.CollectedItems.Add(uniqueID.ID);
                inventory.AddClue(Clue);
                // Destroy(gameObject);
                gameObject.SetActive(false);
            }
        }

        void Start()
        {
            Clue = _database.GetInventoryItem(_clueName);

            uniqueID = new UID(name, transform.position.sqrMagnitude.ToString(), transform.GetSiblingIndex().ToString());
            CheckCollection();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PickUpClue(other.gameObject);
            }
        }
    }


}
