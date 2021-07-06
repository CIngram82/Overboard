using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using Inventory.Database;

public class ClueUI : MonoBehaviour
{
    public Action<Clue> ClueHintLoaded;

    [SerializeField] TextMeshProUGUI _clueNameTMP;

    public Clue Clue { get; private set; }
    public Button ItemButton { get; private set; }

    public void OnClickLoadClue()
    {
        Debug.Log("Loading clue");
        ClueHintLoaded?.Invoke(Clue);
    }

    public void Setup(Clue clue)
    {
        Clue = clue;
        ItemButton = GetComponent<Button>();
       _clueNameTMP.text = clue.Name;
    }
}





