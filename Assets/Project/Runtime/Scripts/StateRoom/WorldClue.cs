using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Inventory.Database;

namespace Inventory.Collectable
{
    public class WorldClue : MonoBehaviour
    {
        [DatabaseItem]
        [SerializeField] string _clueName;
        [SerializeField] ClueDatabase _database;

        UID uniqueID;
        CollectibleItemSet _itemSet;

        public Clue Clue { get; private set; }
        public CollectibleItemSet CollectibleClues
        {
            get
            {
                if (_itemSet == null)
                {
                    _itemSet = FindObjectOfType<Inventory>().CollectedWorldClues;
                }
                return _itemSet;
            }
        }


        void CheckCollection()
        {
            if (CollectibleClues.CollectedItems.Contains(uniqueID.ID))
                //Destroy(gameObject);
                gameObject.SetActive(false);
        }



        [TextArea]
        [SerializeField] string Message;
        [SerializeField] TextMeshProUGUI clueText;
        [SerializeField] GameObject paperPanel;


        void OnMouseEnter()
        {
            clueText.text = Message;
            paperPanel.SetActive(true);
        }

        void OnMouseExit()
        {
            paperPanel.SetActive(false);
        }

    } 
}
