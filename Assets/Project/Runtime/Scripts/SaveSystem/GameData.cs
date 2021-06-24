namespace SaveSystem.Data
{
    [System.Serializable]
    public class GameData
    {
        public bool IsGearsCompleted;
        public bool IsPipesCompleted;
        public float WaterLevel;

        public GameData()
        {
            IsGearsCompleted = false;
            IsPipesCompleted = false;
            WaterLevel = 0.0f;
        }
    }
}
