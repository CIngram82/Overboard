using UnityEngine;

namespace InventorySystem.Database
{
    [CreateAssetMenu(fileName = "ClueDatabase", menuName = "Create Database/Clue Database")]
    [System.Serializable]
    public class ClueDatabase : InventoryDatabase<Clue>
    {

    }

    [System.Serializable]
    public class Clue : InventoryItem
    {
        [TextArea(5, 10)]
        [SerializeField] string _note;

        public string Hint { get => _note; private set => _note = value; }


        public Clue(int id, string name, string note)
        {
            ID = id;
            Name = name;
            Hint = note;
        }
    } 
}
