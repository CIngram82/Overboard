using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using Inventory.Database;

public class ClueUI : MonoBehaviour
{
    public Action<Clue> ClueHintLoaded;

    [SerializeField] TextMeshProUGUI _clueName;

    public Clue Clue { get; private set; }
    public Button ItemButton { get; private set; }


    public void OnClickLoadClue()
    {
        ClueHintLoaded?.Invoke(Clue);
    }

    public void Setup(Clue clue)
    {
        Clue = clue;
        ItemButton = GetComponent<Button>();
        ItemButton.onClick.AddListener(OnClickLoadClue);

        _clueName.text = clue.Name;
        Debug.Log(Clue.Hint);
    }
}





