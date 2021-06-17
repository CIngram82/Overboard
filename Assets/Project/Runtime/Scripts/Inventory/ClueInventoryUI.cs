using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Database;
using TMPro;

public class ClueInventoryUI : MonoBehaviour
{
    [SerializeField] ClueUI _uiCluePrefab;
    [SerializeField] GameObject _pagePanel;
    [SerializeField] TextMeshProUGUI _clueNameTMP;
    [SerializeField] TextMeshProUGUI _ClueHintTMP;

    public List<ClueUI> UIClues { get; private set; } = new List<ClueUI>();


    void LoadClue(Clue clue)
    {
        _clueNameTMP.text = clue.Name;
        _ClueHintTMP.text = clue.Hint;
    }

    void AddUIClue(Clue clue)
    {
        ClueUI uiClueInstance = Instantiate(_uiCluePrefab, transform);
        uiClueInstance.Setup(clue);
        uiClueInstance.ClueHintLoaded += LoadClue;
        UIClues.Add(uiClueInstance);
        Debug.Log("Clue added to UI.");
    }
    void RemoveUIClue(Clue clue)
    {
        ClueUI uIClue = UIClues.Find(x => x.Clue.Name == clue.Name);
        UIClues.Remove(uIClue);
        uIClue.ClueHintLoaded -= LoadClue; 
        Destroy(uIClue.gameObject);
        Debug.Log("Clue removed from UI.");
    }

    void On_Add_Clue(Clue clue) => AddUIClue(clue);
    void On_Remove_Clue(Clue clue) => RemoveUIClue(clue);

    void SubToEvents(bool subscribe)
    {

        EventsManager.InventoryClueAdded -= On_Add_Clue;
        EventsManager.InventoryClueRemoved -= On_Remove_Clue;

        if (subscribe)
        {
            EventsManager.InventoryClueAdded += On_Add_Clue;
            EventsManager.InventoryClueRemoved += On_Remove_Clue;
        }
    }

    void OnEnable()
    {
        SubToEvents(true);
    }
    void OnDisable()
    {
        SubToEvents(false);
    }
}





