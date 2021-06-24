namespace SaveSystem.Data
{
    [System.Serializable]
    public class SaveDataState
    {
        // Game Data
        public GameData GameData;
        // Player Data
        public PlayerData PlayerData;
        // Inventory Data
        public InventoryData InventoryData;


        public SaveDataState()
        {
            GameData = new GameData();
            //PlayerData = new PlayerData();
            InventoryData = new InventoryData();
        }
    }
}
