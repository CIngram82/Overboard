using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CluePanel : MonoBehaviour
{
   [SerializeField] List<Clue> clues = new List<Clue>();
    // Start is called before the first frame update

   public void resetClues()
   {
        foreach(Clue currentclue in clues)
        {
            currentclue.gameObject.SetActive(true);
        }
        gameObject.SetActive(false);
   }
}
