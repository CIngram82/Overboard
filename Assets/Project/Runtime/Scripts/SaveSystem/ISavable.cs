namespace SaveSystem
{
    public interface ISavable
    {
        string SaveKey { get; }

        void Save();
        void Load();
        void SubToEvents(bool subscribe);
    }
}





