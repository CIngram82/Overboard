namespace SaveSystem
{
    public interface ISavable
    {
        string SaveKey { get; }

        void Save();
        void Load();
        void SubscribeToEvents();
        void UnsubscribeFromEvents();
    } 
}





