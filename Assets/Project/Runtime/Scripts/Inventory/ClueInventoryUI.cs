using System.Collections.Generic;
using UnityEngine;
using Inventory.Database;
using TMPro;

public class ClueInventoryUI : MonoBehaviour
{
    [SerializeField] ClueUI _uiCluePrefab;
    [SerializeField] GameObject _journal;
    [Header("Clue TOC")]
    [SerializeField] Transform _uiCluesParent;
    [SerializeField] List<ClueUI> _uiClues;
    [Header("Current Clue")]
    [SerializeField] TextMeshProUGUI _clueNameTMP;
    [SerializeField] TextMeshProUGUI _clueHintTMP;

    bool isJournalOpen = false;

    public List<ClueUI> UIClues { get => _uiClues; private set => _uiClues = value; }


    void OpenJournal(bool isOpen)
    {
        _journal.SetActive(isOpen);
        EventsManager.CameraSwitched(isOpen);
    }

    void LoadClue(Clue clue)
    {
        _clueNameTMP.text = clue.Name;
        _clueHintTMP.text = clue.Hint;
    }

    /// <summary>
    /// Adds UI element for clue to parent panel.
    /// Makes current journal entry clues info.
    /// </summary>
    /// <param name="clue"></param>
    void AddUIClue(Clue clue)
    {
        ClueUI clueUI = Instantiate(_uiCluePrefab, _uiCluesParent);
        clueUI.Setup(clue);
        clueUI.ClueHintLoaded += LoadClue;
        UIClues.Add(clueUI);
        LoadClue(clue);
        Debug.Log("Clue added to UI.");
    }
    void RemoveUIClue(Clue clue)
    {
        ClueUI clueUI = UIClues.Find(x => x.Clue.Name == clue.Name);
        UIClues.Remove(clueUI);
        clueUI.ClueHintLoaded -= LoadClue;
        Destroy(clueUI.gameObject);
        Debug.Log("Clue removed from UI.");
    }

    #region Event Calls
    void On_Add_Clue(Clue clue) => AddUIClue(clue);
    void On_Remove_Clue(Clue clue) => RemoveUIClue(clue);
    void On_Open_Journal(bool open) => OpenJournal(open);
    #endregion

    void SubToEvents(bool subscribe)
    {

        EventsManager.InventoryClueAdded -= On_Add_Clue;
        EventsManager.InventoryClueRemoved -= On_Remove_Clue;
        EventsManager.JournalOpened -= On_Open_Journal;

        if (subscribe)
        {
            EventsManager.InventoryClueAdded += On_Add_Clue;
            EventsManager.InventoryClueRemoved += On_Remove_Clue;
            EventsManager.JournalOpened += On_Open_Journal;
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
    void Update()
    {
        if (PauseController.IsPaused) return;
        if (Input.GetKeyDown(KeyCode.J))
        {
            isJournalOpen = !isJournalOpen;
            OpenJournal(isJournalOpen);
        }        
    }
    void Start()
    {
        Inventory.Inventory.RemoveAllUIChildren(_uiCluesParent);
    }
}
