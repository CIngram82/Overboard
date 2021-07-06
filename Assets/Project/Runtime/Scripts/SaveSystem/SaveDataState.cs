namespace SaveSystem.Data
{
    [System.Serializable]
    public class SaveDataState
    {
        public GameData GameData;
        public PlayerData PlayerData;
        public InventoryData InventoryData;
        public TutorialData TutorialData;


        public SaveDataState()
        {
            GameData = new GameData();
            PlayerData = new PlayerData();
            InventoryData = new InventoryData();
            TutorialData = new TutorialData();
        }
    }
}
