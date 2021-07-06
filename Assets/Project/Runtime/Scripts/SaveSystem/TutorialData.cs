namespace SaveSystem
{
    [System.Serializable]
    public class TutorialData
    {
        public bool FirstInspect;
        public bool FirstClue;


        public TutorialData()
        {
            FirstInspect = true;
            FirstClue = true;
        }
        public TutorialData(bool tutorial)
        {
            FirstInspect = tutorial;
            FirstClue = tutorial;
        }
    }
}
