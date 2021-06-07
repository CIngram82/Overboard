namespace SaveSystem.Data
{
    [System.Serializable]
    public class SaveDataState
    {
        // Player Data
        public PlayerData PlayerData;
        // Inventory Data
        public InventoryData InventoryData;

        public SaveDataState()
        {
            PlayerData = new PlayerData();
            InventoryData = new InventoryData();
        }
    }
}
