using System.Collections.Generic;
using UnityEngine;
using InventorySystem.Database;

public class StartingClues : MonoBehaviour
{

    [SerializeField] ClueDatabase _database;

    [DatabaseItem]
    [SerializeField] List<string> clueNames;
    private List<Clue> clues;


    void Start()
    {
        clues = new List<Clue>();
        foreach (string _clueName in clueNames)
        {
            Clue c = _database.GetInventoryItem(_clueName);
            clues.Add(c); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player.inventory.AddAllClues(clues);
            Destroy(gameObject);
        }
    }
}
